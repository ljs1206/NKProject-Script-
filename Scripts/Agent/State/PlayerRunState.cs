using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReder.OnJumpEvent += HandleJumpEvent;
        _player.InputReder.OnAttackEvent += HandleAttackEvent;
    }

    private void HandleAttackEvent()
    {
        if(_player.PlayerMovement.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Attack);
        }
    }

    private void HandleJumpEvent()
    {
        if(_player.PlayerMovement.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.InputReder.Movement.x == 0){
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        else{
            _player.PlayerMovement.Move();
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.InputReder.OnJumpEvent -= HandleJumpEvent;
        _player.InputReder.OnAttackEvent -= HandleAttackEvent;
    }
}
