using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Персонаж
    public Vector3 offset; // Смещение камеры

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}

