using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CMFreeLookCameraInput : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;

    private void Awake()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    private void Start()
    {
        freeLookCamera.m_XAxis.m_InputAxisName = "";
        freeLookCamera.m_YAxis.m_InputAxisName = "";
    }
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            freeLookCamera.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            freeLookCamera.m_YAxis.m_InputAxisValue = 0;
        }
    }
}
