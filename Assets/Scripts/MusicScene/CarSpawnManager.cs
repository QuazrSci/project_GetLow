using UnityEngine;

public class CarSpawnManager : MonoBehaviour
{
    [SerializeField]
    GameInfoManager gameinfoMngr;

    void Start()
    {
        GameObject car = Instantiate(gameinfoMngr.cars[gameinfoMngr.currentCarID], transform.position, transform.rotation) as GameObject;
        
    }
}
