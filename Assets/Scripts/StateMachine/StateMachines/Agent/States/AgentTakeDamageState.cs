using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTakeDamageState : AgentBaseState
{
    //Animation
    private readonly int TakeDamageAnimationHash = Animator.StringToHash("TakeDamage");
    private const string TakeDamageAnimationTag = "TakeDamage";
    private const float CrossFadeDuration = 0.2f;

    //Values
    private const int Damage = 1;

    public AgentTakeDamageState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //State Info
        stateMachine.StateInfo.SetCurrentStateInfo(StateType.TakeDamage);

        //Animation
        stateMachine.Animator.CrossFadeInFixedTime(TakeDamageAnimationHash, CrossFadeDuration);

        //Subs
        stateMachine.Combat.OnAttackEvent += SetAttackState;
    }

    public override void Exit()
    {
        //Subs
        stateMachine.Combat.OnAttackEvent -= SetAttackState;
    }

    public override void FixedTick(float deltaTime)
    {

    }

    public override void LateTick(float deltaTime)
    {

    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime(TakeDamageAnimationTag);

        if (normalizedTime > 1f)
        {
            stateMachine.Combat.ResetActiveEnemy();
            stateMachine.Health.DealDamage(Damage);

            //If Agent is dead
            if (!stateMachine.Health.IsAlive)
            {
                SetDeathState();
                return;
            }

            //Check agent waiting enemies
            if (stateMachine.Combat.WaitingEnemies.Count > 0)
            {
                stateMachine.Combat.TryAttackEnemy();
            }
            else
                SetPatrolState();
        }
    }
}
