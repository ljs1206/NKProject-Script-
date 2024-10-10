using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool IsDestroyed = false;

    public static T Instance
    {
        get
        {
            if (IsDestroyed == true)
            {
                _instance = null;
            }
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    Debug.LogError(typeof(T).Name + " could not be found");
                }
                else
                {
                    IsDestroyed = false;
                }
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        IsDestroyed = true;
    }
}
