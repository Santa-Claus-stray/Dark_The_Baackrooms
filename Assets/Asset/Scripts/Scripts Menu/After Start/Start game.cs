using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Для работы с UI
using System.Collections; // Для IEnumerator

public class MenuController : MonoBehaviour
{
    public GameObject loadingCanvas; // Ссылка на Canvas загрузки
    public Slider loadingSlider; // Ссылка на Slider для отображения прогресса

    public void StartGame()
    {
        // Запускаем корутину для загрузки уровня
        StartCoroutine(LoadLevel("Level 0"));
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        // Включаем экран загрузки
        loadingCanvas.SetActive(true);

        // Начинаем асинхронную загрузку сцены
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Ожидаем завершения загрузки, обновляя прогресс
        while (!asyncOperation.isDone)
        {
            // Обновляем слайдер с прогрессом загрузки
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingSlider.value = progress;

            yield return null; // Ждем следующего кадра
        }

        // Выключаем экран загрузки после завершения
        loadingCanvas.SetActive(false);
    }
}

