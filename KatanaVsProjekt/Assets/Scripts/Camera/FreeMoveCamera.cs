using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FreeMoveCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float panSpeed = 20f;
    
    private Camera cam;
    private Vector3 lastPanPosition;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake() => cam = Camera.main;
    
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        MoveCamera();
        RotateCamera();
        PanCamera();

        if (Input.GetKeyDown(KeyCode.X))
        {
            ResetCamera();
        }
    }

    private void MoveCamera()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * moveSpeed;
        move = Vector3.ClampMagnitude(move, moveSpeed);
        move *= Time.deltaTime;
        cam.transform.Translate(move);
    }

    private void RotateCamera()
    {
        if (!Input.GetMouseButton(1)) 
            return;
        
        var yaw = Input.GetAxis("Mouse X");
        var pitch = Input.GetAxis("Mouse Y");
        var rotateValue = new Vector3(pitch, -yaw, 0) * rotationSpeed;
        cam.transform.eulerAngles -= rotateValue;
    }
    
    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
            lastPanPosition = Input.mousePosition;
        
        if (Input.GetMouseButton(2))
        {
            var offset = cam.ScreenToViewportPoint(lastPanPosition - Input.mousePosition);
            var move = new Vector3(offset.x, offset.y, 0) * panSpeed;
            cam.transform.Translate(move, Space.Self);
            lastPanPosition = Input.mousePosition;
        }
    }

    private void ResetCamera()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
