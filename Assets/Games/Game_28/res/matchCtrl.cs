using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchCtrl : MonoBehaviour
{
    private Vector2 lastPosition;
    public Vector2 velocity;
    private bool isDragging = false;

    public Collider2D collider2D;
    public float targetV;

    private void Update()
    {
        if (isDragging && velocity.magnitude > targetV)
        {
            collider2D.enabled = true;
        }
        else
        {
            collider2D.enabled = false;
        }

        if (isDragging)
        {
            Debug.Log($"当前速度: {GetSpeed():F2} m/s");
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        lastPosition = GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector2 currentMousePos = GetMouseWorldPos();

        // 移动物体
        transform.position = currentMousePos;

        // 计算速度
        velocity = (currentMousePos - lastPosition) / Time.deltaTime;

        // 更新上一帧位置
        lastPosition = currentMousePos;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    // 获取当前速度
    public Vector2 GetVelocity()
    {
        return velocity;
    }

    // 获取速度大小
    public float GetSpeed()
    {
        return velocity.magnitude;
    }

    // 获取鼠标世界坐标
    private Vector2 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}