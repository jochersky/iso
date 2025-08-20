using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [SerializeField] StateSelect stateSelect;
    [SerializeField] GameObject playerOrientation;
    [SerializeField] GameObject flatCameraOrientation;
    [SerializeField] float xRotation = 33f;
    [SerializeField] float mouseSensitivity = 5f;
    [SerializeField] float zoomSensitivity = 2f;
    [SerializeField] float maxZoom = 30f;
    [SerializeField] float minZoom = 1f;
    [SerializeField] float moveSpeed = 0.5f;

    Camera cam;

    float _yRotation;
    float radius = 50f;
    float _zoom = 20f;
    bool _rotateCam;
    bool playEnabled = true;

    Vector3 moveVector;

    private void Start()
    {
        cam = GetComponent<Camera>();
        // transform.position = playerOrientation.transform.position - (transform.forward * radius) + Vector3.up;
    }

    private void OnEnable()
    {
        stateSelect.togglePressed += ChangePlayState;
    }

    private void OnDisable()
    {
        stateSelect.togglePressed -= ChangePlayState;
    }

    private void Update()
    {
        ZoomCamera();
        RotateCamera();

        // Ensure no independent camera movement when player is in play mode
        if (playEnabled)
        {
            transform.position = playerOrientation.transform.position - (transform.forward * radius) + Vector3.up;
            return;
        }

        GetInput();
    }

  private void FixedUpdate()
    {
        // Ensure no independent camera movement when player is in play mode
        if (playEnabled) return;

        MoveCamera();
    }

    private void ZoomCamera()
    {
        float newZoom = _zoom - Input.mouseScrollDelta.y * zoomSensitivity;
        _zoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
        cam.orthographicSize = _zoom;
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButtonDown(1)) _rotateCam = true;
        if (Input.GetMouseButtonUp(1)) _rotateCam = false;

        if (_rotateCam)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            transform.RotateAround(playerOrientation.transform.position, Vector3.up, mouseX); // Rotate camera around player orienation

            playerOrientation.transform.RotateAround(playerOrientation.transform.position, Vector3.up, mouseX); // Rotate player's movement orientation
        }
    }

    void GetInput()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");
    }

    private void MoveCamera()
    {
        // Set the move direction to be aligned with how the camera is rotated.
        moveVector = playerOrientation.transform.right * moveVector.x + playerOrientation.transform.forward * moveVector.y;
        moveVector.y = 0f;
        moveVector = moveVector.normalized;

        transform.position += moveVector;
    }

    private void ChangePlayState()
    {
        playEnabled = !playEnabled;
        transform.SetPositionAndRotation(playerOrientation.transform.position, transform.rotation);
    }
}
