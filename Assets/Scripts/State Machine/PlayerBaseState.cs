using UnityEngine;

public abstract class PlayerBaseState
{
  private bool _isRootState = false;
  private PlayerStateMachine _context;
  private PlayerStateDictionary _dictionary;
  private PlayerBaseState _currentSubState;
  private PlayerBaseState _currentSuperState;

  protected bool IsRootState { set { _isRootState = value; } }
  protected PlayerStateMachine Context { get { return _context; } }
  protected PlayerStateDictionary Dictionary { get { return _dictionary; } }

  public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateDictionary playerStateDictionary)
  {
    _context = currentContext;
    _dictionary = playerStateDictionary;
  }

  public abstract void EnterState();
  public abstract void UpdateState();
  public abstract void ExitState();
  public abstract void CheckSwitchStates();
  public abstract void InitializeSubState();

  protected void SwitchState(PlayerBaseState newState)
  {
    ExitState();
    newState.EnterState();

    if (_isRootState)
    {
      _context.CurrentState = newState;
    }
    else if (_currentSuperState != null)
    {
      _currentSuperState.SetSubState(newState);
    }
  }

  public void UpdateStates()
  {
    UpdateState();
    if (_currentSubState != null)
    {
      _currentSubState.UpdateStates();
    }
  }

  protected void SetSuperState(PlayerBaseState newSuperState)
  {
    _currentSuperState = newSuperState;
  }

  protected void SetSubState(PlayerBaseState newSubState){
      _currentSubState = newSubState;
      newSubState.SetSuperState(this);
  }
}