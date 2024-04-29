using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 inputMove;
    public static bool inputJump = false;
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

    public static bool GetInputJump()
    {
        return inputJump;
    }

    public static bool CancelInputJump()
    {
        inputJump = false;
        return inputJump;
    }

    public void SetInputCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputCancel = true;
        }
    }
}
