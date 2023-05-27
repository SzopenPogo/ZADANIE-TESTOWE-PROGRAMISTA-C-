using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAttackState : AgentBaseState
{
    //Animation
    private readonly int AttackAnimationHash = Animator.StringToHash("Attack");
    private const string AttackAnimationTag = "Attack";
    private const float CrossFadeDuration = 0.2f;

    //Rotation
    private const float LookAtRotationSpeeed = 2f;

    public AgentAttackState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //State Info
        stateMachine.StateInfo.SetCurrentStateInfo(StateType.Attack);

        //Animation
        stateMachine.Animator.CrossFadeInFixedTime(AttackAnimationHash, CrossFadeDuration);
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
        LookAtTarget(stateMachine.Combat.ActiveEnemy.transform, LookAtRotationSpeeed);

        float normalizedTime = GetNormalizedTime(AttackAnimationTag);

        if (normalizedTime > 1f)
        {

        }
    }
}
