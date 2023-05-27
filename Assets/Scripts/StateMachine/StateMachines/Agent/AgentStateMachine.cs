using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentStateMachine : StateMachine
{
    //Consts
    private const float MinMovementSpeedValue = 0f;
    private const float MaxMovementSpeedValue = 6f;
    public float minMovementSpeed => MinMovementSpeedValue;
    public float maxMovementSpeed => MaxMovementSpeedValue;


    //SerializeFields
    [field: Header("Components")]
    [field: SerializeField] private GameObject agentModel;

    [field: Header("Values")]
    [field: SerializeField]
    [field: Range(MinMovementSpeedValue, MaxMovementSpeedValue)]
    public float MovementSpeed { get; private set; }


    //Components
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }
    public StateInfo StateInfo { get; private set; }
    public Combat Combat { get; private set; }
    public Health Health { get; private set; }
    public AgentSelectable Selectable { get; private set; }

    private void Awake()
    {
        //Get attached Components
        Animator = agentModel.GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        StateInfo = GetComponent<StateInfo>();
        Combat = GetComponent<Combat>();
        Health = GetComponent<Health>();
        Selectable = GetComponent<AgentSelectable>();
    }

    private void Start()
    {
        SwitchState(new AgentPatrolState(this));
    }
}
