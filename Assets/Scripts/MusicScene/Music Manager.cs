using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    //DONT USE INSTANCE IN AWAKE FUNCTION (in other scripts)

    [SerializeField] public GameInfoManager gameInfoMngr;
    [SerializeField] private MusicSlider musicSlider;
    [SerializeField] private Sprite[] buttonSprites; // List of sprites to use for the buttons

    public GameObject[] triggers;
    private ButtonMovement[] triggr_mov;
    private Image[] triggr_img;

    [SerializeField] private FinalScreen finScreen;
    private AudioSource AudioSrc;

    private Transform startPos;
    public TextMeshProUGUI messageObj;
    public AnimationCurve lerpCurve;

    public float triggrs_speed = 10;
    private float resSpeedmult; //multiplier of the speed based on a resolution of the screen
    private string[] lines;
    [SerializeField] public int musicDelayMilSec = 3000;

    void Awake()
    {
        instance = this;
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        //deactivates all the triggers
        foreach(GameObject trigger in triggers) trigger.GetComponent<ButtonMovement>().isMoving = false;

        //find music Clip
        AudioSrc = GetComponent<AudioSource>();
        AudioSrc.volume = 0;

        for(int i=0; i < gameInfoMngr.musicClips.Length; i++)
        {
            if(gameInfoMngr.musicClips[i].name == gameInfoMngr.songName) 
            {
                AudioSrc.clip = gameInfoMngr.musicClips[i];
                AudioSrc.Stop();
            }
        } 
        if(AudioSrc.clip == null) Debug.LogError("SONG ISN'T FOUND! There isn't music with name: " + gameInfoMngr.songName);

        messageObj.alpha = 0;
        resSpeedmult = Screen.currentResolution.width / 400;
        triggrs_speed *= resSpeedmult;
    }

    void Start()
    {
        startPos = triggers[0].GetComponent<ButtonMovement>().startPosition;
        triggr_img = new Image[triggers.Length];
        triggr_mov = new ButtonMovement[triggers.Length];
        for (int i = 0; i <= triggers.Length-1; i++)
        {
            triggr_mov[i] = triggers[i].GetComponent<ButtonMovement>();
            triggr_img[i] = triggers[i].GetComponent<Image>();

            triggers[i].transform.position = startPos.position;
        }
        //read the music profile
        lines = File.ReadAllLines(Application.dataPath + "/Music Profiles/" + gameInfoMngr.songName + ".txt");

        finScreen.gameObject.SetActive(false);

        MusicDelay(musicDelayMilSec);
    }

    public void Message(string text, int r, int g, int b)
    {
        messageObj.text = text;
        messageObj.color = new Color(r,g,b);
        StopCoroutine(AlphaLerp());
        StartCoroutine(AlphaLerp());
    }

    IEnumerator AlphaLerp()
    {
        float timeElapsed = 0f;
        float duration = 0.4f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = lerpCurve.Evaluate(t);

            messageObj.alpha = Mathf.Lerp(1f,0f, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        messageObj.alpha = 0;
    }

    async void MusicDelay(int waitMilSec)
    {
        await Task.Delay(waitMilSec);
        //start music
        StartCoroutine(musicFadeStart());
        StartCoroutine(Music());
        StartCoroutine(musicSlider.StartCountdown());
    }

    IEnumerator Music()
    {
        AudioSrc.Play();

        int i = 0;
        float line_int;
        bool is_found = false;
        while (lines.Length-1 >= i)
        {
            if(float.TryParse(lines[i], out line_int) == false)
            {
                line_int = 0;
            }

            //if line is an arrow
            if (lines[i] == "up" || lines[i] == "down" || lines[i] == "left" || lines[i] == "right")
            {
                is_found = false;
                //trying to find a recyclable trigger
                for (int a = 0; a < triggers.Length; a++)
                {
                    if (triggr_mov[a].is_recyclable)
                    {
                        triggr_mov[a].direction = lines[i];
                        // changing a sprite
                        switch (lines[i])
                        {
                            case "up":
                                triggr_img[a].sprite = buttonSprites[0];
                                break;
                            case "down":
                                triggr_img[a].sprite = buttonSprites[1];
                                break;
                            case "left":
                                triggr_img[a].sprite = buttonSprites[2];
                                break;
                            case "right":
                                triggr_img[a].sprite = buttonSprites[3];
                                break;
                            default:
                                Debug.LogError("Music Profile has a mistake");
                                break;
                        }
                        // trigger starts moving
                        triggr_mov[a].isMoving = true;
                        triggr_mov[a].is_recyclable = false;
                        is_found = true;
                        a = triggers.Length;
                    }
                }
                if (is_found == false) { Debug.LogError("Recyclable Trigger is not found"); }

                //if, theres is no "recyclable trigger" ?

            }
            //if line is = change speed
            else if (lines[i] == "s")
            {
                i++;
                changeSpeed(float.Parse(lines[i]));
            }
            //if line is a time
            else if (line_int > 0 && line_int < 10)
            {
                yield return new WaitForSeconds(line_int);
            }
            else Debug.LogError("Something is wrong with music profile!");
            i++;
        }

        stopMusic();
        yield return 0;
    }


    async void stopMusic()
    {   
        bool is_done = false;
        int count;
        while(!is_done)
        {
            count = 0;
            for(int i=0;i<triggers.Length;i++)
            {
                if(triggr_mov[i].isMoving == false) count++;
            }
            if(count == triggers.Length) is_done = true;
            await Task.Delay(100);
        }

        Debug.Log("!! MUSIC IS OVER !!");
        StartCoroutine(musicFade());
        finScreen.gameObject.SetActive(true);
        finScreen.screenPopUp(1f);
    }

    [SerializeField] float music_fade_min=0.4f;
    IEnumerator musicFade()
    {
        while(AudioSrc.volume > music_fade_min)
        {
            AudioSrc.volume -= Time.deltaTime;
            yield return null;
        }
    }
    
    IEnumerator musicFadeStart()
    {
        while(AudioSrc.volume < 1)
        {
            AudioSrc.volume += Time.deltaTime;
            yield return null;
        }
    }

    void changeSpeed(float speed)
    {
        triggrs_speed = speed*resSpeedmult;
        for (int i = 0; i <= triggers.Length - 1; i++)
        {
            triggr_mov[i].movementSpeed = speed;
        }
    }
}
