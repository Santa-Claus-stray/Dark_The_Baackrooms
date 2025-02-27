using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flashing : MonoBehaviour
{
    private Light _light; // Теперь можно назначить через инспектор
    private float start_intensity;
    public float min_intensity = 0.3f;
    public float max_intensity = 1.5f;
    public float noise_speed = 0.15f;
    public bool flicker_ON;
    public bool random_timer;
    public float random_timer_value_MIN = 5f;
    public float random_timer_value_MAX = 20f;
    private float random_timer_value;
    public float start_timer_value = 0.1f;
    private Coroutine flickerCoroutine;

    void Start()
    {
        if (_light == null)
        {
            _light = GetComponent<Light>();
            if (_light == null)
            {
                Debug.LogError("Light component is missing on this GameObject.");
                return;
            }
        }
        start_intensity = _light.intensity;
    }

    void Update()
    {
        if (flicker_ON)
        {
            _light.intensity = Mathf.Lerp(min_intensity, max_intensity,
                Mathf.PerlinNoise(10, Time.time / noise_speed));
        }

        if (Input.GetMouseButtonDown(0) && flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(StartFlickering());
        }
    }

    IEnumerator StartFlickering()
    {
        yield return new WaitForSeconds(start_timer_value);
        Debug.Log("Starting flicker...");

        while (true)
        {
            random_timer_value = Random.Range(random_timer_value_MIN, random_timer_value_MAX);
            yield return new WaitForSeconds(random_timer_value);

            flicker_ON = !flicker_ON; // Переключаем состояние
        }
    }

    private void OnDisable()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null; // Завершение корутины
        }
    }
}


