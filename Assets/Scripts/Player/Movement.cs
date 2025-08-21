using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject orientation;
    [SerializeField] StateSelect stateSelect;

    private CharacterController _characterController;

    [SerializeField] float MovementSpeed = 10f;
    [SerializeField] float Gravity = -30f;

    private float _rotationY;
    private float _verticalVelocity;

    bool playEnabled = true;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        stateSelect.togglePressed += ChangePlayState;
    }

    private void OnDisable()
    {
        stateSelect.togglePressed -= ChangePlayState;
    }

    public void Move(Vector2 movementVector)
    {
        // Ensure the player is in the play state before handling movement
        if (!playEnabled) return;

        Vector3 move = orientation.transform.forward * movementVector.y + orientation.transform.right * movementVector.x;
        move = move * MovementSpeed * Time.deltaTime;
        _characterController.Move(move);

        _verticalVelocity += Gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
    }

    private void ChangePlayState()
    {
        playEnabled = !playEnabled;
    }
}
