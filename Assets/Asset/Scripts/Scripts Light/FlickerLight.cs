using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource; // Ссылка на источник света
    public float flickerDuration = 0.1f; // Длительность мерцания
    public float flickerInterval = 0.5f; // Интервал между мерцаниями

    private void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>(); // Получаем компонент Light, если он не установлен
        }

        StartCoroutine(FlickerLight());
    }

    private System.Collections.IEnumerator FlickerLight()
    {
        while (true) // Бесконечный цикл для мерцания
        {
            lightSource.enabled = false; // Выключаем свет
            yield return new WaitForSeconds(flickerDuration); // Ждем длительность мерцания

            lightSource.enabled = true; // Включаем свет
            yield return new WaitForSeconds(flickerInterval); // Ждем интервал между мерцаниями
        }
    }
}
