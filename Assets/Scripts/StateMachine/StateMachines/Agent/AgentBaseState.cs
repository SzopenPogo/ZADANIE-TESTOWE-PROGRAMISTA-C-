using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentBaseState : State
{
    protected AgentStateMachine stateMachine;

    public AgentBaseState(AgentStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
