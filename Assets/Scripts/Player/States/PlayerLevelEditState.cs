using System;
using UnityEngine;

public class PlayerLevelEditState : PlayerBaseState
{
  public PlayerLevelEditState(PlayerStateMachine currentContext, PlayerStateDictionary playerStateDictionary)
  : base(currentContext, playerStateDictionary)
  {
  }

  public override void EnterState()
  {
    HandleGravity();
    Context.MoveVelocity = Vector3.zero;
  }

  public override void ExitState()
  {
  }

  public override void InitializeSubState()
  {
  }

  public override void UpdateState()
  {
    if (Context.PlayEnabled)
    {
      SwitchState(Dictionary.LevelEdit());
    }
  }

  public void HandleGravity()
  {
    Context.VerticalVelocityY = 0;
  }
}