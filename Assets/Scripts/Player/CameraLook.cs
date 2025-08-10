using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] GameObject playerOrientation;
    [SerializeField] float xRotation = 33f;
    [SerializeField] float mouseSensitivity = 15f;
    [SerializeField] float zoomSensitivity = 2f;
    [SerializeField] float maxZoom = 22.5f;
    [SerializeField] float minZoom = 1f;

    Camera cam;

    float _yRotation;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        float zoomValue = cam.orthographicSize - Input.mouseScrollDelta.y * zoomSensitivity;
        cam.orthographicSize = Mathf.Clamp(zoomValue, minZoom, maxZoom);

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity;
        _yRotation += mouseX;
        transform.rotation = Quaternion.Euler(xRotation, _yRotation, 0); // Rotate cam
        playerOrientation.transform.rotation = Quaternion.Euler(0, _yRotation, 0); // Rotate player's movement orientation
    }
}
