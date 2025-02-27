using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AudioClip openSound; // Звук открытия двери
    public AudioClip lockedSound; // Звук заблокированной двери
    public bool isLocked = true; // Состояние двери (заблокирована или нет)
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что игрок входит в зону триггера
        {
            if (Input.GetKeyDown(KeyCode.E)) // Проверяем нажатие клавиши E
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
        // Здесь вы можете добавить анимацию открытия двери или логику перемещения
        transform.Rotate(0, 90, 0); // Пример поворота двери на 90 градусов
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



