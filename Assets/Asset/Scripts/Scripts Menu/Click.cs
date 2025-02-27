using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button myButton; // ������ �� ������

    private void Start()
    {
        // ���������, ��� ������ ���������
        if (myButton != null)
        {
            // ��������� ����� OnButtonClick � ������� ������� ������
            myButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("������ �� ��������� � ����������!");
        }
    }

    // �����, ������� ����� ���������� ��� ������� ������
    private void OnButtonClick()
    {
        Debug.Log("������ ������!");
    }
}
