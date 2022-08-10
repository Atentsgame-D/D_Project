using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    public float distance = 10.0f;
    [Range(0.1f, 1.0f)]
    public float speed = 0.1f;
    private void Update()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();     // 마우스의 위치를 스크린 좌표계로 받아옴(원점은 화면의 왼쪽 아래, 크기는 화면 해상도)
        mousePosition.z = distance;

        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector3.Lerp(transform.position, target, speed);
    }
}
