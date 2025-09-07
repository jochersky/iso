using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // References
    private CharacterController _characterController;
    private PlayerInput _playerInput;

    // Player input values

    // Movement Property Variables


    // State Variables
    PlayerBaseState _currentState;
    PlayerStateDictionary _states;

    // Getters and Setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public PlayerStateDictionary States { get { return _states; } set { _states = value; } }

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

    }

    void Update()
    {
        _currentState.UpdateStates();
        // _characterController.Move(movement * Time.deltaTime);
    }

    void OnEnable()
    {
      // enable the character controls action map
      _playerInput.Player.Enable();
    }


    void OnDisable()
    {
      // disable the character controls action map
      _playerInput.Player.Disable();
    }
}
