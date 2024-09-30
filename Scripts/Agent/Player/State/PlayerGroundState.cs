using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerState
{
    // Start is called before the first frame update
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputReder.OnAttackEvent += HandleAttackEvent;
        _player.InputReder.OnJumpEvent += HandleJumpEvent;
    }

    public override void Exit()
    {
        base.Exit();
        _player.InputReder.OnAttackEvent -= HandleAttackEvent;
        _player.InputReder.OnJumpEvent -= HandleJumpEvent;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(!_player.MovementCompo.IsGround && _player.MovementCompo.Velocity.y < 0.1f){
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }
    private void HandleJumpEvent()
    {
        if(_player.MovementCompo.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
        }
    }

    private void HandleAttackEvent()
    {
        if(_player.MovementCompo.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Attack);
        }
    }
}
