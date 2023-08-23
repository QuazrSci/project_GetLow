using UnityEngine;
using Cinemachine;

public class CameraPathScript : MonoBehaviour
{
    [SerializeField] private float speedMult = 0.3f;
    CinemachineTrackedDolly trackDolly;

    private void Start() {
        trackDolly = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void FixedUpdate()
    {
        trackDolly.m_PathPosition += Time.deltaTime * speedMult;
    }
}
