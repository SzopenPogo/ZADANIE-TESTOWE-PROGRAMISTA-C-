using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelectable : Selectable
{
    [field: Header("Agent Data")]
    [field: SerializeField] private Health health;
    [field: SerializeField] private AgentData data;

    [field: Header("Agent Game Objects")]
    [field: SerializeField] private GameObject pointLight;

    public override void Select()
    {
        if (IsSelected)
            return;

        base.Select();
        UiAgentBar.Instance?.InitializeBar(health, data.Agent);
        pointLight.SetActive(true);
    }

    public override void Unselect()
    {
        if (!IsSelected)
            return;

        base.Unselect();
        UiAgentBar.Instance?.DisableBar();
        pointLight.SetActive(false);
    }
}
