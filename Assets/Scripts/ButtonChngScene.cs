using UnityEngine;
using UnityEngine.UI;

public class ButtonChngScene : MonoBehaviour
{
    Button buttn;
    [SerializeField] string scene;
    [SerializeField] bool is_confirm = false;
    [SerializeField] string message;

    void Start()
    {
        buttn = GetComponent<Button>();
        buttn.onClick.AddListener(changeScene);
    }

    void changeScene()
    {
        SceneLoadManager.instance.ChangeScene(scene, is_confirm, message);
    }
}
