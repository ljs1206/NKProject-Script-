using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitStopFeedback : Feedback
{
    [SerializeField] private float _stopDuration;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;
    
    private float _currentTime;
    private Tween playTween;
    private Tween stopTween;
    
    public override void CreateFeedback()
    {
        _currentTime = 0;
        playTween = DOTween.To(() => Time.timeScale, x => _currentTime = x, 1, _fadeInDuration).
            DOTimeScale(0, _fadeInDuration);
        stopTween = DOTween.To(() => Time.timeScale, x => _currentTime = x, 0, _fadeInDuration)
            .DOTimeScale(1, _fadeOutDuration);
        StopTimeFuc();
    }

    private void StopTimeFuc()
    {
        playTween.Play().OnComplete(() => 
            StartCoroutine(GameManager.Instance.DelayCoro(_stopDuration, () => stopTween.Play())));  
    }

    public override void FinishFeedback()
    {
        
    }
}
