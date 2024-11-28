using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private Transform m_TPCameraTransform;
    [SerializeField] InputActionAsset m_InputAsset;

    #region MovementInput

    public bool EnterGlideInput => FindPlayerInputAction("Glide").WasPressedThisFrame();

    public bool ExitGlideInput => FindPlayerInputAction("Glide").WasReleasedThisFrame();
    public bool JumpInput => FindPlayerInputAction("Jump").WasPerformedThisFrame();
    public bool RollInput => FindPlayerInputAction("Roll").WasPressedThisFrame();
    public Vector3 Horizontal => GetWorldToCameraVector(FindPlayerInputAction("Horizontal").ReadValue<Vector2>());
    public Vector3 CameraInput => FindPlayerInputAction("Camera").ReadValue<Vector2>();
    #endregion

    private void Awake()
    {
        m_TPCameraTransform = Camera.main.transform;
        m_InputAsset.Enable();
        m_InputAsset.FindActionMap("Player").Enable();
    }
    private InputAction FindPlayerInputAction(string actionName)
    {
        return m_InputAsset.FindActionMap("Player").FindAction(actionName);
    }

    private Vector3 GetWorldToCameraVector(Vector2 input)
    {
        Vector3 l_Forward = m_TPCameraTransform.forward;
        l_Forward.y = 0;
        l_Forward.Normalize();
        Vector3 l_Right = m_TPCameraTransform.right;
        l_Right.y = 0;
        l_Right.Normalize();
        Vector2 l_Input = FindPlayerInputAction("Horizontal").ReadValue<Vector2>();
        return l_Forward * l_Input.y + l_Right * l_Input.x;
    }
}
