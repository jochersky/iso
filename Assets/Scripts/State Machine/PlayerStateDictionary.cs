using System.Collections.Generic;

enum PlayerStates
{
  // - Root States -
  levelEdit,
  grounded,
  fall,
  // - Sub States -
  idle,
  walk,
}

public class PlayerStateDictionary
{
  PlayerStateMachine _context;
  Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

  public PlayerStateDictionary(PlayerStateMachine currentContext)
  {
    _context = currentContext;

    _states[PlayerStates.levelEdit] = new PlayerLevelEditState(_context, this);
    _states[PlayerStates.grounded] = new PlayerGroundedState(_context, this);
    _states[PlayerStates.fall] = new PlayerFallState(_context, this);

    _states[PlayerStates.idle] = new PlayerIdleState(_context, this);
    _states[PlayerStates.walk] = new PlayerWalkState(_context, this);
  }

  // - Root States -
  public PlayerBaseState LevelEdit()
  {
    return _states[PlayerStates.levelEdit];
  }
  public PlayerBaseState Grounded()
  {
    return _states[PlayerStates.grounded];
  }

  public PlayerBaseState Fall()
  {
    return _states[PlayerStates.fall];
  }

  // - Sub States -
  public PlayerBaseState Idle()
  {
    return _states[PlayerStates.idle];
  }

  public PlayerBaseState Walk()
  {
    return _states[PlayerStates.walk];
  }
}