using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // �������� ������
    public float sprintSpeed = 10f; // �������� ����
    public float squatSpeedMultiplier = 2f; // ��������� �������� ��� ����������

    private Rigidbody rb;
    private bool isSquatting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        HandleSquat();
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // ����������� �������� � ����������� �� ���������
        float speed = isSquatting ? walkSpeed * squatSpeedMultiplier : walkSpeed;

        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    void HandleSquat()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // ���������� ��� ������� ������� "Left Control"
        {
            isSquatting = true;
            // ����� �������� �������� ��� ��������� ��������� ������
            Debug.Log("���������� ������!");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // ������� � ���������� ��������� ��� ���������� �������
        {
            isSquatting = false;
            Debug.Log("���������� ���������!");
        }
    }
}
