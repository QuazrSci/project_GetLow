using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hydraulics : MonoBehaviour
{
    public WheelCollider FrontLeft;
    public WheelCollider FrontRight;
    public WheelCollider RearLeft;
    public WheelCollider RearRight;
    
    public float OriginalDistance;
    public float LowDistance;

    private bool IsOn;

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetButtonDown("AllUp") && !IsOn)
        {
            FrontLeft.suspensionDistance = FrontRight.suspensionDistance = RearLeft.suspensionDistance = RearRight.suspensionDistance = LowDistance;
            IsOn = true;
        }
        else if (Input.GetButtonDown("AllUp") && IsOn)
        {
            FrontLeft.suspensionDistance = FrontRight.suspensionDistance = RearLeft.suspensionDistance = RearRight.suspensionDistance = OriginalDistance;
            IsOn = false;
        }

        if(IsOn)
        {
            return;
        }

        if(Input.GetAxis("HorizontalHyd") < 0)
        {
            FrontLeft.suspensionDistance = RearLeft.suspensionDistance = LowDistance;
        }
        else if(Input.GetAxis("HorizontalHyd") > 0)
        {
            FrontRight.suspensionDistance = RearRight.suspensionDistance = LowDistance;
        }
        else if (Input.GetAxis("VerticalHyd") > 0)
        {
            FrontRight.suspensionDistance = FrontLeft.suspensionDistance = LowDistance;
        }
        else if (Input.GetAxis("VerticalHyd") < 0)
        {
            RearRight.suspensionDistance = RearLeft.suspensionDistance = LowDistance;
        }
        else
        {
            FrontLeft.suspensionDistance = FrontRight.suspensionDistance = RearLeft.suspensionDistance = RearRight.suspensionDistance = OriginalDistance;
        }
    }
}
