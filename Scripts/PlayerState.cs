using UnityEngine;
using System;

public abstract class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    protected int _animBoolHash;

    protected bool _endTriggerCalled; //�ִϸ��̼� �����̺�Ʈ ���� �� �ʿ�

    public PlayerState(Player player, PlayerStateMachine stateMachine, string boolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(boolName);
    }

    //���ö� ����
    public virtual void Enter()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, true);
        _endTriggerCalled = false;
    }
    //���� �� ����
    public virtual void UpdateState()
    {
        //������ �� �� ����. ���߿� ��ӹ��� �ֵ��� ����Ŵ�.
    }
    //������ ����
    public virtual void Exit()
    {
        _player.AnimatorCompo.SetBool(_animBoolHash, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
