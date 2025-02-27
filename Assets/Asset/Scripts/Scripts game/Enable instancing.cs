using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGPUInstancing : MonoBehaviour
{
    public Material material; // ��������, ������� �� ������ ������������ � ������������

    void Start()
    {
        if (material != null)
        {
            // �������� GPU instancing ��� ���������
            material.enableInstancing = true;

            // ��������� �������� � �������
            GetComponent<Renderer>().material = material;
        }
        else
        {
            Debug.LogWarning("�������� �� ��������! ����������, ��������� �������� � ����������.");
        }
    }
}
