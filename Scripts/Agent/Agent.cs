using System;
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    #region component list section
    public Animator AnimatorCompo { get; protected set; }
    public SpriteRenderer SpriteRendererComp { get; protected set; }
    public IMovement MovementCompo { get; protected set; }
    // public AgentVFX VFXCompo { get; protected set; }
    public DamageCaster DamageCasterCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }
    #endregion
    
    #region Debug
    public bool IsDebug = false;
    #endregion
    
    // [field:SerializeField] public AgentStat Stat { get; protected set; }

    public Transform VisualTrm {get; private set;}
    public bool CanStateChangeable { get; protected set; } = true;
    public bool isDead;

    protected virtual void Awake()
    {
        if (!IsDebug)
        {
            VisualTrm = transform.Find("Visual");
            AnimatorCompo = VisualTrm.GetComponent<Animator>();
            SpriteRendererComp = VisualTrm.GetComponent<SpriteRenderer>();
            MovementCompo = GetComponent<IMovement>();
            MovementCompo.Initialize(this);
            Transform damageTrm = VisualTrm.Find("DamageCaster");
            if(damageTrm != null)
            {
                DamageCasterCompo = damageTrm.GetComponent<DamageCaster>();
                DamageCasterCompo.InitCaster(this);
            }
        }
        
        HealthCompo = GetComponent<Health>();
        HealthCompo.Initialize(this);

        //
        // VFXCompo = transform.Find("AgentVFX").GetComponent<AgentVFX>();
        //

        //
        // Stat = Instantiate(Stat); //�ڱ��ڽ� ���������� ����� ����.
        // Stat.SetOwner(this);
        //
    }

    public Coroutine StartDelayCallback(float time, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(time, Callback));
    }

    protected IEnumerator DelayCoroutine(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    public virtual void Attack()
    {
        
    }

    public abstract void SetDead();
}