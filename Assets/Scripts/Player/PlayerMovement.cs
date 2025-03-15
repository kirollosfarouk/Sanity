using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public float WalkSpeed = 6f;
    public float Gravity = 10f;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _characterController;
    private InputSystem_Actions _input;

    private bool _canMove = true;
    private float _currentSpeed = 0f;

    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.Player.AddCallbacks(this);
        _currentSpeed = WalkSpeed;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void Update()
    {
        var camRotationY = Camera.main.transform.rotation.eulerAngles.y;
        var moveRotationQuaternion = Quaternion.AngleAxis(camRotationY, Vector3.up);

        var curSpeed = _canMove ? _input.Player.Move.ReadValue<Vector2>().normalized * _currentSpeed : Vector2.zero;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = moveRotationQuaternion * new Vector3(curSpeed.x, movementDirectionY, curSpeed.y);

        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }


        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }
}