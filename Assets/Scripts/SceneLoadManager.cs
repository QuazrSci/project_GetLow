using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance {get; private set; }

    GameObject loadIcon;
    CanvasGroup loadScreenCanvas;
    CanvasGroup confirmExitCanvas;
    [SerializeField]
    public TextMeshProUGUI confirm_text;

    public Button confirm_yes;
    public Button confirm_no;

    [SerializeField]
    float minLoadTime = 1f;
    [SerializeField]
    AnimationCurve lerpCurve;
    float fadeTime = 0.3f;
    bool is_done;

    string sceneName;

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
        confirmExitCanvas.alpha = 0;
        loadScreenCanvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        GetComponent<Canvas>().sortingOrder = 2;
        //confirm buttons
        //confirm_yes = confirmExitCanvas.transform.Find("Yes").GetComponent<Button>();
        confirm_yes.onClick.AddListener(LoadScene);
        //confirm_no = confirmExitCanvas.transform.Find("No").GetComponent<Button>();
        confirm_no.onClick.AddListener(ConfirmOff);

        confirmExitCanvas.gameObject.SetActive(false);
    }

    public void ChangeScene(string scene, bool is_confirm, string messg)
    {
        sceneName = scene;

        if (is_confirm)
        {
            confirm_text.text = messg;
            ConfirmOn();
        }
        else
        {
            LoadScene();
        }
    }

    public void ConfirmOn()
    {
        confirmExitCanvas.gameObject.SetActive(true);
        StartCoroutine(ConfirmAlphaLerp(0, 1));
    }

    void ConfirmOff()
    {
        StartCoroutine(ConfirmAlphaLerp(1, 0));
    }

    IEnumerator ConfirmAlphaLerp(float from, float to)
    {
        GetComponent<Canvas>().sortingOrder = 2;
        float timeElapsed = 0f;
        float duration = 0.4f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = lerpCurve.Evaluate(t);

            confirmExitCanvas.alpha = Mathf.Lerp(from, to, t);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        confirmExitCanvas.alpha = to;
        if (to == 0)
        {
            confirmExitCanvas.gameObject.SetActive(false);
            GetComponent<Canvas>().sortingOrder = 0;
        }
    }

    public async void LoadScene()
    {
        Time.timeScale = 1; //timeScale == 1;
        //start loading the scene
        AsyncOperation scene =  SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        is_done = false;
        loadScreenCanvas.gameObject.SetActive(true);
        GetComponent<Canvas>().sortingOrder = 1;
        loadScreenCanvas.alpha = 0;
        StartCoroutine(LoadAniamtion());

        float i=0;
        float alpha=0;
        //Load screen Appear
        while (alpha < 1)
        {
            await Task.Delay(4);
            loadScreenCanvas.alpha = alpha;
            i += Time.unscaledDeltaTime * fadeTime;
            alpha += i;
        }
        loadScreenCanvas.alpha = 1;
        if (confirmExitCanvas.gameObject.activeInHierarchy) confirmExitCanvas.gameObject.SetActive(false);

        //allow to load up scene
        scene.allowSceneActivation = true;


        //wait until the scene is loaded
        while (scene.progress < 0.9f)
        {
            await Task.Delay(300);
        }

        i=0;
        alpha = 1;
        //Load screen Fade
        while (loadScreenCanvas.alpha > 0)
        {
            await Task.Delay(4);
            loadScreenCanvas.alpha = alpha;
            i += Time.unscaledDeltaTime * fadeTime;
            alpha -= i;
        }

        loadScreenCanvas.alpha = 0;
        loadScreenCanvas.gameObject.SetActive(false);
        GetComponent<Canvas>().sortingOrder = 0;
        is_done = true;
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
        } while (duration > 0 && !is_done);
    }
}
