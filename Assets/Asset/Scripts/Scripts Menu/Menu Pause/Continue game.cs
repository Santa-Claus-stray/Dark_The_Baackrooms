using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continuegame : MonoBehaviour
{
    // Метод для продолжения игры
    public void ContinueGame()
    {
        Time.timeScale = 1; // Возвращаем время в норму
        // Здесь можно скрыть меню паузы, если оно открыто
    }
}
