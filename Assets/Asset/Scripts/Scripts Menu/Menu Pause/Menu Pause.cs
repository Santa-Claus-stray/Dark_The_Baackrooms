using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Ссылка на UI меню паузы
    private bool isPaused = false; // Флаг для отслеживания состояния паузы

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
        Time.timeScale = 0f; // Останавливаем время
        isPaused = true; // Устанавливаем флаг паузы в true
        Cursor.visible = true; // Показываем курсор
        Cursor.lockState = CursorLockMode.None; // Разблокируем курсор
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Скрываем меню паузы
        Time.timeScale = 1f; // Возобновляем время
        isPaused = false; // Устанавливаем флаг паузы в false
        Cursor.visible = false; // Скрываем курсор
        Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор в центре экрана
    }

    public bool IsPaused
    {
        get { return isPaused; } // Свойство для доступа к состоянию паузы
    }
}

