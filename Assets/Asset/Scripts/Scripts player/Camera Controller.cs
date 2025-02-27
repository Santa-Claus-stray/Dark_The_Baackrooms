using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CharacterController controller; // Ссылка на компонент CharacterController
    public Transform playerCamera; // Ссылка на камеру игрока
    public float walkSpeed = 5f; // Скорость ходьбы
    public float runSpeed = 10f; // Скорость бега
    public float crouchSpeed = 2.5f; // Скорость при приседании
    public float jumpHeight = 1f; // Высота прыжка
    public float gravity = -9.81f; // Сила тяжести
    public float mouseSensitivity = 100f; // Чувствительность мыши
    

    private float ySpeed; // Скорость по оси Y
    private bool isCrouching = false; // Флаг для определения состояния приседания
    private float originalHeight; // Оригинальная высота контроллера
    private float crouchedHeight = 0.5f; // Высота при приседании
    

    private float xRotation = 0f; // Переменная для хранения угла вращения по оси X

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Скрыть курсор и заблокировать его в центре экрана
        originalHeight = controller.height; // Запоминаем оригинальную высоту контроллера
    }

    void Update()
    {
        MovePlayer(); // Обработка движения игрока
        RotateCamera(); // Обработка вращения камеры и персонажа
        HandleJumpAndCrouch(); // Обработка прыжка и приседания
    }

    void MovePlayer()
    {
        // Получение ввода
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Определение скорости движения
        float speed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Применение скорости к контроллеру
        controller.Move(move * speed * Time.deltaTime);

        // Применение гравитации
        ySpeed += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, ySpeed, 0) * Time.deltaTime);
    }

    void RotateCamera()
    {
        // Получение ввода мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Вращение камеры по оси Y (горизонтальное вращение)
        transform.Rotate(Vector3.up * mouseX);

        // Вращение камеры по оси X (вертикальное вращение)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничение вращения по оси X
        playerCamera.localEulerAngles = new Vector3(xRotation, 0f, 0f); // Применяем вращение к камере

        // Вращение персонажа в направлении взгляда камеры (по оси Y)
        Vector3 direction = playerCamera.forward;
        direction.y = 0; // Убираем вертикальную составляющую, чтобы персонаж не наклонялся
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Плавное вращение
        }
    }

    void HandleJumpAndCrouch()
    {
        // Прыжок
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity); // Расчет начальной скорости прыжка
        }

        // Приседание
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchedHeight; // Уменьшаем высоту контроллера
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = originalHeight; // Восстанавливаем оригинальную высоту контроллера
        }
    }
}



