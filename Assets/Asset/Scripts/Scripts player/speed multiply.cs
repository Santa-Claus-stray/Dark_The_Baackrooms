using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // Скорость ходьбы
    public float sprintSpeed = 10f; // Скорость бега
    public float squatSpeedMultiplier = 2f; // Множитель скорости при приседании

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

        // Определение скорости в зависимости от состояния
        float speed = isSquatting ? walkSpeed * squatSpeedMultiplier : walkSpeed;

        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    void HandleSquat()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) // Приседание при нажатии клавиши "Left Control"
        {
            isSquatting = true;
            // Можно добавить анимацию или изменение положения камеры
            Debug.Log("Приседание начато!");
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // Возврат в нормальное состояние при отпускании клавиши
        {
            isSquatting = false;
            Debug.Log("Приседание завершено!");
        }
    }
}
