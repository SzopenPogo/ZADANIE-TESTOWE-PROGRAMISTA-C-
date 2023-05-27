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

    protected void SetWaitingState()
    {
        stateMachine.SwitchState(new AgentWaitingState(stateMachine));
    }

    protected float GetNormalizedTime(string animationTag)
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag(animationTag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag(animationTag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected bool LookAtTarget(Transform targetTransform, float speed)
    {
        //Look Position
        Vector3 lookPosition = targetTransform.position;
        lookPosition.y = stateMachine.transform.position.y;

        //Calculate target rotation
        Quaternion targetRotation = Quaternion.LookRotation((lookPosition - stateMachine.transform.position));
        targetRotation.x = 0f;
        targetRotation.z = 0f;

        //Rotate towards target position
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, Time.deltaTime * speed);

        return stateMachine.transform.rotation == targetRotation;
    }
}
