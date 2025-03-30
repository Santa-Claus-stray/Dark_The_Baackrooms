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
    public Camera playerCamera;
    public float standingCameraHeight = 1.7f; // Высота камеры, когда персонаж стоит
    public float crouchingCameraHeight = 0.5f; // Высота камеры, когда персонаж приседает



    private float ySpeed;
    private bool isCrouching = false;
    private bool wantsToCrouch;
    private float originalHeight;
    private float crouchedHeight = 0.5f;
    private float currentHeight;
    private float targetHeight;
    private float currentSpeed;
    private float targetSpeed;
    private Vector3 moveDirection = Vector3.zero;
    private float standingCheckRadius = 0.2f;
    private bool isGrounded;
    private bool isJumping;
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
        controller.height = currentHeight;
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
        Vector3 spherePosition = transform.position + Vector3.down * (groundCheckRadius + 0.1f);
        isGrounded = Physics.CheckSphere(spherePosition, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            lastGroundedTime = Time.time; // Обновляем время последнего касания земли
            ySpeed = 0; // Сбрасываем вертикальную скорость при касании земли
            isJumping = false; // Сбрасываем состояние прыжка
        }
    }


    void HandleCrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            wantsToCrouch = true; // Игрок хочет присесть
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            wantsToCrouch = false; // Игрок отпустил клавишу
        }
    }

    void UpdateCrouch()
    {
        // Проверяем, хочет ли игрок присесть
        if (wantsToCrouch && !isCrouching)
        {
            isCrouching = true;
            targetHeight = crouchedHeight;
            currentSpeed = crouchSpeed; // Устанавливаем скорость при приседании
            animator.SetTrigger("Crouch");
        }
        else if (!wantsToCrouch && isCrouching)
        {
            isCrouching = false;
            targetHeight = originalHeight;
            currentSpeed = walkSpeed; // Восстанавливаем скорость
            animator.SetTrigger("Stand");
        }

        // Плавно изменяем текущую высоту
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        controller.height = currentHeight;

        // Обновляем позицию контроллера
        Vector3 controllerPosition = transform.position;
        controllerPosition.y = currentHeight / 2; // Устанавливаем позицию контроллера в зависимости от его высоты
        controller.transform.position = controllerPosition;

        // Обновляем позицию камеры
        Vector3 cameraPosition = transform.position;

        // Плавно изменяем высоту камеры
        float cameraTargetHeight = isCrouching ? crouchingCameraHeight : standingCameraHeight;
        cameraPosition.y += Mathf.Lerp(playerCamera.transform.position.y, cameraTargetHeight, Time.deltaTime * crouchTransitionSpeed);

        // Обновляем позицию камеры
        playerCamera.transform.position = cameraPosition;

        // Если необходимо, можно также обновить направление взгляда камеры
        playerCamera.transform.LookAt(transform.position + transform.forward); // Это просто пример

        // Логирование для отладки
        Debug.Log($"Current Height: {currentHeight}, Target Height: {targetHeight}, Camera Position: {cameraPosition}");
    }






    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Определяем направление движения
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Устанавливаем скорость в зависимости от состояния
        if (isCrouching)
        {
            currentSpeed = crouchSpeed; // Если приседаем, используем скорость приседания
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // Если удерживаем Shift, используем скорость бега
        }
        else
        {
            currentSpeed = walkSpeed; // В противном случае используем скорость ходьбы
        }

        // Применяем движение с учетом текущей скорости
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Обновляем анимации
        UpdateAnimation(move);
    }

    void UpdateAnimation(Vector3 move)
    {
        float moveAmount = move.magnitude;

        // Обновляем параметры анимации
        if (animator != null)
        {
            animator.SetBool("isRunning", currentSpeed == runSpeed && moveAmount > 0.1f);
            animator.SetFloat("Speed", moveAmount);
            animator.SetBool("IsCrouching", isCrouching);
            animator.SetBool("IsGrounded", isGrounded);
            animator.SetBool("IsJumping", isJumping);
        }
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
        // Проверка на null для предотвращения ошибок
        if (controller == null) return;

        // Визуализация проверки земли
        Gizmos.color = isGrounded ? Color.green : Color.red;
        float groundCheckOffset = 0.1f; // Константа для смещения вниз
        Gizmos.DrawWireSphere(transform.position + Vector3.down * groundCheckOffset, groundCheckRadius);

        // Визуализация CharacterController
        Gizmos.color = Color.yellow;
        Vector3 centerPosition = transform.position + controller.center;
        Vector3 size = new Vector3(controller.radius * 2, controller.height, controller.radius * 2);
        Gizmos.DrawWireCube(centerPosition, size);
    }

}