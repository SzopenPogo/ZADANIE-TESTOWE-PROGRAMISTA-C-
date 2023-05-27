using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAttackState : AgentBaseState
{
    public AgentAttackState(AgentStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //State Info
        stateMachine.StateInfo.SetCurrentStateInfo(StateType.Attack);
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

    }
}
