using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateMachine : StateMachine
{
    private void Start()
    {
        SwitchState(new AgentPatrolState(this));
    }
}
