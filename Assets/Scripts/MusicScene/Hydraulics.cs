using System.Collections;
using UnityEngine;

public class Hydraulics : MonoBehaviour
{
    private Transform[] wheels;
    private Transform[] hooks;
    private Transform car_body;
    
    [SerializeField] private float maxDistance = 0.2f;
    [SerializeField] private float JumpHeight = 0.9f;
    [SerializeField] private float jump_speed = 2f;
    [SerializeField] private AnimationCurve JumpCurve;

    private float wheel_min_y;

    private void Start() 
    {  
        transform.position = Vector3.zero;
        car_body = transform.GetChild(0).transform.Find("Body");
        wheels = new Transform[4];
        hooks = new Transform[4];
        for(int i=0; i < 4; i++)
        {
            wheels[i] = transform.GetChild(0).transform.Find("Wheels").GetChild(i);
            hooks[i] = transform.GetChild(0).transform.Find("Body").transform.Find("Hooks").GetChild(i);
            hooks[i].position = wheels[i].position; //hook the hooks to the wheels
            hooks[i].GetComponent<FixedJoint>().connectedBody = car_body.GetComponent<Rigidbody>(); // connect rigidbody to hooks
        }
        wheel_min_y = wheels[0].position.y;
    }

    void Update()
    {
        //check if body is in the bounds of the area
        car_body.position = new Vector3(0,car_body.position.y,0);
        car_body.localEulerAngles = new Vector3(car_body.localEulerAngles.x,0,car_body.localEulerAngles.z);

        //check if all the wheels don't exit the maxDistance
        for(int i = 0; i < 4; i++)
        {
            float y_pos = hooks[i].position.y - maxDistance;
            y_pos = Mathf.Clamp(y_pos,wheel_min_y,5);
            //rotation
            wheels[i].localEulerAngles = new Vector3(wheels[i].rotation.x,180 + car_body.localEulerAngles.y, wheels[i].rotation.z);
            //position
            wheels[i].position = new Vector3(hooks[i].position.x, y_pos, hooks[i].position.z);
        }
    
    }

    public void wheelJump(string direction)
    {
        switch(direction)
        {
            case "up": StartCoroutine(jump(hooks[0])); break;
            case "down": StartCoroutine(jump(hooks[1])); break;
            case "right": StartCoroutine(jump(hooks[2])); break;
            case "left": StartCoroutine(jump(hooks[3])); break;
            default: Debug.LogError("Theres isn't: "+direction+" direction in wheel jump script"); break;
        }
    }

    IEnumerator jump(Transform hook)
    {   
        float timeElapsed = Mathf.InverseLerp(0, JumpHeight, hook.position.y);
        float t;
        while(timeElapsed < 1)
        {
            timeElapsed += Time.deltaTime * jump_speed;
            t = JumpCurve.Evaluate(timeElapsed);
            hook.position = new Vector3(
                hook.position.x, 
                Mathf.Lerp(0, JumpHeight, t),
                hook.position.z
            );
            yield return new WaitForEndOfFrame();
        }
        
    }

}