using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CharacterController controller; // ������ �� ��������� CharacterController
    public Transform playerCamera; // ������ �� ������ ������
    public float walkSpeed = 5f; // �������� ������
    public float runSpeed = 10f; // �������� ����
    public float crouchSpeed = 2.5f; // �������� ��� ����������
    public float jumpHeight = 1f; // ������ ������
    public float gravity = -9.81f; // ���� �������
    public float mouseSensitivity = 100f; // ���������������� ����
    

    private float ySpeed; // �������� �� ��� Y
    private bool isCrouching = false; // ���� ��� ����������� ��������� ����������
    private float originalHeight; // ������������ ������ �����������
    private float crouchedHeight = 0.5f; // ������ ��� ����������
    

    private float xRotation = 0f; // ���������� ��� �������� ���� �������� �� ��� X

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ������ ������ � ������������� ��� � ������ ������
        originalHeight = controller.height; // ���������� ������������ ������ �����������
    }

    void Update()
    {
        MovePlayer(); // ��������� �������� ������
        RotateCamera(); // ��������� �������� ������ � ���������
        HandleJumpAndCrouch(); // ��������� ������ � ����������
    }

    void MovePlayer()
    {
        // ��������� �����
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // ����������� �������� ��������
        float speed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // ���������� �������� � �����������
        controller.Move(move * speed * Time.deltaTime);

        // ���������� ����������
        ySpeed += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, ySpeed, 0) * Time.deltaTime);
    }

    void RotateCamera()
    {
        // ��������� ����� ����
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // �������� ������ �� ��� Y (�������������� ��������)
        transform.Rotate(Vector3.up * mouseX);

        // �������� ������ �� ��� X (������������ ��������)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ����������� �������� �� ��� X
        playerCamera.localEulerAngles = new Vector3(xRotation, 0f, 0f); // ��������� �������� � ������

        // �������� ��������� � ����������� ������� ������ (�� ��� Y)
        Vector3 direction = playerCamera.forward;
        direction.y = 0; // ������� ������������ ������������, ����� �������� �� ����������
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // ������� ��������
        }
    }

    void HandleJumpAndCrouch()
    {
        // ������
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -2f * gravity); // ������ ��������� �������� ������
        }

        // ����������
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchedHeight; // ��������� ������ �����������
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = originalHeight; // ��������������� ������������ ������ �����������
        }
    }
}



