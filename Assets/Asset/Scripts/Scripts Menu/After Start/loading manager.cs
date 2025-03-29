using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� �������� �������� ��� ����������
using System.Collections;

namespace MyGame.Loading
{
    public class LoadingManager : MonoBehaviour
    {
        public Slider slider; // ������ �� �������
        public Canvas loadingCanvas; // ������ �� Canvas ��������
        public string levelToLoad; // ��� �����, ������� ����� ���������

        private void Start()
        {
            // �������� Canvas �������� � ������
            loadingCanvas.gameObject.SetActive(false);
        }

        public void OnStartButtonPressed()
        {
            // ��������� �������� ��� �������� ����� � ������� ��������
            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            // ���������� Canvas ��������
            loadingCanvas.gameObject.SetActive(true);

            // ���������� �������� ��������
            slider.value = 0;

            // �������� ����������� �������� �����
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);

            // ���� ����� �����������, ��������� �������
            while (!asyncOperation.isDone)
            {
                // ��������� �������� �������� � ����������� �� ��������� ��������
                slider.value = asyncOperation.progress;
                yield return null; // ���� ���������� �����
            }

            // �������� Canvas �������� ����� ���������� ��������
            loadingCanvas.gameObject.SetActive(false);
        }
    }
}








