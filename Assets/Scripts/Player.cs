using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerController Controller{ get; private set; }
    public PlayerInputManager Input { get; private set; }
    public PlayerMoveFSM Fsm { get; private set; }
    public PlayerJump Jump { get; private set; }
    public PlayerRotation PlayerRotation { get; private set; }



    protected override void Awake()
    {
        base.Awake();
        
        Input = GetComponent<PlayerInputManager>();
        Controller = GetComponent<PlayerController>();
        Fsm = GetComponent<PlayerMoveFSM>();
        Jump = GetComponent<PlayerJump>();
        PlayerRotation = GetComponent<PlayerRotation>();
    }
    
}
