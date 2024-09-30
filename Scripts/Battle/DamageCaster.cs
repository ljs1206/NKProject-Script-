using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private int _damage; // 적에게 줄 데미지(나중에 수정할 예정)
    [Header("Overlap Settings")]
    [SerializeField] private Transform _attackTrm; // Cast시작 위치
    [SerializeField] private LayerMask _whatIsTarget; // Cast에 걸릴 Layer

    [Header("Box")]
    [SerializeField]
    private Vector2 _castBoxSize; // CastBox의 크기
    [SerializeField]
    private float _castDistance; // Cast를 날릴 길이

    [SerializeField]
    [Range(0, 1f)]
    private float _casterInterpolation = 0.5f; // 보간

    private Agent _owner; // 소유자

    public void InitCaster(Agent agent)
    {
        _owner = agent;
    }
    
    private RaycastHit2D[] _hitInfos = {};

    public bool CastDamage() // 피격 되었나?
    {
        bool isSucess = false;
        Vector3 visualScaleVec = new Vector3(transform.parent.localScale.x, 0, 0); 
        Vector3 startPos = GetStartPos();
        
        _hitInfos = Physics2D.BoxCastAll(startPos, _castBoxSize, 0,
            visualScaleVec, _castDistance, _whatIsTarget);
        
        if (_hitInfos.Length > 0)
        {
            foreach(RaycastHit2D hit in _hitInfos){
                if(hit.collider.TryGetComponent(out IDamageable health))
                {
                    if(!isSucess) isSucess = true;
                    health.ApplyDamage(_damage, hit.point, hit.normal, 5, _owner, DamageType.Melee);
                }
                else{
                    Debug.LogError($"{hit.collider.name} is don’t have Helath Componenet");
                }
            }
        }
        
        return isSucess;
    }

    private Vector3 GetStartPos() // Cast 시작 위치 구하는 Method
    {
        return _attackTrm.position + new Vector3(transform.parent.localScale.x, 0, 0) * -_casterInterpolation * 2;
    }
    
    public void OnDrawGizmos(){
        Vector3 visualScaleVec = new Vector3(transform.parent.localScale.x, 0, 0);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(transform.parent.localScale.x, 0, 0));
        Gizmos.DrawWireCube(GetStartPos(), _castBoxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetStartPos() + visualScaleVec * _castDistance, _castBoxSize);
        Gizmos.color = Color.white;
    }
}
