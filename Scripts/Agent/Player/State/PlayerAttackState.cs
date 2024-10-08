using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        _player.MovementCompo.CanMove = false;
        _player.MovementCompo.StopImmediately();
        base.Enter();
    }

    public override void UpdateState()
    {
        if(_endTriggerCalled)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _player.MovementCompo.CanMove = true;
        base.Exit();
    }

    public override void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
