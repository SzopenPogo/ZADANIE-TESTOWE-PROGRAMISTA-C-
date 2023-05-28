using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public static InputReader Instance;

    //Scripts
    private Controls controls;

    //Vector2 Values
    public Vector2 MovementValue { get; private set; }
    public Vector2 MousePositionValue { get; private set; }
    public Vector2 MouseDelta { get; private set; }

    //Bools
    public bool IsLeftMouseButtonPressed { get; private set; }

    //Actions
    public Action MouseLeftClickEvent;
    public Action CancelClickEvent;
    public Action CenterCameraEvent;

    private void Awake() => Instance = this;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>(); 
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePositionValue = context.ReadValue<Vector2>();
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsLeftMouseButtonPressed = true;
            MouseLeftClickEvent?.Invoke();
        }
        if (context.canceled)
            IsLeftMouseButtonPressed = false;
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        CancelClickEvent?.Invoke();
    }

    public void OnCenterCamera(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        CenterCameraEvent?.Invoke();
    }
}
