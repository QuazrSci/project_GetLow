using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private Mobile_input input;

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start() {
        input.MainAction.Touch.started += ctx => checkTouch(ctx);
    }


    void checkTouch(InputAction.CallbackContext context)
    {
        
    }
}
