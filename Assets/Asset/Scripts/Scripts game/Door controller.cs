using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AudioClip openSound; // ���� �������� �����
    public AudioClip lockedSound; // ���� ��������������� �����
    public bool isLocked = true; // ��������� ����� (������������� ��� ���)
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� ����� ������ � ���� ��������
        {
            if (Input.GetKeyDown(KeyCode.E)) // ��������� ������� ������� E
            {
                if (isLocked)
                {
                    PlayLockedSound();
                }
                else
                {
                    OpenDoor();
                }
            }
        }
    }

    private void OpenDoor()
    {
        // ����� �� ������ �������� �������� �������� ����� ��� ������ �����������
        transform.Rotate(0, 90, 0); // ������ �������� ����� �� 90 ��������
        PlayOpenSound();
    }

    private void PlayOpenSound()
    {
        audioSource.clip = openSound;
        audioSource.Play();
    }

    private void PlayLockedSound()
    {
        audioSource.clip = lockedSound;
        audioSource.Play();
    }
}



