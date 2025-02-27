using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource audioSource; // Ссылка на компонент AudioSource

    private void Start()
    {
        // Устанавливаем начальную громкость
        audioSource.volume = 0.3f; // Значение от 0.0 до 1.0

    }   
}