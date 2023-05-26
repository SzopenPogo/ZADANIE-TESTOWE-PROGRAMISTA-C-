using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Agent", menuName = "Agent")]
public class Agent : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string agentName;

    public Sprite Icon => icon;
    public string AgentName => agentName;
}
