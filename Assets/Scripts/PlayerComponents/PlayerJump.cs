using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : BasePlayerComponent
{
    public Action OnJump;
    [Space(5)]
    
    [SerializeField] private UnityEvent OnGroundJump;
    [SerializeField] private UnityEvent OnAirJump;
    [SerializeField] private UnityEvent OnLand;
    
    private int m_JumpsCount;
    private float m_LastJumpPressTime;
    private bool m_TryingToJump;
    private bool m_IsOrWasGrounded;

    [Header("========== HEIGHT RELATED PROPERTIES ==========")]
    [Space(3)]
    [SerializeField] private float m_FirstJumpHeight;
    [SerializeField] private float m_SecondJumpHeight;
    
    [Header("========== INPUT RELATED PROPERTIES ==========")]
    [Space(3)]
    [SerializeField] private float m_JumpPressBufferTime = 0.3f;
    
    [Header("========== OTHER PROPERTIES ==========")]
    [Space(3)]
    [SerializeField] private int m_MaxJumps = 2;

    private bool m_HasPressedJump;
    private void OnEnable()
    {
        m_Player.Controller.OnIsGroundedChanged += OnIsGroundedChanged;
        m_Player.Controller.OnBeforeMove += OnBeforeMove;
    }

    private void OnDisable()
    {
        m_Player.Controller.OnIsGroundedChanged -= OnIsGroundedChanged;
        m_Player.Controller.OnBeforeMove -= OnBeforeMove;

    }

    private void Update()
    {
        if (m_Player.Input.JumpInput)
        {
            m_TryingToJump = true;
            m_HasPressedJump = true;
            m_LastJumpPressTime = Time.unscaledTime;
        }
    }

    private void OnIsGroundedChanged(bool grounded)
    {
        if (grounded)
        {
            // if (m_Player.Controller.LastVel.y < -13)
            // {
            //     Ray l_TransformPosition = new Ray(transform.position + Vector3.up * 1f, Vector3.down);
            //     if (Physics.Raycast(l_TransformPosition, out RaycastHit l_Hit, 1.1f))
            //     {
            //         if (l_Hit.transform.tag != "Terrain/Bouncy")
            //             OnLand?.Invoke();
            //     }
            // }
        }
    }

    private void OnBeforeMove()
    {
        if (m_Player.Controller.IsGrounded)
            m_JumpsCount = 0;
        
        bool l_WasTryingToJump = Time.unscaledTime - m_LastJumpPressTime < m_JumpPressBufferTime && m_LastJumpPressTime != 0 && m_HasPressedJump;

        bool l_IsOrWasTryingToJump = m_TryingToJump || (l_WasTryingToJump && m_Player.Controller.IsGrounded);

        bool l_JumpAllowed = m_JumpsCount < m_MaxJumps && m_Player.Fsm.IsWalking;

        bool l_CanJump = l_JumpAllowed && l_IsOrWasTryingToJump && !m_Player.Controller.IsLocked;
        if (l_CanJump)
            Jump();
        m_TryingToJump = false; 
    }

    private void Jump()
    {
        float l_JumpSeed;
        if (m_JumpsCount <= 0)
            l_JumpSeed = UtilsMethods.HeightToForce(m_FirstJumpHeight, m_Player.Controller.Gravity);
        else
        {
            l_JumpSeed = UtilsMethods.HeightToForce(m_SecondJumpHeight, m_Player.Controller.Gravity);
        }
        m_JumpsCount++;
        m_Player.Controller.AddImpulse(new Vector3(0f, l_JumpSeed, 0f), false, true);
        OnJump?.Invoke();
        m_HasPressedJump = false;

        if (m_Player.Controller.IsGrounded)
        {
            OnGroundJump?.Invoke();
        }
        else
        {
            OnAirJump?.Invoke();
        }
    }

    public void ResetAirJumps()
    {
        m_JumpsCount = 0;
    }
}
