using System;
using UnityEditor;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private SaveStats saveStats;

    [NonSerialized] public int pointBank;
    [NonSerialized] public int count_missed;
    [NonSerialized] public int count_ok;
    [NonSerialized] public int count_good;
    [NonSerialized] public int count_amazing;
    [NonSerialized] public int count_god;
    
    void Start()
    {
        saveStats = SaveStats.instance;
        count_missed = 0;
        pointBank = 0;
        count_ok = 0;
        count_good = 0;
        count_amazing = 0;
        count_god = 0;
    }

    public void AddPoints(int points, string quality)
    {
        pointBank += points;
        switch(quality)
        {
            case "missed":
                count_missed++;
                break;
            case "ok":
                count_ok++;
                break;
            case "good":
                count_good++;
                break;
            case "amazing":
                count_amazing++;
                break;
            case "god":
                count_god++;
                break;
            default:
                Debug.LogError("Misspeled quality in the addPoint() function");
                break;
        }
    }

    public void SaveSongStats(string song)
    {
        saveStats.saveinfo(song, pointBank,count_missed,count_ok,count_good,count_amazing,count_god);
    }
}
