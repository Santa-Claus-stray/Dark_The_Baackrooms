using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;
    public Animator animator; // Ссылка на компонент аниматора
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;
    public float crouchTransitionSpeed = 10f; // Скорость перехода в присед
    public LayerMask obstacleLayer; // Слой для проверки препятствий над головой

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
    private float xRotation = 0f;
    private float standingCheckRadius = 0.2f;
    private float originalCameraHeight; // Начальная высота камеры
    private Vector3 cameraOffset; // Смещение камеры относительно центра контроллера

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalHeight = controller.height;
        currentHeight = originalHeight;
        targetHeight = originalHeight;
        currentSpeed = walkSpeed;
        targetSpeed = walkSpeed;

        // Сохраняем начальное положение камеры
        originalCameraHeight = playerCamera.localPosition.y;
        cameraOffset = playerCamera.localPosition;

        // Получаем компонент аниматора, если он не был назначен
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        
        // Отключаем Root Motion
        if (animator != null)
        {
            animator.applyRootMotion = false;
        }
    }

    void Update()
    {
        HandleCrouchInput();
        MovePlayer();
        RotateCamera();
        HandleJump();
        UpdateCrouch();
    }

    void HandleCrouchInput()
    {
        // Присед при удержании левого Control
        wantsToCrouch = Input.GetKey(KeyCode.LeftControl);
    }

    void UpdateCrouch()
    {
        // Проверяем, можно ли встать
        bool canStand = !Physics.SphereCast(
            transform.position + Vector3.up * (crouchedHeight - 0.1f),
            standingCheckRadius,
            Vector3.up,
            out RaycastHit hit,
            originalHeight - crouchedHeight + 0.1f,
            obstacleLayer
        );

        // Определяем целевую высоту
        targetHeight = (wantsToCrouch || !canStand) ? crouchedHeight : originalHeight;
        
        // Плавно меняем текущую высоту
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        controller.height = currentHeight;

        // Обновляем состояние приседа
        isCrouching = Mathf.Abs(currentHeight - crouchedHeight) < 0.01f;

        // Корректируем позицию центра контроллера
        Vector3 center = controller.center;
        center.y = currentHeight / 2f;
        controller.center = center;

        // Обновляем позицию камеры
        Vector3 newCameraPos = cameraOffset;
        newCameraPos.y = Mathf.Lerp(originalCameraHeight * 0.5f, originalCameraHeight, (currentHeight - crouchedHeight) / (originalHeight - crouchedHeight));
        playerCamera.localPosition = newCameraPos;
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Определяем целевую скорость
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        targetSpeed = isCrouching ? crouchSpeed : (isRunning ? runSpeed : walkSpeed);
        
        // Плавно меняем текущую скорость
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);

        // Рассчитываем направление движения
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = Vector3.ClampMagnitude(move, 1f);

        // Обновляем параметры анимации
        if (animator != null)
        {
            float moveAmount = move.magnitude;
            animator.SetBool("IsRunning", isRunning && moveAmount > 0.1f);
            animator.SetFloat("Speed", moveAmount, 0.1f, Time.deltaTime);
            animator.SetBool("IsCrouching", isCrouching);
        }

        // Применяем плавное ускорение и замедление
        moveDirection = Vector3.Lerp(moveDirection, move * currentSpeed, Time.deltaTime * 15f);

        // Применяем гравитацию
        if (controller.isGrounded)
        {
            ySpeed = -2f;
            if (moveDirection.magnitude < 0.1f)
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        // Объединяем движение
        Vector3 finalMove = moveDirection + new Vector3(0, ySpeed, 0);
        controller.Move(finalMove * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded && !isCrouching)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    
    }
   
}



