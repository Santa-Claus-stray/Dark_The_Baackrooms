using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public Slider slider; // ������ �� �������
    public Canvas loadingCanvas; // ������ �� Canvas ��������
    public Canvas gameCanvas; // ������ �� Canvas ����

    private void Start()
    {
        // ��������� �������� ��� ����������� ������ ��������
        StartCoroutine(ShowLoadingScreen());
    }

    private IEnumerator ShowLoadingScreen()
    {
        // ���������� �������� ��������
        slider.value = 0;

        // ��������� ������� ��������
        for (float i = 0; i <= 1; i += 0.1f)
        {
            slider.value = i; // ��������� �������
            yield return new WaitForSeconds(0.5f); // �������� ��� �������� ��������
        }

        // ����� ���������� �������� ��������� ����
        StartGame();
    }

    private void StartGame()
    {
        // �������� ������� � Canvas ��������
        slider.gameObject.SetActive(false);
        loadingCanvas.gameObject.SetActive(false);

        // ���������� Canvas ����
        if (gameCanvas != null)
        {
            gameCanvas.gameObject.SetActive(true);
        }

        // ����� �� ������ ������������ ������� ������� ��� ������ ����
        // ��������:
        // GameObject.Find("GameObjectsContainer").SetActive(true);
    }
}
