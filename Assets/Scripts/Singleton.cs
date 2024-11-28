using UnityEngine;
/// <summary>
/// A static Instance is similar to a singleton, but instead of destroying any new
/// instances, it overrides the current Instance. This is handy for resetting the state /// and saves you doing it manually
/// </summary>

/// <summary>
/// A static Instance is similar to a singleton, but instead of destroying any new
/// instances, it overrides the current Instance. This is handy for resetting the state /// and saves you doing it manually
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T m_Instance;

    protected virtual void Awake()
    {
        if (m_Instance != null && !m_Instance.Equals(null))
            Destroy(gameObject);            
        else
            m_Instance = this as T;
    }
    protected virtual void OnApplicationQuit()
    {
        m_Instance = null;
        Destroy(gameObject);
    }

    public static T GetInstance()
    {
        if (m_Instance == null || m_Instance.Equals(null))
        {
            var l_GameObject = new GameObject(typeof(T).Name);
            m_Instance = l_GameObject.AddComponent<T>();
        }
        return m_Instance;
    }
}
/// <summary>
/// This transforms the static Instance into a basic singleton. This will destroy any new
/// versions created, leaving the original Instance intact
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (m_Instance != null && !m_Instance.Equals(null)) Destroy(gameObject);
        base.Awake();
    }
}
/// <summary>
/// Finally we have a persistent version of the singleton. This will survive through scene
/// loads. Perfect for system classes which require stateful, persistent data. Or audio sources /// where music plays through loading screens, etc
/// </summary>
public abstract class PersistentSingleton<T>: Singleton<T> where T : MonoBehaviour 
{ 
    protected override void Awake()
    {
        base.Awake();
        gameObject.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}
