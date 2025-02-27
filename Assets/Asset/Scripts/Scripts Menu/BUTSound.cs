using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button button; // Ссылка на кнопку
    public AudioClip sound; // Аудиоклип для воспроизведения
    private AudioSource audioSource; // Компонент AudioSource для воспроизведения звука

    private void Start()
    {
        // Получаем компонент AudioSource, если он не установлен
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;

        // Добавляем метод OnButtonClick к событию нажатия кнопки
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Воспроизводим звук
        audioSource.Play();
    }
}
