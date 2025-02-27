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

        // Убедитесь, что видео будет зациклено
        if (videoPlayer != null)
        {
            videoPlayer.isLooping = true; // Включаем зацикливание
            videoPlayer.Play(); // Запускаем воспроизведение видео
        }
        else
        {
            Debug.LogError("VideoPlayer component not found!");
        }
    }
}
