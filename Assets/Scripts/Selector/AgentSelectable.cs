using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelectable : Selectable
{
    [field: SerializeField] private Health health;
    [field: SerializeField] private AgentData data;

    public override void Select()
    {
        base.Select();
        UiAgentBar.Instance.InitializeBar(health, data.Agent);
    }

    public override void Unselect()
    {
        base.Unselect();
        UiAgentBar.Instance.DisableBar();
    }
}
