using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private PlayerInput pl_input;

    private InputAction touchTrigger;
    [SerializeField]
    private GameObject AcceptCircle;
    private AcceptCircleScrpt AcceptScr;

    void Awake()
    {
        AcceptScr = AcceptCircle.GetComponent<AcceptCircleScrpt>();
        pl_input = GetComponent<PlayerInput>();
        touchTrigger = pl_input.actions.FindAction("TriggerTouch");
    }

    private void OnEnable()
    {
        //touchTrigger.performed += TouchFunc;
    }

    private void OnDisable()
    {
        //touchTrigger.performed -= TouchFunc;
    }

    private void TouchFunc(InputAction.CallbackContext context)
    {
        //bool touch = context.ReadValueAsButton();
        //Debug.Log("Main Touch: " + touch);
        //AcceptScr.AcceptTrigger();
    }

}
