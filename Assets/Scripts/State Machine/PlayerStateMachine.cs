using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // State Variables
    PlayerBaseState _currentState;
    PlayerStateDictionary _states;

    // Getters and Setters
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; }} 
    public PlayerStateDictionary States{ get { return _states; } set { _states = value; }} 

    void Start()
    {

    }

    void Update()
    {
        
    }
}
