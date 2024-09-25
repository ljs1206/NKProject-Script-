using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rg2d;
    public Rigidbody2D Rg2d {get; protected set;}
    private Transform _visualTrm;
    
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    
    [Header("Jump Settings")]
    [SerializeField] private Transform _footTrm;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _checkJumpRay; 
    [SerializeField] private LayerMask _whatIsGround;
    
    [Header("Input")]
    [SerializeField] private InputReader _InputReader;

    public bool IsGround;
    [HideInInspector] public bool CanMove = true;

    private void Awake()
    {
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
        IsGround = Physics2D.Raycast(_footTrm.position, Vector3.down,
            _checkJumpRay, _whatIsGround);
    }

    public void Move()
    {
        Rg2d.velocity = new Vector2(_InputReader.Movement.x *_moveSpeed ,Rg2d.velocity.y);
        if (_InputReader.Movement.x != 0)
        {
            _visualTrm.localScale = new Vector3(_InputReader.Movement.x,
                _visualTrm.localScale.y, _visualTrm.localScale.z) ;
        }
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
    #endif
}
