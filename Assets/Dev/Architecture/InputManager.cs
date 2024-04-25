using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class InputManager : MonoBehaviour
{
    public static Vector2 inputMove;
    public static bool inputJump;
    public static bool inputCancel;

    public void SetInputMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public static Vector2 GetInputMove()
    {
        return inputMove;
    }

    public void SetInputJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputJump = true;
        }
    }

    public void SetInputCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputCancel = true;
        }
    }
}
