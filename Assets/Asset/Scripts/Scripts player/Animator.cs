using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;

    // �������� ���������� ��������
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    void Start()
    {
        // �������� ��������� Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ���������, ������������ �� ������ ��� ������ (��������, W)
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool(IsWalking, true);
            animator.SetBool(IsRunning, false);
        }
        // ���������, ������������ �� ������ ��� ���� (��������, Shift + W)
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsRunning, true);
        }
        else
        {
            // ���� �� ���� �� ������ �� ������, ������������ � ��������� Idle
            animator.SetBool(IsWalking, false);
            animator.SetBool(IsRunning, false);
        }
    }
}










