using UnityEngine;
using CI.QuickSave;

public class SongInfoLoad : MonoBehaviour
{
    
    void loadStats(string song)
    {
        int points, missed, ok, good, amazing, god;

        QuickSaveReader.Create(song)
                       .Read<int>("points", (r) => { points = r; })
                       .Read<int>("missed", (r) => { missed = r; })
                       .Read<int>("ok", (r) => { ok = r; })
                       .Read<int>("good", (r) => { good = r; })
                       .Read<int>("amazing", (r) => { amazing = r; })
                       .Read<int>("god", (r) => { god = r; });
        
    }
}
