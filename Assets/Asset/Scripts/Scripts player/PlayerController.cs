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
    public float jumpForce = 5f; // Определите силу прыжка

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
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController not found on " + gameObject.name);
            return; // Прекращаем выполнение метода, если контроллер не найден
        }

        originalHeight = controller.height;
        currentHeight = originalHeight;
        targetHeight = originalHeight;
        currentSpeed = walkSpeed;
        targetSpeed = walkSpeed;

        animator = GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.applyRootMotion = false;
        }

        // Для отладки
        Debug.Log("Original Height: " + originalHeight);
        Debug.Log("Current Speed: " + currentSpeed);
    }

    void Update()
    {
        // Проверяем, находится ли персонаж на земле
        CheckGrounded();
        HandleCrouchInput();
        UpdateCrouch();
        MovePlayer();
        HandleJump();
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            lastGroundedTime = Time.time; // Обновляем время последнего касания земли
            ySpeed = 0; // Сбрасываем вертикальную скорость при касании земли
            isJumping = false; // Сбрасываем состояние прыжка
        }
    }

    void HandleCrouchInput()
    {
        wantsToCrouch = Input.GetKey(KeyCode.LeftControl);
    }

    void UpdateCrouch()
{
    wantsToCrouch = Input.GetKey(KeyCode.LeftControl); // Проверяем, нажата ли клавиша

    if (wantsToCrouch && !isCrouching)
    {
        isCrouching = true; // Переключаем состояние на приседание
        targetHeight = crouchedHeight; // Целевая высота - высота приседа
        animator.SetBool("IsCrouching", true); // Анимация приседания
        Debug.Log("Начало приседания");
    }
    else if (!wantsToCrouch && isCrouching)
    {
        isCrouching = false; // Переключаем состояние на стояние
        targetHeight = originalHeight; // Целевая высота - оригинальная высота
        animator.SetBool("IsCrouching", false); // Анимация стояния
        Debug.Log("Возвращение в стоячее положение");
    }

    // Плавный переход высоты
    currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
    controller.height = currentHeight; // Устанавливаем высоту контроллера

    // Обновляем позицию камеры
    Vector3 cameraOffset = new Vector3(0, currentHeight / 2, 0); // Смещение камеры
    cameraController.transform.localPosition = cameraOffset; // Устанавливаем позицию камеры
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
            // Устанавливаем небольшое значение ySpeed для "погружения" в землю
            ySpeed = -2f;
            isJumping = false;

            // Обработка прыжка
            if (Input.GetKeyDown(KeyCode.Space)) // Проверка нажатия пробела
            {
                ySpeed = jumpForce; // Устанавливаем ySpeed на желаемую величину для прыжка
                isJumping = true;
            }

            if (moveDirection.magnitude < 0.1f)
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime; // Применяем гравитацию
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
        // Проверяем, находится ли персонаж на земле
        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer);

        // Если персонаж на земле и нажата клавиша пробела
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Устанавливаем вертикальную скорость для прыжка
            ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true; // Устанавливаем состояние прыжка
        }
        else if (!isGrounded)
        {
            // Применяем гравитацию, если не на земле
            ySpeed += gravity * Time.deltaTime;
        }
        else
        {
            // Если персонаж на земле, сбрасываем вертикальную скорость
            ySpeed = -2f; // Небольшое значение, чтобы избежать прилипания к земле
            isJumping = false; // Сбрасываем состояние прыжка
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