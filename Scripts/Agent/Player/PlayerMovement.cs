using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    private Rigidbody2D Rg2d;

    public Vector3 Velocity => Rg2d.velocity;

    public bool _isGround;
    public bool IsGround => _isGround;

    public bool CanMove { get; set; }

    private Transform _visualTrm;
    
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    
    [Header("Jump Settings")]
    [SerializeField] private Transform _footTrm;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _checkJumpRay; 
    [SerializeField] private LayerMask _whatIsGround;

    private Player _player;

    public void Initialize(Agent agent)
    {
        _player = agent as Player;
        Rg2d = GetComponent<Rigidbody2D>();
        _visualTrm = transform.Find("Visual");
    }
    private void Start()
    {
        CanMove = true;
    }

    private void Update()
    {
        if(CanMove)
            Move();
        
        CheckOnTheGround();
    }

    public void StopImmediately(){
        Rg2d.velocity = Vector2.zero;
    }

    private void CheckOnTheGround()
    {
        _isGround = Physics2D.Raycast(_footTrm.position, Vector3.down,
            _checkJumpRay, _whatIsGround);
    }

    public void Move()
    {
        Rg2d.velocity = new Vector2(_player.InputReder.Movement.x *_moveSpeed ,Rg2d.velocity.y);
        if (_player.InputReder.Movement.x != 0)
        {
            _visualTrm.localScale = new Vector3(_player.InputReder.Movement.x,
                _visualTrm.localScale.y, _visualTrm.localScale.z) ;
        }
    }

    public void AttackFrontMove(Vector3 force)
    {
        Rg2d.AddForce(force, ForceMode2D.Impulse);
    }
    
    public void Jump(){
        if (IsGround)
        {
            Rg2d.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(_footTrm.position, Vector3.down));
    }

    public void GetKnockback(Vector3 force)
    {
        
    }
#endif
}
