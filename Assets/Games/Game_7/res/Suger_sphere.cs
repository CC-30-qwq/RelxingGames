using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suger_sphere : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;

    [Header("Drag Settings")]
    [SerializeField] private float releaseForce = 5f;
    [SerializeField] private bool useLerp = true;
    [SerializeField] private float lerpSpeed = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        Vector3 mousePos = GetMouseWorldPos();
        offset = transform.position - mousePos;
        isDragging = true;

        // 开始拖拽时暂停物理模拟
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPos();
            Vector3 targetPosition = mousePos + offset;

            if (useLerp)
            {
                // 使用插值平滑移动
                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
                rb.MovePosition(newPosition);
            }
            else
            {
                // 直接移动到目标位置
                rb.MovePosition(targetPosition);
            }
        }
    }

    void OnMouseUp()
    {
            isDragging = false;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !isDragging)
        {
        }
    }
}