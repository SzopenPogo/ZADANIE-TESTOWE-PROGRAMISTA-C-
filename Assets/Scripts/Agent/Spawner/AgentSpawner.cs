using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentSpawner : MonoBehaviour
{
    public static AgentSpawner Instance;

    [field: Header("Prefabs")]
    [field: SerializeField] private List<GameObject> agentsPrefabs = new();

    [field: Header("Data")]
    [field: SerializeField] private AgentSpawnerSpawnData spawnData;
    [field: SerializeField] public AgentSpawnerAreaData AreaData { get; private set; }

    private void OnValidate() => ValidateData();

    private void Awake() => Instance = this;

    private void Start()
    {
        ValidateData();
        SpawnStartAgents();
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
        GameObject agent = agentsPrefabs[GetRandomAgentIndex()];
        agent.transform.position = GetRandomPointInAgentArea();
        Instantiate(agent, transform);
    }

    private void SpawnStartAgents()
    {
        for (int i = 0; i < spawnData.startAgents; i++)
            SpawnAgent();
    }
}