using System;
using UnityEngine;

[Serializable]
public class AgentSpawnerSpawnData
{
    //Consts
    private const int MinStartAgentsInInspector = 3;
    private const int MaxStartAgentsInInspector = 5;
    private const float MinSpawnAgentDelay = 2f;
    private const float MaxSpawnAgentDelay = 6f;
    private const int MinSpawnAgents = 0;
    private const int MaxSpawnAgents = 30;

    [field: Header("Agent Values")]
    //Start Agents
    [field: SerializeField]
    [Tooltip("Number of agents at the start of the game.")]
    [Range(MinStartAgentsInInspector, MaxStartAgentsInInspector)]
    public int startAgents;

    //Min Spawn Agent Delay
    [field: SerializeField]
    [Range(MinSpawnAgentDelay, MaxSpawnAgentDelay)]
    [Tooltip("Minimum seconds between agent spawns.\nShould always be less or equal than Max Spawn Agent Delay.")]
    public float minSpawnAgentDelay;

    //Max Spawn Agent Delay
    [field: SerializeField]
    [Range(MinSpawnAgentDelay, MaxSpawnAgentDelay)]
    [Tooltip("Maximum seconds between agent spawns.\nShould always be greater or equal than Min Spawn Agent Delay.")]
    public float maxSpawnAgentDelay;

    //Max Agents
    [field: SerializeField]
    [Range(MinSpawnAgents, MaxSpawnAgents)]
    [Tooltip("Maximum number of spawned agents")]
    public int maxAgents = 10;

    public void ValidateData()
    {
        //Validate soawb agent delay
        if (maxSpawnAgentDelay < minSpawnAgentDelay)
            maxSpawnAgentDelay = minSpawnAgentDelay;
    }
}
