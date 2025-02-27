using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGameObjectSwitcher : MonoBehaviour
{
    public GameObject object1; // Первый GameObject
    public GameObject object2; // Второй GameObject
    public GameObject object3; // Третий GameObject

    public Button button1; // Первая кнопка
    public Button button2; // Вторая кнопка
    public Button button3; // Третья кнопка

    void Start()
    {
        // Скрываем все объекты, кроме первого
        ShowObject(object1);

        // Привязываем обработчики событий к кнопкам
        button1.onClick.AddListener(() => ShowObject(object1));
        button2.onClick.AddListener(() => ShowObject(object2));
        button3.onClick.AddListener(() => ShowObject(object3));
    }

    // Метод для отображения выбранного GameObject
    private void ShowObject(GameObject objectToShow)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);

        objectToShow.SetActive(true);
    }
}
