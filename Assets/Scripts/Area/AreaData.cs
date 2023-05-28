using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AreaData
{
    [field: Header("Area Values")]
    //Active area
    [field: SerializeField]
    [Tooltip("Area in which the agent can move.")]
    public Vector2 ActiveAreaBounds { get; private set; } = new(10f, 10f);


    [field: SerializeField]
    [Tooltip("Active Area offset.")]
    [Range(20f, 55f)]
    private float ActiveAreaOffset;
    [field: SerializeField]
    public Vector2 MaxInactiveAreaBounds { get; private set; } = new(15f, 15f);
    [field: SerializeField]
    public Vector2 MinInactiveAreaBounds { get; private set; } = new(15f, 15f);

    public void ValidateAreaData()
    {
        ValidateInactiveArea();
    }

    private void ValidateInactiveArea()
    {
        float activeAreaBoundWithOffsetX = ActiveAreaBounds.x + ActiveAreaOffset;
        float activeAreaBoundWithOffsetY = ActiveAreaBounds.y + ActiveAreaOffset;

        MaxInactiveAreaBounds = new Vector2(activeAreaBoundWithOffsetX, activeAreaBoundWithOffsetY);
        MinInactiveAreaBounds = new Vector2(-activeAreaBoundWithOffsetX, -activeAreaBoundWithOffsetY);
    }
}
