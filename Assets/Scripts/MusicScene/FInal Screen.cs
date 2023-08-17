using System.Collections;
using System;
using UnityEngine;
using TMPro;

public class FinalScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI qualityText;
    private TextMeshProUGUI pointsText;

    private PointsManager pointsMngr;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        qualityText = transform.Find("stats").Find("quality").GetComponent<TextMeshProUGUI>();
        pointsText = transform.Find("stats").Find("points").GetComponent<TextMeshProUGUI>();

        pointsMngr = MusicManager.instance.GetComponent<PointsManager>();
    }

    public void screenPopUp(float mult)
    {
        StartCoroutine(alphaLerp(mult));
    }

    IEnumerator alphaLerp(float mult)
    {
        float lerp = 0;
        while (lerp < 1)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1,lerp);
            lerp += Time.deltaTime*mult;
            yield return new WaitForEndOfFrame();
        }
        canvasGroup.alpha = 1;
        setStats();
    }

    void setStats()
    {
        pointsText.text = Convert.ToString(pointsMngr.pointBank);

        qualityText.text = Convert.ToString(
            pointsMngr.count_missed +"\n"+
            pointsMngr.count_ok + "\n"+
            pointsMngr.count_good + "\n" +
            pointsMngr.count_amazing + "\n" +
            pointsMngr.count_god
            );
    }
}
