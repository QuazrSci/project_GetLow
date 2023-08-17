using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelectionScript : MonoBehaviour
{
    [SerializeField]
    private GameInfoManager gameInfoMngr;
    [SerializeField]
    private TextMeshProUGUI carName;
    private Transform carSelectionObj;
    [SerializeField]
    private Button btnRight;
    [SerializeField]
    private Button btnLeft;
    private Transform[] cars;
    private int current;

    [SerializeField]
    private int disBetween;

    void Awake()
    {
        carSelectionObj = transform;
        cars = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++) cars[i] = transform.GetChild(i);
    }

    void Start()
    {
        int min = -(cars.Length / 2 * disBetween);
        for(int i = 0; i < cars.Length; i++)
        {
            cars[i].position = new Vector3(min + (disBetween*i), transform.position.y, transform.position.z);
        }

        current = 0;
        gameInfoMngr.currentCarID = current;
        carName.text = cars[current].name;
        carSelectionObj.position = new Vector3(-min, transform.position.y,transform.position.z);
        btnRight.onClick.AddListener(delegate{StartCoroutine(MoveCar(1));});
        btnLeft.onClick.AddListener(delegate{StartCoroutine(MoveCar(-1));});
    }

    IEnumerator MoveCar(int dir)
    {
        if((current + dir) != -1 && current+dir < cars.Length)
        {
            current = current + dir;
            carName.text = cars[current].name;
            gameInfoMngr.currentCarID = current;
            carSelectionObj.position = new Vector3(carSelectionObj.position.x - (dir*disBetween), carSelectionObj.position.y, carSelectionObj.position.z);  
        }
        yield return null;
    }
}
