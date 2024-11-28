using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Events for communication
    public Action OnBeforeMove;
    public Action OnAfterMove;
    public Action<bool> OnIsGroundedChanged;
    public Action OnLanded;

    // Components and state variables
    private CharacterController m_CharacterController;
    private Vector3 m_Velocity;
    private bool m_WasGrounded;
    private float m_LockDuration;

    [Header("Controller Settings")]
    public float Gravity = -9.81f;
    [SerializeField] private float m_BaseWallBounciness = 0.5f;
    [SerializeField] private LayerMask m_GroundedLayerMask;

    private bool m_ForceLocked = false;

    // Public properties
    public bool IsLocked => m_LockDuration > 0.0f || m_ForceLocked;
    public bool IsGrounded { get; set; }

    public Vector3 Velocity
    {
        get => m_Velocity;
        set => m_Velocity = value;
    }

    public bool UseGravity { get; set; } = true;
    public float GravityModifier { get; set; } = 1f;
    public Vector3 HorVelocity
    {
        get
        {
            var l_Vel = m_Velocity;
            l_Vel.y = 0;
            return l_Vel;
        }
    }
    private void Awake()
    {
        // Initialize the character controller component
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Update the lock duration
        m_LockDuration -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Apply gravity if enabled
        if (UseGravity)
            UpdateGravity();
        
        // Handle movement and grounded state updates
        UpdateMovement();
        UpdateGrounded();
    }

    private void UpdateMovement()
    {
        if (m_CharacterController.enabled)
        {
            // Notify listeners before moving
            OnBeforeMove?.Invoke();

            // Apply velocity to move the character
            m_CharacterController.Move(m_Velocity * Time.fixedDeltaTime);

            // Notify listeners after moving
            OnAfterMove?.Invoke();
        }
    }

    private void UpdateGrounded()
    {
        bool wasGrounded = IsGrounded;

        // Check if the character is grounded
        IsGrounded = Physics.CheckSphere(transform.position, 0.1f, m_GroundedLayerMask);

        // Trigger events if the grounded state changes
        if (wasGrounded != IsGrounded)
        {
            OnIsGroundedChanged?.Invoke(IsGrounded);

            if (IsGrounded && m_Velocity.y < 0)
            {
                m_Velocity.y = 0f;
                OnLanded?.Invoke();
            }
        }
    }

    private void UpdateGravity()
    {
        // Apply gravity if the character is not grounded
        if (!IsGrounded)
        {
            m_Velocity.y += Gravity * Time.fixedDeltaTime * GravityModifier;
        }
    }

    public void AddImpulse(Vector3 velocity, bool overrideHorizontal, bool overrideVertical)
    {
        // Add an impulse to the velocity, with optional overrides
        if (overrideHorizontal)
        {
            m_Velocity.x = velocity.x;
            m_Velocity.z = velocity.z;
        }
        else
        {
            m_Velocity.x += velocity.x;
            m_Velocity.z += velocity.z;
        }

        if (overrideVertical)
        {
            m_Velocity.y = velocity.y;
        }
        else
        {
            m_Velocity.y += velocity.y;
        }
    }

    public void LockMovement(float duration)
    {
        // Lock the character's movement for the specified duration
        if (m_LockDuration < duration)
        {
            m_LockDuration = duration;
        }
    }
}
