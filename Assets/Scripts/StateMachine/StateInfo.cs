using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInfo : MonoBehaviour
{
    public StateType CurrentState { get; private set; }

    public void SetCurrentStateInfo(StateType newState) => CurrentState = newState;
}
