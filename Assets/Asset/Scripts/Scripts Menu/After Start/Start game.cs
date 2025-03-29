using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��� ������ � UI
using System.Collections; // ��� IEnumerator

public class MenuController : MonoBehaviour
{
    public GameObject loadingCanvas; // ������ �� Canvas ��������
    public Slider loadingSlider; // ������ �� Slider ��� ����������� ���������

    public void StartGame()
    {
        // ��������� �������� ��� �������� ������
        StartCoroutine(LoadLevel("Level 0"));
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        // �������� ����� ��������
        loadingCanvas.SetActive(true);

        // �������� ����������� �������� �����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // ������� ���������� ��������, �������� ��������
        while (!asyncOperation.isDone)
        {
            // ��������� ������� � ���������� ��������
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingSlider.value = progress;

            yield return null; // ���� ���������� �����
        }

        // ��������� ����� �������� ����� ����������
        loadingCanvas.SetActive(false);
    }
}

