using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    [SerializeField] private float _maxHealth = 100f;

    private Agent _owner;
    private float _currentHealth;

    public void Initialize(Agent player)
    {
        _owner = player;
        _currentHealth = _maxHealth;
    }
    
    public void ApplyDamage(int damage, Vector3 hitPoint, Vector3 normal, float knockbackPower, Agent dealer,
        DamageType damageType)
    {
        if (_owner.isDead) return;
        
        _currentHealth = Mathf.Clamp(
            _currentHealth - damage, 0, _maxHealth);
        OnHitEvent?.Invoke(); 
        
        if(_currentHealth <= 0)
        {
            OnDeadEvent?.Invoke();
        }
    }
}
