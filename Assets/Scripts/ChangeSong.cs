using UnityEngine;
using UnityEngine.UI;

public class ChangeSong : MonoBehaviour
{
    Button btn;
    [SerializeField]
    private GameInfoManager gameInfoMngr;
    [SerializeField]
    private string song;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(chngSong);
    }

    void chngSong()
    {
        gameInfoMngr.songName = song;
    }
  
}
