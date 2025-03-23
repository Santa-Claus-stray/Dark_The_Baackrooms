using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // ������ �� UI ���� �����
    private bool isPaused = false; // ���� ��� ������������ ��������� �����

    void Update()
    {
        // ���������, ������ �� ������� Esc ��� ��������/�������� ���� �����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // ���������� ���� �����
        Time.timeScale = 0f; // ������������� �����
        isPaused = true; // ������������� ���� ����� � true
        Cursor.visible = true; // ���������� ������
        Cursor.lockState = CursorLockMode.None; // ������������ ������
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // �������� ���� �����
        Time.timeScale = 1f; // ������������ �����
        isPaused = false; // ������������� ���� ����� � false
        Cursor.visible = false; // �������� ������
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������ � ������ ������
    }

    public bool IsPaused
    {
        get { return isPaused; } // �������� ��� ������� � ��������� �����
    }
}

