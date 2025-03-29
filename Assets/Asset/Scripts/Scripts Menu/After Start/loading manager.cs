using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Не забудьте добавить эту библиотеку
using System.Collections;

namespace MyGame.Loading
{
    public class LoadingManager : MonoBehaviour
    {
        public Slider slider; // Ссылка на слайдер
        public Canvas loadingCanvas; // Ссылка на Canvas загрузки
        public string levelToLoad; // Имя сцены, которую нужно загрузить

        private void Start()
        {
            // Скрываем Canvas загрузки в начале
            loadingCanvas.gameObject.SetActive(false);
        }

        public void OnStartButtonPressed()
        {
            // Запускаем корутину для загрузки сцены с экраном загрузки
            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            // Показываем Canvas загрузки
            loadingCanvas.gameObject.SetActive(true);

            // Сбрасываем значение слайдера
            slider.value = 0;

            // Начинаем асинхронную загрузку сцены
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);

            // Пока сцена загружается, обновляем слайдер
            while (!asyncOperation.isDone)
            {
                // Обновляем значение слайдера в зависимости от прогресса загрузки
                slider.value = asyncOperation.progress;
                yield return null; // Ждем следующего кадра
            }

            // Скрываем Canvas загрузки после завершения загрузки
            loadingCanvas.gameObject.SetActive(false);
        }
    }
}








