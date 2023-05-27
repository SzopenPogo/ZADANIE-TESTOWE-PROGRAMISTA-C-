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
    [field: SerializeField] public AgentSpawnerAreaData AreaData { get; private set; }
    private List<GameObject> agents = new();
    private bool isAgentsRespawning;

    private void OnValidate() => ValidateData();

    private void Awake() => Instance = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Debug.Log(agents.Count);
    }

    private void Start()
    {
        ValidateData();
        SpawnStartAgents();

        StartCoroutine(AgentRespawner());
    }

    private void ValidateData()
    {
        spawnData.ValidateData();
        AreaData.Validate();
    }

    public Vector3 GetRandomPointInAgentArea()
    {
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.x = UnityEngine.Random.Range(0, AreaData.AgentAreaBounds.x);
        spawnPoint.z = UnityEngine.Random.Range(0, AreaData.AgentAreaBounds.y);

        return spawnPoint;
    }

    private int GetRandomAgentIndex() => UnityEngine.Random.Range(0, agentsPrefabs.Count);

    private void SpawnAgent()
    {
        //Set agent
        GameObject agent = agentsPrefabs[GetRandomAgentIndex()];
        agent.transform.position = GetRandomPointInAgentArea();

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