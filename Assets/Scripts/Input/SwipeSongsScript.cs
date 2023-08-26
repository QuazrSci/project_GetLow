using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private InputSys input;
    [SerializeField] private Transform songsFolder;
    [SerializeField] private Transform ActiveZone;
    private Transform[] btns;

    private float deadZone = 5f;

    private void Awake()
    {
        input = new InputSys();
        input.Enable();

        btns = new Transform[songsFolder.childCount];
        for(int i=0; i<songsFolder.childCount; i++)
        {
            btns[i] = songsFolder.GetChild(i);
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start() {
        input.MainAction.Touch.started += ctx => OnTouch(ctx);
    }

    async void OnTouch(InputAction.CallbackContext context)
    {
        while(input.MainAction.Touch.IsInProgress())
        {
            float deltaY = input.MainAction.TouchPos.ReadValue<Vector2>().y;
            Debug.Log(input.MainAction.TouchPos.ReadValue<Vector2>().y);
            if(Mathf.Abs(deltaY) > Mathf.Abs(deadZone))
            {
                songsFolder.position = new Vector3(
                    songsFolder.position.x, 
                    songsFolder.position.y + deltaY,
                    songsFolder.position.z
                    );
            } 
            await Task.Yield();
        }
        CheckActiveBtn();
        Debug.Log("stopped touch");
    }

    void CheckActiveBtn()
    {
        int ActiveBtn = -1;
        float ActiveBtnPosY = 999;
        for(int i=0; i<btns.Length; i++) // find button
        {
            float Distance = Mathf.Abs(btns[i].position.y) - Mathf.Abs(ActiveZone.position.y);
            if(Distance < ActiveBtnPosY)
            {
                ActiveBtn = i;
                ActiveBtnPosY = Distance;
            }
        }
        if(ActiveBtn==-1) {Debug.LogError("Not Found Active Button!");} //throw error


    }
}
