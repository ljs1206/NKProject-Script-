using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReder.OnAttackEvent += HandleAttackEvent;
        _player.InputReder.OnJumpEvent += HandleJumpEvent;
    }

    private void HandleAttackEvent()
    {
        if(_player.MovementCompo.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Attack);
        }
    }

    private void HandleJumpEvent()
    {
        if(_player.MovementCompo.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

    public override void UpdateState()
    {
        if (_player.InputReder.Movement.x != 0)
        {
            _player.StateMachine.ChangeState(PlayerStateEnum.Run);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.InputReder.OnAttackEvent -= HandleAttackEvent;
        _player.InputReder.OnJumpEvent -= HandleJumpEvent;
    }
}
