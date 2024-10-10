using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeFeedback : Feedback
{
    [Header("ShakeSettings")]
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakeAmplitude = 5f;
    [SerializeField] private float _shakeFrequnecy = 5f;
    [SerializeField] private bool _fade;
    
    public override void CreateFeedback()
    {
        CameraManager.Instance.ShakeCamera(_shakeDuration, _shakeAmplitude, _shakeFrequnecy, _fade);
    }

    public override void FinishFeedback()
    {
        
    }
}
