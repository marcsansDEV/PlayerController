using UnityEngine;

[RequireComponent(typeof(Player))]
public abstract class BasePlayerComponent : MonoBehaviour
{
    protected Player m_Player;

    protected virtual void Awake()
    {
        m_Player = GetComponent<Player>();
    }
}