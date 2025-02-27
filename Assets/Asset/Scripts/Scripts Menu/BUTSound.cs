using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public Button button; // ������ �� ������
    public AudioClip sound; // ��������� ��� ���������������
    private AudioSource audioSource; // ��������� AudioSource ��� ��������������� �����

    private void Start()
    {
        // �������� ��������� AudioSource, ���� �� �� ����������
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;

        // ��������� ����� OnButtonClick � ������� ������� ������
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // ������������� ����
        audioSource.Play();
    }
}
