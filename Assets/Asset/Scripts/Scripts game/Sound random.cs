using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundClips; // ������ ����������� ��� ���������������
    public float interval = 2f; // �������� ����� �����������������

    private AudioSource audioSource;
    private float nextPlayTime;
    private int currentSoundIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextPlayTime = Time.time + interval; // ������������� ����� ���������� ���������������
    }

    void Update()
    {
        // ���������, ������ �� ����� ���������������
        if (Time.time >= nextPlayTime)
        {
            PlayNextSound();
            nextPlayTime = Time.time + interval; // ��������� ����� ���������� ���������������
        }
    }

    void PlayNextSound()
    {
        if (soundClips.Length == 0) return; // ���������, ���� �� ����� ��� ���������������

        audioSource.clip = soundClips[currentSoundIndex]; // ������������� ������� ���������
        audioSource.Play(); // ������������� ����

        // ��������� ������ ��� ���������� �����
        currentSoundIndex = (currentSoundIndex + 1) % soundClips.Length; // ����������� ������� �� �������
    }
}
