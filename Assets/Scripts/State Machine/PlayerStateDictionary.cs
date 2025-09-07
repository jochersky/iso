using System.Collections.Generic;

enum PlayerStates
{
  // - Sub States -
  idle,
  walk,
  // - Root States -
  grounded,
  fall
}

public class PlayerStateDictionary
{
  PlayerStateMachine _context;
  Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

  public PlayerStateDictionary(PlayerStateMachine currentContext)
  {
    _context = currentContext;
  }

  public PlayerBaseState Grounded()
  {
    return _states[PlayerStates.grounded];
  }
}