using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float MaxMoveSpeed = 1f;
    [SerializeField] float MoveAccel = 0.5f;
    [SerializeField] float StopDrag = 0.6f;
    [SerializeField] float Gravity = -9.8f;
    private float MoveDrag;

    [Header("References")]
    [SerializeField] GameObject orientation;
    [SerializeField] StateSelect stateSelect;
    private CharacterController _characterController;

    private Vector3 _moveVelocity;
    private Vector3 _verticalVelocity;
    private float currentSpeed;
    bool playEnabled = true;

    private void Awake()
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

    private void FixedUpdate()
    {
        ApplyGravity();    
    }

    public void Move(Vector2 moveInputVec)
    {
        // Ensure the player is in the play state before handling movement
        if (!playEnabled) return;

        if (moveInputVec != Vector2.zero)
        {
            ApplyMove(moveInputVec);
        }
        else
        {
            ApplyStopDrag();
        }

        ApplyGravity();

        Vector3 finalMove = _moveVelocity + _verticalVelocity;
        _characterController.Move(finalMove * Time.deltaTime);
    }

    private void ApplyMove(Vector2 moveInputVec)
    {
        Vector3 moveDir = orientation.transform.forward * moveInputVec.y + orientation.transform.right * moveInputVec.x;
        currentSpeed += MoveAccel * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, MaxMoveSpeed);
        _moveVelocity = currentSpeed * moveDir;
    }

    private void ApplyStopDrag()
    {
        currentSpeed = 0;
        _moveVelocity.x *= StopDrag;
        _moveVelocity.z *= StopDrag;
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded)
        {
            // Need to have small amount of gravity even when on ground for character controller.
            _verticalVelocity.y = -0.05f;
        }
        else
        {
            _verticalVelocity.y += Gravity * Time.deltaTime;
        }
    }

    private void ChangePlayState()
    {
        playEnabled = !playEnabled;
    }
}
