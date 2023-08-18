using UnityEngine;

[CreateAssetMenu(fileName = "GameInfoManager", menuName = "ScriptableObjects/GameInfoManager")]
public class GameInfoManager : ScriptableObject
{
    public string songName = null;
    public int currentCarID;
    public Object[] cars;
    public AudioClip[] musicClips;
}
