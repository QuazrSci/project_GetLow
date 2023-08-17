using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptCircleScrpt : MonoBehaviour
{
    public static AcceptCircleScrpt instance { get; private set; }
    [SerializeField]
    private GameObject[] triggr;
    private PointsManager pointsMngr;

    public AnimationCurve lerpCurve;
    Vector3 origScale;

    private void Awake()
    {
        instance = this;
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        triggr = new GameObject[MusicManager.instance.triggers.Length];
        triggr = MusicManager.instance.triggers;
        origScale = transform.localScale;
        pointsMngr = MusicManager.instance.GetComponent<PointsManager>();
    }

    public void AcceptTrigger(string InputDirection)
    {
        int triggrNum = -1;
        float xDis = 2000;
        int i;
        for(i=0; i < triggr.Length;i++) //find the nearest trigger
        {
            if(triggr[i].transform.position.x - transform.position.x < xDis && triggr[i].GetComponent<ButtonMovement>().is_active)
            {
                xDis = triggr[i].transform.position.x - transform.position.x;
                triggrNum = i;
            }
        }
        if(triggrNum!=-1 && xDis < transform.position.x)
        {
            Debug.Log(xDis + " | " + transform.position.x + "|" + triggr[triggrNum].transform.position.x + "||i=" + triggrNum);
            triggr[triggrNum].GetComponent<ButtonMovement>().is_active = false;

            if (xDis < -50 || xDis > 50 || triggr[triggrNum].GetComponent<ButtonMovement>().direction != InputDirection)
            { MusicManager.instance.Message("missed", 50, 50, 50); pointsMngr.AddPoints(0, "missed"); }
            else if (xDis < -40 || xDis > 40) 
            { MusicManager.instance.Message("Ok", 0, 200, 255); pointsMngr.AddPoints(10, "ok"); }
            else if (xDis < -20 || xDis > 20) 
            { MusicManager.instance.Message("Good!", 0, 255, 50); pointsMngr.AddPoints(20, "good"); }
            else if (xDis < -4 || xDis > 4) 
            { MusicManager.instance.Message("Amazing!", 220, 0, 255); pointsMngr.AddPoints(30, "amazing"); }
            else { MusicManager.instance.Message("God!", 255, 200, 0); pointsMngr.AddPoints(50, "god"); }
        } 
        else MusicManager.instance.Message("missed", 50, 50, 50);


        StopCoroutine(AcceptEffect());
        StartCoroutine(AcceptEffect());
        
    }

    IEnumerator AcceptEffect()
    {
        float timeElapsed = 0f;
        float duration = 0.25f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed /duration ;
            t = lerpCurve.Evaluate(t);

            transform.localScale = Vector3.Lerp(origScale, Vector3.zero, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = origScale;
    }
}
