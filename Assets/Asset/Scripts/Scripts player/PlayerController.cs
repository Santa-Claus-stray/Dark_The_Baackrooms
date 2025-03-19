using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public CameraController cameraController;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float crouchTransitionSpeed = 10f;
    public LayerMask groundLayer; // Слой для проверки земли
    public float groundCheckRadius = 0.2f; // Радиус проверки земли

    private float ySpeed;
    private bool isCrouching = false;
    private bool wantsToCrouch = false;
    private float originalHeight;
    private float crouchedHeight = 0.5f;
    private float currentHeight;
    private float targetHeight;
    private float currentSpeed;
    private float targetSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private float standingCheckRadius = 0.2f;
    private bool isGrounded;
    private bool isJumping = false;
    private float lastGroundedTime;
    private float coyoteTime = 0.2f; // Время, в течение которого можно прыгнуть после схода с платформы

    void Start()
    {
        originalHeight = controller.height;
        currentHeight = originalHeight;
        targetHeight = originalHeight;
        currentSpeed = walkSpeed;
        targetSpeed = walkSpeed;

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        
        if (animator != null)
        {
            animator.applyRootMotion = false;
        }
    }

    void Update()
    {
        // Проверяем, находится ли персонаж на земле
        CheckGrounded();
        
        HandleCrouchInput();
        MovePlayer();
        HandleJump();
        UpdateCrouch();
    }

    void CheckGrounded()
    {
        // Проверяем землю с помощью Physics.CheckSphere
        Vector3 checkPosition = transform.position + Vector3.down * 0.1f;
        isGrounded = Physics.CheckSphere(checkPosition, groundCheckRadius, groundLayer);
        
        // Обновляем время последнего касания земли
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }

    void HandleCrouchInput()
    {
        wantsToCrouch = Input.GetKey(KeyCode.LeftControl);
    }

    void UpdateCrouch()
    {
        bool canStand = !Physics.SphereCast(
            transform.position + Vector3.up * (crouchedHeight - 0.1f),
            standingCheckRadius,
            Vector3.up,
            out RaycastHit hit,
            originalHeight - crouchedHeight + 0.1f,
            groundLayer
        );

        targetHeight = (wantsToCrouch || !canStand) ? crouchedHeight : originalHeight;
        
        // Сохраняем текущую позицию
        Vector3 currentPosition = transform.position;
        
        // Изменяем высоту контроллера
        float heightDifference = currentHeight - targetHeight;
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        controller.height = currentHeight;

        // Корректируем позицию, чтобы персонаж не поднимался
        Vector3 newPosition = currentPosition;
        newPosition.y -= heightDifference;
        transform.position = newPosition;

        isCrouching = Mathf.Abs(currentHeight - crouchedHeight) < 0.01f;

        if (cameraController != null)
        {
            cameraController.SetCrouchState(isCrouching);
        }

        // Обновляем центр контроллера
        Vector3 center = controller.center;
        center.y = currentHeight / 2f;
        controller.center = center;
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        targetSpeed = isCrouching ? crouchSpeed : (isRunning ? runSpeed : walkSpeed);
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = Vector3.ClampMagnitude(move, 1f);

        moveDirection = Vector3.Lerp(moveDirection, move * currentSpeed, Time.deltaTime * 15f);
        
        if (isGrounded)
        {
            ySpeed = -2f;
            isJumping = false;
            if (moveDirection.magnitude < 0.1f)
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        // Обновляем анимацию
        if (animator != null)
        {
            float moveAmount = move.magnitude;
            animator.SetBool("isRunning", isRunning && moveAmount > 0.1f);
            animator.SetFloat("Speed", moveAmount, 0.1f, Time.deltaTime);
            animator.SetBool("IsCrouching", isCrouching);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetBool("IsJumping", isJumping);
        }

        // Уведомляем камеру о состоянии прыжка
        if (cameraController != null)
        {
            cameraController.SetJumpState(!isGrounded);
        }

        // Применяем движение
        Vector3 finalMove = moveDirection + new Vector3(0, ySpeed, 0);
        controller.Move(finalMove * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Проверяем, можем ли мы прыгнуть (на земле или в пределах coyote time)
            bool canJump = isGrounded || (Time.time - lastGroundedTime <= coyoteTime);
            
            // Проверяем, что персонаж не приседает
            if (canJump && !isCrouching)
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;
            }
        }
    }

    // Визуализация отладочной информации в редакторе
    void OnDrawGizmos()
    {
        // Визуализация проверки земли
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * 0.1f, groundCheckRadius);
        
        // Визуализация CharacterController
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + controller.center, 
            new Vector3(controller.radius * 2, controller.height, controller.radius * 2));
    }
} 