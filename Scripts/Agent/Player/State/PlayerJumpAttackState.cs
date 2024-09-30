using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpAttackState : PlayerState
{
    public PlayerJumpAttackState(Player player, PlayerStateMachine stateMachine, string boolName) : base(player, stateMachine, boolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_player.MovementCompo.IsGround){
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        if(_endTriggerCalled){
            if(_player.MovementCompo.Velocity.y > 0)
                _stateMachine.ChangeState(PlayerStateEnum.Jump);
            else if(_player.MovementCompo.Velocity.y < 0)
                _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    public override void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
