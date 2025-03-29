using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Ссылка на UI меню паузы
    private bool isPaused = false; // Флаг для отслеживания состояния паузы
    public GameObject player; // Ссылка на объект игрока
    private PlayerMovement playerMovement; // Компонент управления движением игрока

    void Start()
    {
        // Получаем компонент управления движением игрока
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Проверяем, нажата ли клавиша Esc для открытия/закрытия меню паузы
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
        pauseMenuUI.SetActive(true); // Показываем меню паузы
        isPaused = true; // Устанавливаем флаг паузы в true
        Cursor.visible = true; // Показываем курсор
        Cursor.lockState = CursorLockMode.None; // Разблокируем курсор

        // Отключаем управление движением игрока
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Отключаем компонент управления движением
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Скрываем меню паузы
        isPaused = false; // Устанавливаем флаг паузы в false
        Cursor.visible = false; // Скрываем курсор
        Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор

        // Включаем управление движением игрока
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Включаем компонент управления движением
        }
    }

    public bool IsPaused
    {
        get { return isPaused; } // Свойство для доступа к состоянию паузы
    }
}


