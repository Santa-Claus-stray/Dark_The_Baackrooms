using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundClips; // ћассив аудиоклипов дл€ воспроизведени€
    public float interval = 2f; // »нтервал между воспроизведени€ми

    private AudioSource audioSource;
    private float nextPlayTime;
    private int currentSoundIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextPlayTime = Time.time + interval; // ”станавливаем врем€ следующего воспроизведени€
    }

    void Update()
    {
        // ѕровер€ем, пришло ли врем€ воспроизведени€
        if (Time.time >= nextPlayTime)
        {
            PlayNextSound();
            nextPlayTime = Time.time + interval; // ќбновл€ем врем€ следующего воспроизведени€
        }
    }

    void PlayNextSound()
    {
        if (soundClips.Length == 0) return; // ѕровер€ем, есть ли звуки дл€ воспроизведени€

        audioSource.clip = soundClips[currentSoundIndex]; // ”станавливаем текущий аудиоклип
        audioSource.Play(); // ¬оспроизводим звук

        // ќбновл€ем индекс дл€ следующего звука
        currentSoundIndex = (currentSoundIndex + 1) % soundClips.Length; // ÷иклический переход по массиву
    }
}
