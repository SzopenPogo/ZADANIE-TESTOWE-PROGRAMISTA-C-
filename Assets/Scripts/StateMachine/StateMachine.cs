using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    private void Update() => currentState?.Tick(Time.deltaTime);

    private void FixedUpdate() => currentState?.FixedTick(Time.deltaTime);

    private void LateUpdate() => currentState?.LateTick(Time.deltaTime);

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}