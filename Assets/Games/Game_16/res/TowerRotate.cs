using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotate : MonoBehaviour
{
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float sensitivity = 1f;

    private float startAngle;
    [SerializeField] private float currentRotation;
    [SerializeField] float timer = 0;

    public Transform flag;
    public float raiseHeight = 2f;
    public float duration = 1;

    bool finished;

    public bool finish;

    AudioSource audioSource;
    public AudioSource ground;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        finished = false;
        currentRotation = NormalizeAngle(transform.eulerAngles.z);
    }

    private void Update()
    {
        if (Mathf.Abs(currentRotation) < 1f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (timer > 1f)
        {
            finished = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (timer < 2)
            {
                audioSource.Play();
                finish = true;
                flag.DOMoveY(raiseHeight, duration).SetRelative(true);
            }
            timer = 3;
        }
    }

    void OnMouseDown()
    {
        if (!finished)
        {
            ground.Play();

            // 获取初始鼠标位置角度
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - transform.position).normalized;
            startAngle = Vector2.SignedAngle(Vector2.right, direction) - currentRotation;
        }
    }

    private void OnMouseUp()
    {
        ground.Stop();
    }

    void OnMouseDrag()
    {
        if (!finished)
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