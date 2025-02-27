using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoLooper : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // ���������, ��� ����� ����� ���������
        if (videoPlayer != null)
        {
            videoPlayer.isLooping = true; // �������� ������������
            videoPlayer.Play(); // ��������� ��������������� �����
        }
        else
        {
            Debug.LogError("VideoPlayer component not found!");
        }
    }
}
