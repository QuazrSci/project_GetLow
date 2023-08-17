using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    Button btn;
    SceneLoadManager loadMngr;
    [SerializeField]
    private string message;

    private void Awake() {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(startExit);
    }

    private void Start() {
        loadMngr = SceneLoadManager.instance;
    }

    public void startExit()
    {
        loadMngr.confirm_yes.onClick.RemoveAllListeners();
        loadMngr.confirm_yes.onClick.AddListener(ExitGame);
        loadMngr.confirm_no.onClick.AddListener(ExitCancel);
        
        loadMngr.confirm_text.text = message;

        loadMngr.ConfirmOn();
    }

    void ExitCancel()
    {
        loadMngr.confirm_yes.onClick.RemoveAllListeners();
        loadMngr.confirm_yes.onClick.AddListener(loadMngr.LoadScene);
        loadMngr.confirm_no.onClick.RemoveListener(ExitCancel);
    }

    void ExitGame()
    {
        Debug.Log("Application is quited!");
        Application.Quit();
    }
}
