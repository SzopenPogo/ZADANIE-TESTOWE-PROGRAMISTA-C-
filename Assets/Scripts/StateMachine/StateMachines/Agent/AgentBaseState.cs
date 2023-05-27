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

    protected void SetMovementSpeed(float speed) => stateMachine.NavMeshAgent.speed = speed;

    protected float GetMovementSpeedPercent()
    {
        float movementSpeedPercent = stateMachine.MovementSpeed / stateMachine.maxMovementSpeed;
        return Mathf.Clamp(movementSpeedPercent, 0, 1);
    }

    protected void SetDestination(Vector3 destination)
    {
        stateMachine.NavMeshAgent.SetDestination(destination);
    }

    protected bool IsNavMeshAgentRunningInPlace()
    {
        if (stateMachine.NavMeshAgent.velocity.normalized == Vector3.zero)
            return true;

        return false;
    }

    protected void SetAttackState()
    {
        stateMachine.SwitchState(new AgentAttackState(stateMachine));
    }
}
