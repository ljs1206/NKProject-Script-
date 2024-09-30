using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void UpdateState()
    {

        if(!_player.MovementCompo.IsGround && _player.MovementCompo.Velocity.y <= 0){
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }   
        if(_player.MovementCompo.Velocity.y == 0){
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Enter()
    {
        base.Enter();
        _player.Jump();
        _player.InputReder.OnAttackEvent += HandleAttackEvent;
    }

    public override void Exit()
    {
        base.Exit();
        _player.InputReder.OnAttackEvent -= HandleAttackEvent;
    }

    private void HandleAttackEvent()
    {
        _stateMachine.ChangeState(PlayerStateEnum.JumpAttack);
    }
}
