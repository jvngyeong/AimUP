using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public float PlayerPrefsSensitivity;
    public float Sensitivity;// ���콺 ȸ�� �ӵ�
    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )
    private float yRotate = 0.0f; // ���� ����� Y�� ȸ������ ���� ���� ( �¿� ȸ�� ���� )
    private Vector3 ScreenCenter;
    private void Start()
    {
        PlayerPrefsSensitivity = PlayerPrefs.GetFloat("Sensitivity", 0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        Sensitivity = PlayerPrefsSensitivity * 5;
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        MouseRotation();
    }

    // ���콺�� �����ӿ� ���� ī�޶� ȸ�� ��Ų��.
    void MouseRotation()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * Sensitivity;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        yRotate += yRotateSize;

        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
        float xRotateSize = -Input.GetAxis("Mouse Y") * Sensitivity;
        // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
        // Clamp �� ���� ������ �����ϴ� �Լ�
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -40, 40);
        yRotate = Mathf.Clamp(yRotate, -40, 40); // �¿� ȸ�� ���� �߰�
        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }
}