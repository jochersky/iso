using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Movement characterMover;

    private InputAction _moveAction;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 movementVector = _moveAction.ReadValue<Vector2>();
        characterMover.Move(movementVector);
    }
}
