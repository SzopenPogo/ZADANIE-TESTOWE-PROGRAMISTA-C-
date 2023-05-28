using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: Header("Objects")]
    [field: SerializeField] private Transform targetObjectTransform;

    [field: Header("Values")]
    [field: SerializeField, Range(0f, 30f)] private float moveSpeed = 15f;
    [field: SerializeField, Range(0f, 150f)] private float rotationSpeed = 75;

    //Values
    private Vector3 inputMoveDirection = Vector3.zero;
    private Vector3 inputRotationDirection = Vector3.zero;

    private void Start()
    {
        InputReader.Instance.CenterCameraEvent += SetCameraToActiveAreaCenter;

        SetCameraToActiveAreaCenter();
    }

    private void OnDestroy()
    {
        InputReader.Instance.CenterCameraEvent -= SetCameraToActiveAreaCenter;
    }

    private void Update()
    {
        //Update Values
        UpdateInputMoveDirectionValue();
        UpdateInputRotationDirectionValue();

        //Update camera
        UpdateCameraPosition();
        UpdateCameraRotation();
    }

    public void SetCameraToActiveAreaCenter()
    {
        targetObjectTransform.position = new Vector3(Area.Instance.AreaData.ActiveAreaBounds.x /2,
            0f, Area.Instance.AreaData.ActiveAreaBounds.y /2);
    }

    private void UpdateInputMoveDirectionValue()
    {
        //Vertical
        inputMoveDirection.x = InputReader.Instance.MovementValue.y;
        inputMoveDirection.y = 0f;
        inputMoveDirection.z = InputReader.Instance.MovementValue.x;
    }

    private void UpdateInputRotationDirectionValue()
    {
        inputRotationDirection = Vector3.zero;
        inputRotationDirection.y = InputReader.Instance.MouseDelta.x;
    }

    private Vector3 GetMoveValue()
    {
        //Calculate move direction based on target current transform
        //and input value
        Vector3 moveDirection = targetObjectTransform.forward * inputMoveDirection.x
            + targetObjectTransform.right * inputMoveDirection.z;

        //Calculate and return move value
        return moveDirection * moveSpeed * Time.deltaTime;
    }

    private Vector3 GetRotationValue()
    {
        return inputRotationDirection * rotationSpeed * Time.deltaTime;
    }

    private void UpdateCameraPosition()
    {
        //If player is not moving
        if (InputReader.Instance.MovementValue == Vector2.zero)
            return;

        Vector3 updatedCameraPosition = targetObjectTransform.position + GetMoveValue();

        //If updated camera position is out of active area
        if (updatedCameraPosition.x >= Area.Instance.AreaData.ActiveAreaBounds.x ||
            updatedCameraPosition.z >= Area.Instance.AreaData.ActiveAreaBounds.y ||
            updatedCameraPosition.x <= 0 || updatedCameraPosition.z <= 0)
            return;

        //Update camera position
        targetObjectTransform.position = updatedCameraPosition;
    }

    private void UpdateCameraRotation()
    {
        if (!InputReader.Instance.IsLeftMouseButtonPressed)
            return;

        //Rotate camera
        targetObjectTransform.eulerAngles += GetRotationValue();
    }
}
