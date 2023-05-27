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

    private void Awake()
    {
        Animator = agentModel.GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SwitchState(new AgentPatrolState(this));
    }
}
