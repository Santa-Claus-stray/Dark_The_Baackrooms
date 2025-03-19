using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody; // Ссылка на Transform игрока
    public float mouseSensitivity = 100f;
    public float originalCameraHeight = 1.6f;
    public float crouchedCameraHeight = 0.8f;
    public float cameraTransitionSpeed = 10f;
    public float jumpCameraOffset = 0.5f; // Смещение камеры при прыжке

    private float xRotation = 0f;
    private float currentCameraHeight;
    private float targetCameraHeight;
    private Vector3 cameraOffset;
    private float jumpCameraHeight;
    private bool isJumping = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentCameraHeight = originalCameraHeight;
        targetCameraHeight = originalCameraHeight;
        cameraOffset = transform.localPosition;
        jumpCameraHeight = originalCameraHeight + jumpCameraOffset;
    }

    void Update()
    {
        RotateCamera();
        UpdateCameraHeight();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Вращаем камеру вверх-вниз
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращаем персонажа влево-вправо
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    void UpdateCameraHeight()
    {
        // Плавно меняем высоту камеры
        currentCameraHeight = Mathf.Lerp(currentCameraHeight, targetCameraHeight, Time.deltaTime * cameraTransitionSpeed);
        
        Vector3 newCameraPos = cameraOffset;
        newCameraPos.y = currentCameraHeight;
        transform.localPosition = newCameraPos;
    }

    // Метод для изменения высоты камеры при приседании
    public void SetCrouchState(bool isCrouching)
    {
        targetCameraHeight = isCrouching ? crouchedCameraHeight : originalCameraHeight;
    }

    public void SetJumpState(bool jumping)
    {
        isJumping = jumping;
        targetCameraHeight = jumping ? jumpCameraHeight : originalCameraHeight;
    }
}



