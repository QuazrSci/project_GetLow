using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    //DONT USE INSTANCE IN AWAKE FUNCTION (in other scripts)

    [SerializeField]
    private TextAsset MusicProfile;
    [SerializeField]
    private Sprite[] buttonSprites; // List of sprites to use for the buttons
    public GameObject[] triggers;
    [SerializeField]
    private ButtonMovement[] triggr_mov;
    [SerializeField]
    private Image[] triggr_img;

    public TextMeshProUGUI messageObj;
    public AnimationCurve lerpCurve;

    public float triggrs_speed = 10;
    string[] lines;

    void Awake()
    {
        instance = this;
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        //deactivates all the triggers
        foreach(GameObject trigger in triggers) trigger.GetComponent<ButtonMovement>().isMoving = false;

        messageObj.alpha = 0;
    }

    void Start() //read the music profile
    {
        for (int i = 0; i <= triggers.Length-1; i++)
        {
            triggr_mov[i] = triggers[i].GetComponent<ButtonMovement>();
            triggr_img[i] = triggers[i].GetComponent<Image>();
        }

        lines = File.ReadAllLines(Application.dataPath + "/Music Profiles/" + MusicProfile.name + ".txt");
        
        //foreach (string line in lines) { Debug.Log(line); }

        StartCoroutine(Music());
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

    IEnumerator Music()
    {
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
                for (int a=0; a < triggers.Length;a++)
                {
                    if(triggr_mov[a].is_recyclable)
                    {
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
                if(is_found == false) { Debug.LogError("Recyclable Trigger is not found"); }

                //if, theres is no "recyclable trigger" ?

            }
            //if line is a time
            else if (line_int > 0 && line_int < 10)
            {
                yield return new WaitForSecondsRealtime(line_int);
            }
            else Debug.LogError("Something is wrong with music profile!");
            i++;
        }
        Debug.Log("!! MUSIC IS OVER !!");
        yield return 0;
    }
}
