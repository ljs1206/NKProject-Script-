using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public enum CameraChange
{
    Priority, SetActive // todo 추후에 카메라를 바꾸는 방식이 추가되면 추가해야됨..
}

public class CameraManager : MonoSingleton<CameraManager>
{
    public Camera MainCam { get; private set; }
    
    public CinemachineVirtualCamera currentCam { get; private set; }

    [HideInInspector] public List<CinemachineVirtualCamera> camList = new();

    public void Awake()
    {
        MainCam = Camera.main;
        camList = FindObjectsByType<CinemachineVirtualCamera>(FindObjectsSortMode.None).ToList();
    }

    public void Start()
    {
        currentCam = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    public void AddCamera(CinemachineVirtualCamera vcam)
    {
        camList.Add(vcam);
    }

    /// <summary>
    /// 현재 카메라를 바꾸고 싶다면 이 함수를 함께 불러와주세요..
    /// </summary>
    public void ChangeCamera(CameraChange type, int priority = 0, CinemachineVirtualCamera vcam = null)
    {
        
        switch (type)
        {
            case CameraChange.Priority:
                currentCam.Priority = 0;
                vcam.Priority = priority;
                break;
            case CameraChange.SetActive:
                currentCam.gameObject.SetActive(false);
                vcam.gameObject.SetActive(true);
                break;
        }
        
        currentCam = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    public void ShakeCamera(float duration, float amplitude, float frequency, bool isFade = false)
    {
        var vCam = currentCam.GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(Shake(duration, amplitude, frequency, vCam, isFade));
    }

    private IEnumerator Shake(float duration, float amplitude, float frequency, 
        CinemachineVirtualCamera vCam, bool isFade)
    {
        if (isFade)
        {
            float currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float cAmplitude = Mathf.Lerp(amplitude, 0, Time.fixedDeltaTime);
                float cFrequency = Mathf.Lerp(frequency, 0, Time.fixedDeltaTime);
                
                vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = cAmplitude;
                vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = cFrequency;
                yield return null;
            }
        }
        else
        {
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
            yield return new WaitForSeconds(duration);
        }
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
