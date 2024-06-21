using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public float PlayerPrefsSensitivity;
    public float Sensitivity;// 마우스 회전 속도
    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )
    private float yRotate = 0.0f; // 내부 사용할 Y축 회전량은 별도 정의 ( 좌우 회전 방향 )
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

    // 마우스의 움직임에 따라 카메라를 회전 시킨다.
    void MouseRotation()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * Sensitivity;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        yRotate += yRotateSize;

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * Sensitivity;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        // Clamp 는 값의 범위를 제한하는 함수
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -40, 40);
        yRotate = Mathf.Clamp(yRotate, -40, 40); // 좌우 회전 제한 추가
        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
    }
}