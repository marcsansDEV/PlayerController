using UnityEngine;

public class PlGlideState : PlState
{
    [Header("========== OTHER PROPERTIES ==========")]
    [Space(5)]
    [SerializeField] private float m_MaxFallingSpeed;
    public override bool NormalizedInput => false;
    public override PlayerMoveFSM.TStates Type => PlayerMoveFSM.TStates.Glide;

    public override void OnEnter()
    {
        base.OnEnter();
        var l_Vel = m_PlayerCtrl.Controller.Velocity;
        l_Vel.y *= 0.5f;
        m_PlayerCtrl.Controller.Velocity = l_Vel;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected override void OnBeforeMove()
    {
        base.OnBeforeMove();
        if (-m_PlayerCtrl.Controller.Velocity.y > m_MaxFallingSpeed)
        {
            var l_Vel = m_PlayerCtrl.Controller.Velocity;
            l_Vel.y = Mathf.MoveTowards(l_Vel.y, -m_MaxFallingSpeed, m_Acceleration * Time.fixedDeltaTime);
            m_PlayerCtrl.Controller.Velocity = l_Vel;
        }
    }

    protected override bool CanEnterState()
    {
        return m_PlayerCtrl.Input.EnterGlideInput && !m_PlayerCtrl.Controller.IsGrounded;

    }

    protected override bool CanExitState()
    {
        return m_PlayerCtrl.Input.ExitGlideInput || m_PlayerCtrl.Controller.IsGrounded;
    }
}