using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private GameInfoManager gameInfoMngr;
    private Slider slider;
    private float musicTime = 0;
    private string[] lines;

    private void Start() 
    {
        gameInfoMngr = MusicManager.instance.gameInfoMngr;
        slider = GetComponent<Slider>();
        lines = File.ReadAllLines(Application.dataPath + "/Music Profiles/" + gameInfoMngr.songName + ".txt");

        float line_int;
        int i = 0;
        while (lines.Length-1 >= i)
        {
            if(float.TryParse(lines[i], out line_int) == false) line_int = 0; //if line isn't a number

            if (line_int > 0 && line_int < 10)
            {
                musicTime += line_int;
            }
            i++;
        }

        slider.value = 0;
        slider.maxValue = musicTime;
    }

    public IEnumerator StartCountdown()
    {
        while(slider.value < slider.maxValue)
        {
            slider.value += Time.deltaTime;
            yield return null;
        }
    }
}
