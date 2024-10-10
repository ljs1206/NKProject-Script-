using System.Collections;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    private static readonly int Hit = Shader.PropertyToID("_Hit");
    
    [SerializeField] private float _blinkDuration;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Coroutine _coroutine;
    
    public override void CreateFeedback()
    {
        _coroutine = StartCoroutine(StartBlinkCoro());
    }

    private IEnumerator StartBlinkCoro()
    {
        _spriteRenderer.material.SetInteger(Hit, 1);
        yield return new WaitForSeconds(_blinkDuration);
        _spriteRenderer.material.SetInteger(Hit, 0);
    }

    public override void FinishFeedback()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
        _spriteRenderer.material.SetInteger(Hit, 0);
    }
}
