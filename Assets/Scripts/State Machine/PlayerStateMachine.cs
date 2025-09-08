using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject _orientation;
    [SerializeField] StateSelect _stateSelect;
    private CharacterController _characterController;
    private PlayerInput _playerInput;

    [Header("Movement Properties")]
    [SerializeField] float _maxMoveSpeed = 1f;
    [SerializeField] float _moveAccel = 0.5f;
    [SerializeField] float _stopDrag = 0.6f;
    [SerializeField] float _gravity = -9.8f;

    // Player input and movement values
    private Vector2 _moveInput;
    private bool _movePressed;
    private Vector3 _moveVelocity;
    private Vector3 _verticalVelocity;
    private float _currentHorizontalSpeed;
    bool playEnabled = true;

    // State Variables
    PlayerBaseState _currentState;
    PlayerStateDictionary _states;

    // Getters and Setters
    public CharacterController CharacterController { get { return _characterController; } } 
    
    public PlayerBaseState CurrentState
    { get { return _currentState; } set { _currentState = value; } }
    public PlayerStateDictionary States { get { return _states; } set { _states = value; } }

    public float MoveAccel { get { return _moveAccel; } }
    public float MaxMoveSpeed { get { return _maxMoveSpeed; } }
    public float StopDrag { get { return _stopDrag; } }
    public float Gravity { get { return _gravity; } }

    public bool MovePressed { get { return _movePressed; } set { _movePressed = value; } }

    public Vector2 MoveInput { get { return _moveInput; } }

    public float MoveVelocityX { get { return _moveVelocity.x; } set { _moveVelocity.x = value; } }
    public float MoveVelocityY { get { return _moveVelocity.y; } set { _moveVelocity.y = value; } }
    public float MoveVelocityZ { get { return _moveVelocity.z; } set { _moveVelocity.z = value; } }
    public Vector3 MoveVelocity { get { return _moveVelocity; } set { _moveVelocity = value; } }
    public float VerticalVelocityY { get { return _verticalVelocity.y; } set { _verticalVelocity.y = value; }}
    public Vector3 VerticalVelocity { get { return _verticalVelocity; } set { _verticalVelocity = value; } }

    public float CurrentHorizontalSpeed { get { return _currentHorizontalSpeed; } set { _currentHorizontalSpeed = value; } }

    public Vector3 ForwardDir { get { return _orientation.transform.forward; } }
    public Vector3 RightDir { get { return _orientation.transform.right; } }

    void Start()
    {
        // Initialize references
        _characterController = GetComponent<CharacterController>();
        _playerInput = new PlayerInput();

        // State machine + initial state setup
        _states = new PlayerStateDictionary(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        // Subscribe the player input callbacks
        _playerInput.Player.Move.started += OnMoveInput;
        _playerInput.Player.Move.canceled += OnMoveInput;
        _playerInput.Player.Move.performed += OnMoveInput;
    }

    void Update()
    {
        _currentState.UpdateStates();
        _characterController.Move((MoveVelocity + _verticalVelocity) * Time.deltaTime);
    }

    void OnEnable()
    {
        // enable the character controls action map
        _playerInput.Player.Enable();
        _stateSelect.togglePressed += ChangePlayState;
    }

    void OnDisable()
    {
        // disable the character controls action map
        _playerInput.Player.Disable();
        _stateSelect.togglePressed -= ChangePlayState;
    }

    // callback handler function to set the player input values
    void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _movePressed = _moveInput != Vector2.zero;
    }

    private void ChangePlayState()
    {
        playEnabled = !playEnabled;
    }
}
