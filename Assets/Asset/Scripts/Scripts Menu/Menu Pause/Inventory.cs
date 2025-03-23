using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryUI; // ������ �� UI ���������
    private bool isInventoryOpen = false; // ���� ��� ������������ ��������� ���������
    private PauseMenuController pauseMenuController; // ������ �� ���������� ���� �����

    void Start()
    {
        // �������� ������ �� ���������� ���� �����
        pauseMenuController = FindObjectOfType<PauseMenuController>();
    }

    void Update()
    {
        // ���������, ������ �� ������� I ��� ��������/�������� ���������
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isInventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void OpenInventory()
    {
        if (!pauseMenuController.IsPaused) // ���������, �� ������� �� ���� �����
        {
            inventoryUI.SetActive(true); // ���������� ���������
            isInventoryOpen = true; // ������������� ���� ��������� � true
            EnableGameplayInteractions(false); // ��������� �������������� � �������� ���������
        }
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false); // �������� ���������
        isInventoryOpen = false; // ������������� ���� ��������� � false
        EnableGameplayInteractions(true); // �������� �������������� � �������� ���������
    }

    private void EnableGameplayInteractions(bool enable)
    {
        // ����� �� ������ ��������� ��������������� � �������� ���������
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = enable; // �������� ��� ��������� ���������� �������
        }
    }
}



