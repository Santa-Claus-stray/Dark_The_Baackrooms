using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource audioSource; // ������ �� ��������� AudioSource

    private void Start()
    {
        // ������������� ��������� ���������
        audioSource.volume = 0.3f; // �������� �� 0.0 �� 1.0

    }   
}