using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource; // Ссылка на компонент AudioSource

    private void Start()
    {
        // Проверяем, что AudioSource назначен
        if (audioSource != null)
        {
            audioSource.loop = true; // Устанавливаем зацикливание
            audioSource.Play(); // Начинаем воспроизведение музыки
        }
        else
        {
            Debug.LogWarning("AudioSource не назначен в инспекторе!");
        }
    }
}