using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class GamepadController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] private float gravity = -0.01f;
    [SerializeField] private float deadZone = 0.1f;
    [SerializeField] private float rotateSpeed = 1f;

    [SerializeField] private bool isGamepad;
    private PlayerControls gamepad;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;
    private PlayerInput playerInput;
    [SerializeField] private Transform camera;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator charAnimation;


    private void Awake()
    {
        gamepad = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        
    }

    private void OnEnable()
    {
        gamepad.Enable();
    }

    private void OnDisable()
    {
        gamepad.Enable();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    private void HandleInput()
    {
        movement = gamepad.Controller.Movement.ReadValue<Vector2>();
        aim = gamepad.Controller.Aim.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        if (move != Vector3.zero)
        {
            // Jika bergerak, aktifkan animasi running
            charAnimation.SetBool("IsRunning", true);
        }
        else
        {
            // Jika tidak bergerak, matikan animasi running
            charAnimation.SetBool("IsRunning", false);
        }

        /*characterController.Move(move * moveSpeed * Time.deltaTime);*/

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        // Menghadapkan karakter ke arah hadap kamera
        Vector3 moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * move;

        // Menggerakkan objek menggunakan CharacterController
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

    }

    private void HandleRotation()
    {
        // Mengatur rotasi hanya pada sumbu X dan Y
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.x += -aim.y * rotateSpeed * Time.deltaTime;
        eulerRotation.y += aim.x * rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(eulerRotation);
    }



}
