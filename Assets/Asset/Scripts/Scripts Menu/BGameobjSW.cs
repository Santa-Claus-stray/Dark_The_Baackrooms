using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGameObjectSwitcher : MonoBehaviour
{
    public GameObject object1; // ������ GameObject
    public GameObject object2; // ������ GameObject
    public GameObject object3; // ������ GameObject

    public Button button1; // ������ ������
    public Button button2; // ������ ������
    public Button button3; // ������ ������

    void Start()
    {
        // �������� ��� �������, ����� �������
        ShowObject(object1);

        // ����������� ����������� ������� � �������
        button1.onClick.AddListener(() => ShowObject(object1));
        button2.onClick.AddListener(() => ShowObject(object2));
        button3.onClick.AddListener(() => ShowObject(object3));
    }

    // ����� ��� ����������� ���������� GameObject
    private void ShowObject(GameObject objectToShow)
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);

        objectToShow.SetActive(true);
    }
}
