using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance {get; private set; }

    GameObject loadIcon;
    CanvasGroup loadScreenCanvas;
    CanvasGroup confirmExitCanvas;


    [SerializeField]
    float minLoadTime = 1f;
    [SerializeField]
    AnimationCurve lerpCurve;
    float fadeTime = 0.3f;
    bool is_done;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        loadScreenCanvas = transform.Find("LoadingScreen").GetComponent<CanvasGroup>();
        confirmExitCanvas = transform.Find("ConfirmExit").GetComponent<CanvasGroup>();
        loadIcon = loadScreenCanvas.transform.Find("Load").gameObject;
        loadScreenCanvas.alpha = 0;
        GetComponent<Canvas>().sortingOrder = 0;
    }

    public void ConfirmOn()
    {
        StartCoroutine(ConfirmAlphaLerp(0, 1));
    }

    public void ConfirmOff()
    {
        StartCoroutine(ConfirmAlphaLerp(1, 0));
    }

    IEnumerator ConfirmAlphaLerp(float from, float to)
    {
        float timeElapsed = 0f;
        float duration = 0.4f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = lerpCurve.Evaluate(t);

            confirmExitCanvas.alpha = Mathf.Lerp(from, to, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        confirmExitCanvas.alpha = to;
    }

    public async void LoadScene(string SceneName)
    {
        is_done = false;
        AsyncOperation scene =  SceneManager.LoadSceneAsync(SceneName);
        scene.allowSceneActivation = false;
        GetComponent<Canvas>().sortingOrder = 1;
        loadScreenCanvas.alpha = 0;
        StartCoroutine(LoadAniamtion());
        int i = (int) fadeTime * 1000;
        float incrs =  (i / 5 / 1000);
        //Load screen Appear
        do
        {
            await Task.Delay(555);
            loadScreenCanvas.alpha += incrs;
            i -= 5;
        } while (i > 0);

        do
        {
            await Task.Delay(50);
        } while (scene.progress < 0.9f);

        i = (int) fadeTime * 1000;
        //Load screen Fade
        do
        {
            await Task.Delay(5);
            loadScreenCanvas.alpha -= incrs;
            i -= 5;
        } while (i > 0);

        loadScreenCanvas.alpha = 0;
        GetComponent<Canvas>().sortingOrder = 0;
        is_done = true;
        scene.allowSceneActivation = true;
    }

    IEnumerator LoadAniamtion()
    {
        float duration = minLoadTime;
        do
        {
            loadScreenCanvas.alpha += 0.03f;
            loadIcon.transform.Rotate(new Vector3(0, 0, 5));
            yield return new WaitForFixedUpdate();
            duration -= 0.02f;
        } while (duration > 0 && is_done);
    }
}
