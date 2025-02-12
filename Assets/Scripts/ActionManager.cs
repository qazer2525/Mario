
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;

    public UnityEvent Z;

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpHold.Invoke();
        }
    }

    // called twice, when pressed and unpressed
    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jump.Invoke();
        }

    }

    // called twice, when pressed and unpressed
    public void OnMoveAction(InputAction.CallbackContext context)
    {
        // Debug.Log("OnMoveAction callback invoked");
        if (context.started)
        {
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        if (context.canceled)
        {
            moveCheck.Invoke(0);
        }

    }



    public void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("mouse click started");
        else if (context.performed)
        {
            Debug.Log("mouse click performed");
        }
        else if (context.canceled)
            Debug.Log("mouse click cancelled");
    }

    public void OnPointAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            Debug.Log($"Point detected: {point}");

        }
    }
    public void OnZAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("Z started");
        else if (context.performed)
        {
            Z.Invoke();
        }
        else if (context.canceled)
            Debug.Log("Z cancelled");
    }

}
