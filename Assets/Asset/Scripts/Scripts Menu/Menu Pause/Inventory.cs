using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryUI; // Ссылка на UI инвентаря
    private bool isInventoryOpen = false; // Флаг для отслеживания состояния инвентаря
    private PauseMenuController pauseMenuController; // Ссылка на контроллер меню паузы

    void Start()
    {
        // Получаем ссылку на контроллер меню паузы
        pauseMenuController = FindObjectOfType<PauseMenuController>();
    }

    void Update()
    {
        // Проверяем, нажата ли клавиша I для открытия/закрытия инвентаря
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
        if (!pauseMenuController.IsPaused) // Проверяем, не активно ли меню паузы
        {
            inventoryUI.SetActive(true); // Показываем инвентарь
            isInventoryOpen = true; // Устанавливаем флаг инвентаря в true
            EnableGameplayInteractions(false); // Отключаем взаимодействие с игровыми объектами
        }
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false); // Скрываем инвентарь
        isInventoryOpen = false; // Устанавливаем флаг инвентаря в false
        EnableGameplayInteractions(true); // Включаем взаимодействие с игровыми объектами
    }

    private void EnableGameplayInteractions(bool enable)
    {
        // Здесь вы можете управлять взаимодействием с игровыми объектами
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = enable; // Включаем или отключаем управление игроком
        }
    }
}



