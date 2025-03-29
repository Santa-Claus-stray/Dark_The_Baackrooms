using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Slider slider; // Ссылка на слайдер
    public Canvas loadingCanvas; // Ссылка на Canvas загрузки
    public Canvas gameCanvas; // Ссылка на Canvas игры

    private void Start()
    {
        // Запускаем корутину для отображения экрана загрузки
        StartCoroutine(ShowLoadingScreen());
    }

    private IEnumerator ShowLoadingScreen()
    {
        // Сбрасываем значение слайдера
        slider.value = 0;

        // Эмулируем процесс загрузки
        for (float i = 0; i <= 1; i += 0.1f)
        {
            slider.value = i; // Обновляем слайдер
            yield return new WaitForSeconds(0.5f); // Задержка для имитации загрузки
        }

        // После завершения загрузки запускаем игру
        StartGame();
    }

    private void StartGame()
    {
        // Скрываем слайдер и Canvas загрузки
        slider.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(false);

        // Активируем Canvas игры
        if (gameCanvas != null)
        {
            gameCanvas.gameObject.SetActive(true);
        }

        // Здесь вы можете активировать игровые объекты или начать игру
        // Например:
        // GameObject.Find("GameObjectsContainer").SetActive(true);
    }
}
