using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light lightSource; // ������ �� �������� �����
    public float flickerDuration = 0.1f; // ������������ ��������
    public float flickerInterval = 0.5f; // �������� ����� ����������

    private void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>(); // �������� ��������� Light, ���� �� �� ����������
        }

        StartCoroutine(FlickerLight());
    }

    private System.Collections.IEnumerator FlickerLight()
    {
        while (true) // ����������� ���� ��� ��������
        {
            lightSource.enabled = false; // ��������� ����
            yield return new WaitForSeconds(flickerDuration); // ���� ������������ ��������

            lightSource.enabled = true; // �������� ����
            yield return new WaitForSeconds(flickerInterval); // ���� �������� ����� ����������
        }
    }
}
