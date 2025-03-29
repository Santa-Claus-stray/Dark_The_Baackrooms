using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // ������ �� UI ���� �����
    private bool isPaused = false; // ���� ��� ������������ ��������� �����
    public GameObject player; // ������ �� ������ ������
    private PlayerMovement playerMovement; // ��������� ���������� ��������� ������

    void Start()
    {
        // �������� ��������� ���������� ��������� ������
        playerMovement = player.GetComponent<PlayerMovement>();
    }

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
        isPaused = true; // ������������� ���� ����� � true
        Cursor.visible = true; // ���������� ������
        Cursor.lockState = CursorLockMode.None; // ������������ ������

        // ��������� ���������� ��������� ������
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // ��������� ��������� ���������� ���������
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // �������� ���� �����
        isPaused = false; // ������������� ���� ����� � false
        Cursor.visible = false; // �������� ������
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������

        // �������� ���������� ��������� ������
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // �������� ��������� ���������� ���������
        }
    }

    public bool IsPaused
    {
        get { return isPaused; } // �������� ��� ������� � ��������� �����
    }
}


