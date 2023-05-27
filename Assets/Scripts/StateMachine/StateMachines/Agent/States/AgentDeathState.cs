using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDeathState : AgentBaseState
{
    //Animation
    private readonly int DeathAnimationHash = Animator.StringToHash("Death");
    private const string DeathDamageAnimationTag = "Death";
    private const float CrossFadeDuration = 0.2f;

    //Tag
    private const string DeadTag = "Dead";

    public AgentDeathState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //State Info
        stateMachine.StateInfo.SetCurrentStateInfo(StateType.Death);

        //Animation
        stateMachine.Animator.CrossFadeInFixedTime(DeathAnimationHash, CrossFadeDuration);

        //SetTag
        stateMachine.gameObject.tag = DeadTag;

        //Disable scripts
        stateMachine.Combat.enabled = false;
    }

    public override void Exit()
    {
    }

    public override void FixedTick(float deltaTime)
    {
    }

    public override void LateTick(float deltaTime)
    {
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(DeathDamageAnimationTag);

        if (normalizedTime > 1f)
        {
            stateMachine.Selectable.Unselect();
            AgentSpawner.Instance.DestroyAgent(stateMachine.gameObject);
        }
    }
}
