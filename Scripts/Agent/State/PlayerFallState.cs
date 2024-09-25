using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.PlayerMovement.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
