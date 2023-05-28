using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool IsSelected { get; protected set; } = false;

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
        if (IsSelected)
            return;

        IsSelected = true;
    }

    public virtual void Unselect()
    {
        if (!IsSelected)
            return;

        IsSelected = false;
    }
}
