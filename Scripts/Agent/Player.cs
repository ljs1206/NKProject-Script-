using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateEnum
{
    Idle, 
    Run, 
    Fall,
    Attack,
    Dash,
    Hit,
    Ground,
    Jump,
    JumpAttack
}

public class Player : Agent
{
    [Header("Setting Values")]
    public float moveSpeed = 8f;
    public float dashSpeed = 20f;

    [Header("Attack Settings")]
    public float attackSpeed = 1f;
    public int currentComboCounter = 0;
    public AnimationEvent _animationEvent;
    public List<int> frontMoves;

    public PlayerStateMachine StateMachine { get; protected set; }
    [Header("Input")]
    [SerializeField] private InputReader _InputReader;
    
    private PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement => _playerMovement;
    public InputReader InputReder => _InputReader;
    // public PlayerVFX PlayerVFXCompo => VFXCompo as PlayerVFX;
    

    protected override void Awake()
    {
        base.Awake();
        
        _playerMovement = GetComponent<PlayerMovement>();
        
        StateMachine = new PlayerStateMachine();

        foreach(PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();

            try
            {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(
                                t, this, StateMachine, typeName) as PlayerState;
                StateMachine.AddState(stateEnum, state);
            }catch(Exception ex)
            {
                Debug.LogError($"{typeName} is loading error! check Message");
                Debug.LogError(ex.Message);
            }
        }
    }

    protected void Start()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    protected void Update()
    {
        StateMachine.CurrentState.UpdateState();

        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     HealthCompo.ApplyDamage(5, Vector3.zero, Vector2.zero, 0, this, DamageType.Melee);
        // }
    }


    public override void Attack()
    {
        // bool success = DamageCasterCompo.CastDamage();

        // if(success && currentComboCounter == 2)
        // {
        //     SkillManager.Instance.GetSkill<ThunderStrikeSkill>().UseSkill();
        // }
    }

    public void PlayBladeVFX()
    {
        // PlayerVFXCompo.PlayBladeVFX(currentComboCounter);
    }

    // public void SwordPosToSpine(){
    //     _animationEvent.
    // }

    public override void SetDead()
    {
        //������ �ƹ��͵� ���մϴ�.
    }

    public void Jump()
    {
        PlayerMovement.Jump();
    }
}
