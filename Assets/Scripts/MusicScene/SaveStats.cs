using UnityEngine;
using CI.QuickSave;

public class SaveStats : MonoBehaviour
{
    public static SaveStats instance { get; private set; }

    void Awake()
    {
        instance = this;
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    public void saveinfo(string song,int points, int missed, int ok, int good, int amazing, int god)
    {
        QuickSaveWriter.Create(song)
                       .Write("points", points)
                       .Write("missed",missed)
                       .Write("ok", ok)
                       .Write("good",good)
                       .Write("amazing",amazing)
                       .Write("god",god)
                       .Commit();
    }
}
