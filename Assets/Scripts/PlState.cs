using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMoveFSM))]
[RequireComponent(typeof(Player))]
public abstract class PlState: MonoBehaviour
{
    protected Player m_PlayerCtrl;
        
    [Header("========== BASE STATE PROPERTIES ==========")]
    [Space(5)]
    [SerializeField] private List<PlayerMoveFSM.TStates> m_PossibleNextStates = new();
    [Space(10)]
    [SerializeField] private UnityEvent OnEnterState;
    [SerializeField] private UnityEvent OnExitState;
    [Header("========== MOVEMENT RELATED PROPERTIES ==========")]
    [Space(5)]
    [SerializeField] protected float m_MaxSpeed;
    [SerializeField] protected float m_Acceleration;
    [Range(0f,1f)]
    [SerializeField] protected float m_GravityMod = 1f;

    public abstract PlayerMoveFSM.TStates Type { get; }
    public abstract bool NormalizedInput { get; }
    public float GravityMod => m_GravityMod;

    protected virtual void Awake()
    {
        m_PlayerCtrl = GetComponent<Player>();
    }

    protected virtual void OnEnable()
    {
        m_PlayerCtrl.Controller.OnBeforeMove += OnBeforeMove;
        m_PlayerCtrl.Controller.OnAfterMove += OnAfterMove;
    }
    protected virtual void OnDisable()
    {
        m_PlayerCtrl.Controller.OnBeforeMove -= OnBeforeMove;
        m_PlayerCtrl.Controller.OnAfterMove -= OnAfterMove;
    }
    public virtual void OnEnter()
    {
        OnEnterState?.Invoke();
        m_PlayerCtrl.Fsm.SetMaxSpeed(m_MaxSpeed);
        m_PlayerCtrl.Fsm.SetMaxAcceleration(m_Acceleration);
        enabled = true;
    }

    public virtual void OnExit()
    {
        OnExitState?.Invoke();
        enabled = false;
    }

    public virtual void Update()
    {
        TryChangeState();
    }

    private void TryChangeState()
    {
        if (!CanExitState())
            return;
        
        foreach (var l_StateType in m_PossibleNextStates)
        {
            var l_State = m_PlayerCtrl.Fsm.GetState(l_StateType);
            if (l_State.CanEnterState())
            {
                m_PlayerCtrl.Fsm.ChangeState(l_StateType);
            }
        }
    }

    protected virtual void OnBeforeMove()
    {
    }
    protected virtual void OnAfterMove()
    {
    }
    
    protected abstract bool CanEnterState();
    protected abstract bool CanExitState();
}