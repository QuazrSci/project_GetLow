using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CarSelectionScript : MonoBehaviour
{
    [SerializeField]
    private GameInfoManager gameInfoMngr;
    [SerializeField]
    private TextMeshProUGUI carName;
    [SerializeField]
    private Button btnRight;
    [SerializeField]
    private Button btnLeft;
    private Transform[] cars;
    private float[] car_position;
    private int current;

    [SerializeField]
    private int disBetween;
    [SerializeField]
    private AnimationCurve lerpCurve;

    void Awake()
    {
        cars = new Transform[transform.childCount];
        car_position = new float[transform.childCount];
        for(int i = 0; i < transform.childCount; i++) cars[i] = transform.GetChild(i);
    }

    void Start()
    {
        int min = -(cars.Length / 2 * disBetween);
        for(int i = 0; i < cars.Length; i++)
        {
            car_position[i] = min + (disBetween*i);
            cars[i].position = new Vector3(min + (disBetween*i), transform.position.y, transform.position.z);
        }

        current = 0;
        gameInfoMngr.currentCarID = current;
        carName.text = cars[current].name;
        transform.position = new Vector3(-min, transform.position.y,transform.position.z);
        btnRight.onClick.AddListener(delegate{startChange(1);});
        btnLeft.onClick.AddListener(delegate{startChange(-1);});
    }

    void startChange(int dir)
    {
        //stop moving
        if(is_changing) { StopCoroutine(MoveCar(dir)); StopCoroutine(MoveCar(-dir)); }
        //start moving
        StartCoroutine(MoveCar(dir));
    }

    bool is_changing;
    IEnumerator MoveCar(int dir)
    {
        if((current + dir) != -1 && current+dir < cars.Length)
        {
            is_changing = true;
            current = current + dir;
            carName.text = cars[current].name;
            gameInfoMngr.currentCarID = current;
            float cur_x_pos = transform.position.x;
            float timeElapsed = Mathf.InverseLerp(cur_x_pos,-car_position[current], transform.position.x);
            float t=0;
            while(timeElapsed < 1)
            {
                timeElapsed += Time.deltaTime;
                t = lerpCurve.Evaluate(timeElapsed);
                transform.position = new Vector3(
                    Mathf.Lerp(cur_x_pos, -car_position[current], t),
                    transform.position.y, 
                    transform.position.z
                );
                yield return new WaitForEndOfFrame();
            }
            
            is_changing = false;
        }
        yield return null;
    }
}
