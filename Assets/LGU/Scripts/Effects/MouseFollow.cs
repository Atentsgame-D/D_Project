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
        Vector3 mousePosition = Mouse.current.position.ReadValue();     // ���콺�� ��ġ�� ��ũ�� ��ǥ��� �޾ƿ�(������ ȭ���� ���� �Ʒ�, ũ��� ȭ�� �ػ�)
        mousePosition.z = distance;

        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector3.Lerp(transform.position, target, speed);
    }
}
