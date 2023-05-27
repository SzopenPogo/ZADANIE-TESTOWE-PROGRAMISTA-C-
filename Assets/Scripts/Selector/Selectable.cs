using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool IsSelected { get; protected set; }

    private void OnDestroy()
    {
        Unselect();
    }

    private void OnDisable()
    {
        Unselect();
    }

    public virtual void Select()
    {
        IsSelected = true;
    }

    public virtual void Unselect()
    {
        IsSelected = false;
    }
}
