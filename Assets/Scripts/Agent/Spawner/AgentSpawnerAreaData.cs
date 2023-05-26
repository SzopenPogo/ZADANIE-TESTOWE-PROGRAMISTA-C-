using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AgentSpawnerAreaData
{
    private const float MinAgentAreaBound = 1f;

    [field: Header("Area Values")]
    [field: SerializeField]
    [Tooltip("Area in which the agent can move.")]
    public Vector2 AgentAreaBounds { get; private set; } = new(10f, 10f);

    public void Validate()
    {
        if (AgentAreaBounds.x < MinAgentAreaBound)
            AgentAreaBounds = new Vector2(MinAgentAreaBound, AgentAreaBounds.y);
        if (AgentAreaBounds.y < MinAgentAreaBound)
            AgentAreaBounds = new Vector2(AgentAreaBounds.x, MinAgentAreaBound);
    }
}
