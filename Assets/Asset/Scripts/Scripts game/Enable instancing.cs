using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGPUInstancing : MonoBehaviour
{
    public Material material; // Материал, который вы хотите использовать с инстансингом

    void Start()
    {
        if (material != null)
        {
            // Включаем GPU instancing для материала
            material.enableInstancing = true;

            // Применяем материал к объекту
            GetComponent<Renderer>().material = material;
        }
        else
        {
            Debug.LogWarning("Материал не назначен! Пожалуйста, назначьте материал в инспекторе.");
        }
    }
}
