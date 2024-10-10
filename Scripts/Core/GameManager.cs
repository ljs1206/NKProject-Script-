using System.Collections;
using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public void SetTime(float value)
    {
        Time.timeScale = value;
    }

    public IEnumerator DelayCoro(float seconds, Action action)
    {
        Debug.Log(seconds);
        yield return new WaitForSecondsRealtime(seconds);
        action?.Invoke();
    }
}
