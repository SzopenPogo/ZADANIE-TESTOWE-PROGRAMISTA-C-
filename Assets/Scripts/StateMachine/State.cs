using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void FixedTick(float deltaTime);
    public abstract void LateTick(float deltaTime);
    public abstract void Exit();
}
