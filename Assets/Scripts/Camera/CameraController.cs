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

    private Vector3 inputMoveDirection = Vector3.zero;
    private Vector3 inputRotationDirection = Vector3.zero;

    private void Update()
    {
        //Update Values
        UpdateInputMoveDirectionValue();
        UpdateInputRotationDirectionValue();

        //Update camera
        UpdateCameraPosition();
        UpdateCameraRotation();
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

    private void UpdateCameraPosition() => targetObjectTransform.position += GetMoveValue();

    private void UpdateCameraRotation()
    {
        if (!InputReader.Instance.IsLeftMouseButtonPressed)
            return;

        //Rotate camera
        targetObjectTransform.eulerAngles += GetRotationValue();
    }
}
