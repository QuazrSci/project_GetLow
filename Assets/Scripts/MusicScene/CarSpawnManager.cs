using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    [SerializeField]
    GameInfoManager gameinfoMngr;

    void Awake()
    {
        //if the car isn't spawning = the problem is with the gameinfomanager (the current car is not assgined to gameinfomanager.cars)
        GameObject car = Instantiate(gameinfoMngr.cars[gameinfoMngr.currentCarID], transform.position, transform.rotation, transform) as GameObject;
    }
}
