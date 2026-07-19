using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antenna : MonoBehaviour
{
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float sensitivity = 1f;

    private float startAngle;
    public float currentRotation;

    public AudioSource one;
    public AudioSource two;
    public AudioSource three;
    bool finifhed;

    private void Update()
    {
        if (currentRotation >= -60)
        {
            if (!one.isPlaying)
            {
                one.Play();
            }
            if (currentRotation >= 0)
            {
                one.Stop();
                if (!two.isPlaying)
                {
                    two.Play();
                }
                if (currentRotation == 60)
                {
                    two.Stop();
                    if (!finifhed)
                    {
                        finifhed = true;
                        three.Play();
                    }
                }
            }
        }
    }

    void Start()
    {
        currentRotation = NormalizeAngle(transform.eulerAngles.z);
    }

    void OnMouseDown()
    {
        // 获取初始鼠标位置角度
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        startAngle = Vector2.SignedAngle(Vector2.right, direction) - currentRotation;
    }

    void OnMouseDrag()
    {
        // 获取当前鼠标位置角度
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        float mouseAngle = Vector2.SignedAngle(Vector2.right, direction);

        // 计算旋转角度
        float newRotation = mouseAngle - startAngle;
        newRotation = NormalizeAngle(newRotation);
        newRotation = Mathf.Clamp(newRotation, minAngle, maxAngle);

        // 应用旋转
        if (currentRotation < maxAngle)
        {
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
            currentRotation = newRotation;
        }
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180) angle -= 360;
        return angle;
    }
}