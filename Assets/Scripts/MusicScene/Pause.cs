using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private Button btn;
    private Button btnResume;
    [SerializeField] private CanvasGroup pauseScreen;
    [SerializeField] AnimationCurve AppearCurve;
    [SerializeField] private Countdown countdownObj;

    private float appearDuration = 0.4f;

    void Start()
    {
        Time.timeScale=1;
        btn = GetComponent<Button>();
        btnResume = pauseScreen.transform.Find("Resume").GetComponent<Button>();
        btn.onClick.AddListener(delegate{Appear(0,1);});
        btnResume.onClick.AddListener(delegate{Appear(1,0);});

        pauseScreen.gameObject.SetActive(false);
        pauseScreen.alpha = 0;
    }

    async void Appear(float from, float to)
    {
        if(from == 0) //pause
        {
            pauseScreen.gameObject.SetActive(true);
            btn.interactable=false;
            chngTimeScale(1,0);
        }

        float timeElapsed = 0f;
        while (timeElapsed < appearDuration)
        {
            float t = timeElapsed / appearDuration;
            t = AppearCurve.Evaluate(t);
            pauseScreen.alpha = Mathf.Lerp(from,to, t);
            timeElapsed += Time.unscaledDeltaTime;
            await Task.Yield();
        }
        pauseScreen.alpha = to;

        if(to==0) //resume
        {
            pauseScreen.gameObject.SetActive(false); 
            btn.interactable=true;
            countdownObj.countdown(3);
            await Task.Delay(3000);
            chngTimeScale(0,1);
        }
    }

    async void chngTimeScale(int from, int to)
    {
        float timeElapsed = 0f;
        float duration = 0.2f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = AppearCurve.Evaluate(t);

            Time.timeScale = Mathf.Lerp(from,to, t);
            timeElapsed += Time.unscaledDeltaTime;
            await Task.Yield();
        }
        if(to==0) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
