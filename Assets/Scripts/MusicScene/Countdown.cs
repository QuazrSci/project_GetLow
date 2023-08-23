using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    
    private TextMeshProUGUI textObj;
    [SerializeField] private AnimationCurve lerpCurve;
    private float minSize = 0.3f;
    private float maxSize = 3f;

    void Start()
    {
        textObj = GetComponent<TextMeshProUGUI>();
        textObj.alpha = 0f;
        StartCoroutine(countdown(MusicManager.instance.musicDelayMilSec / 1000));
    }

    public IEnumerator countdown(float time)
    {   
        //countdown delay
        yield return new WaitForSecondsRealtime(time/4);

        //countdown start
        StartCoroutine(TextAnimAlpha("3",time/4));
        StartCoroutine(TextAnimSize(time/4));

        yield return new WaitForSecondsRealtime(time/4);

        StartCoroutine(TextAnimAlpha("2",time/4));
        StartCoroutine(TextAnimSize(time/4));

        yield return new WaitForSecondsRealtime(time/4);

        StartCoroutine(TextAnimAlpha("1",time/4));
        StartCoroutine(TextAnimSize(time/4));
    }

    IEnumerator TextAnimAlpha(string text, float duration)
    {
        textObj.text = text;

        // appear
        float timeElapsed = 0f;
        while (timeElapsed < (duration/2))
        {
            float t = timeElapsed / (duration/2);
            t = lerpCurve.Evaluate(t);

            textObj.alpha = Mathf.Lerp(0,1f, t);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // disappear
        timeElapsed = 0f;
        while (timeElapsed < (duration/2))
        {
            float t = timeElapsed / (duration/2);
            t = lerpCurve.Evaluate(t);

            textObj.alpha = Mathf.Lerp(1f,0f, t);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
    IEnumerator TextAnimSize(float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = lerpCurve.Evaluate(t);

            float size = Mathf.Lerp(minSize,maxSize, t);
            textObj.transform.localScale = new Vector3 (size,size,size);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
