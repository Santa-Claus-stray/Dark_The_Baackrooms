using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource; // ������ �� ��������� AudioSource

    private void Start()
    {
        // ���������, ��� AudioSource ��������
        if (audioSource != null)
        {
            audioSource.loop = true; // ������������� ������������
            audioSource.Play(); // �������� ��������������� ������
        }
        else
        {
            Debug.LogWarning("AudioSource �� �������� � ����������!");
        }
    }
}