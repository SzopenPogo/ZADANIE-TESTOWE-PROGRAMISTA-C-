using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class AgentSpawner : MonoBehaviour
{
    public static AgentSpawner Instance;

    [field: Header("Prefabs")]
    [field: SerializeField] private List<GameObject> agentsPrefabs = new();

    [field: Header("Data")]
    [field: SerializeField] private AgentSpawnerSpawnData spawnData;
    private List<GameObject> agents = new();
    private bool isAgentsRespawning;
    private void OnValidate() => ValidateData();

    private void Awake() => Instance = this;

    private void Start()
    {
        ValidateData();
        SpawnStartAgents();

        StartCoroutine(AgentRespawner());
    }

    private void ValidateData()
    {
        spawnData.ValidateData();
    }

    private int GetRandomAgentPrefabIndex() => UnityEngine.Random.Range(0, agentsPrefabs.Count);

    private void SpawnAgent()
    {
        //Set agent
        GameObject agent = agentsPrefabs[GetRandomAgentPrefabIndex()];
        agent.transform.position = Area.Instance.GetRandomPointInActiveArea();

        //Spawn agent
        GameObject spawnedAgent = Instantiate(agent, transform);
        agents.Add(spawnedAgent);
    }

    private void SpawnStartAgents()
    {
        for (int i = 0; i < spawnData.startAgents; i++)
            SpawnAgent();
    }

    private float GetRandomSpawnDelay()
    {
        return UnityEngine.Random.Range(spawnData.minSpawnAgentDelay,
            spawnData.maxSpawnAgentDelay);
    }

    private IEnumerator AgentRespawner()
    {
        if (isAgentsRespawning || agents.Count >= spawnData.maxAgents)
            yield break;

        isAgentsRespawning = true;

        while (agents.Count < spawnData.maxAgents)
        {
            yield return new WaitForSeconds(GetRandomSpawnDelay());
            SpawnAgent();
        }

        isAgentsRespawning = false;
    }

    public void DestroyAgent(GameObject agent)
    {
        if (!agents.Contains(agent))
            return;

        agents.Remove(agent);
        Destroy(agent);

        StartCoroutine(AgentRespawner());
    }
}