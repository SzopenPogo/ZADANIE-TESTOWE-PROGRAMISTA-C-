using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    public static MouseRaycast Instance;

    //Raycast Data
    public Ray Ray { get; private set; }
    public RaycastHit RaycastHitData { get; private set; }

    private void Awake() => Instance = this;

    private void Update()
    {
        CastRaycast();
    }

    //Default Raycast
    private void CastRaycast()
    {
        Ray = Camera.main.ScreenPointToRay(InputReader.Instance.MousePositionValue);
        Physics.Raycast(Ray, out RaycastHit raycastHitData);

        RaycastHitData = raycastHitData;
    }

    public GameObject GetHittedGameObject()
    {
        GameObject hittedGameObject = null;

        //If hitted object don't have any collider
        if (RaycastHitData.collider == null)
            return hittedGameObject;

        hittedGameObject = RaycastHitData.collider.gameObject;
        return hittedGameObject;
    }
}
