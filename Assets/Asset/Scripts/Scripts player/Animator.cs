using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;

    // Названия параметров анимации
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    void Start()
    {
        // Получаем компонент Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Проверяем, удерживается ли кнопка для ходьбы (например, W)
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool(IsWalking, true);
            animator.SetBool(IsRunning, false);
        }
        // Проверяем, удерживается ли кнопка для бега (например, Shift + W)
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsRunning, true);
        }
        else
        {
            // Если ни одна из клавиш не нажата, возвращаемся к состоянию Idle
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsRunning, false);
        }
    }
}










