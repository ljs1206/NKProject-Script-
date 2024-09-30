using UnityEngine;

public class CheckPlayerHideObject : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private int _downValue = 140;
    [SerializeField] private Transform _playerTrm;
    private Collider2D _hideCol;
    
    private void Update()
    {
        if (TryToRay())
        {
            Color objColor = _hideCol.gameObject.GetComponent<SpriteRenderer>().color;
            _hideCol.gameObject.GetComponent<SpriteRenderer>().color =
                new Color(objColor.r, objColor.b, objColor.g, _downValue);
        }
    }

    private bool TryToRay()
    {
        if (Camera.main != null)
        {
            Vector3 dir = (_playerTrm.position - Camera.main.transform.position).normalized;
            _hideCol = Physics2D.Raycast(Camera.main.transform.position,
                dir, int.MaxValue,
                _targetLayer).collider;
        }

        return _hideCol;
    }

    private void OnDrawGizmos()
    {
        if (_playerTrm != null)
        {
            Vector3 dir = (_playerTrm.position - Camera.main.transform.position).normalized;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(Camera.main.transform.position,
                dir);
        }
    }
}
