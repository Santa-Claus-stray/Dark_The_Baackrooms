using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton; // Ссылка на кнопку

    private void Start()
    {
        // Проверяем, что кнопка назначена
        if (myButton != null)
        {
            // Добавляем метод OnButtonClick к событию нажатия кнопки
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("Кнопка не назначена в инспекторе!");
        }
    }

    // Метод, который будет вызываться при нажатии кнопки
    private void OnButtonClick()
    {
        Debug.Log("Кнопка нажата!");
    }
}
