using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openCork : MonoBehaviour
{
    public float targetScale = 1;
    bool outed;
    float timer;

    AudioSource audioSource;

    [Header("拉伸参数")]
    public float sensitivity = 0.1f;      // 鼠标灵敏度
    public float maxStretch = 3f;         // 最大拉伸倍数
    public float minStretch = 1f;         // 最小拉伸倍数
    public float elasticity = 8f;         // 回弹速度

    [Header("移动参数")]
    public float moveSpeed = 0.5f;        // 上移速度系数
    public float duration;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isPulling = false;
    private float lastMouseY;             // 上一帧鼠标Y位置
    private float accumulatedPull;        // 累计拉动量

    public Vector3 openoffset;
    public Transform top;
    public GameObject bubble;

    public bool open;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        open = false;
        outed = false;
        originalScale = transform.localScale;
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (outed)
        {
            timer += Time.deltaTime;
            if (timer < duration)
            {
                transform.DOMoveY(transform.position.y + moveSpeed, duration);
                if (!open)
                {
                    audioSource.Play();
                    open = true;
                    Invoke("effectInstantiate", 0.5f);
                }
            }
        }
    }

    private void effectInstantiate()
    {
        GameObject bubbles = Instantiate(bubble, top.position, top.rotation);
    }

    void OnMouseDown()
    {
        isPulling = true;
        lastMouseY = Input.mousePosition.y;
        accumulatedPull = 0f;
    }

    void OnMouseDrag()
    {
        if (transform.localScale.y > targetScale)
        {
            outed = true;
        }
        else
        {
            if (!isPulling) return;

            // 获取当前鼠标Y位置
            float currentMouseY = Input.mousePosition.y;

            // 计算鼠标Y轴移动距离（向上移动为正）
            float deltaY = currentMouseY - lastMouseY;

            // 计算拉动量
            float pullAmount = deltaY * sensitivity;

            // 累计拉动量
            accumulatedPull += pullAmount;

            // 计算拉伸比例（0到1之间）
            float stretchRatio = Mathf.Clamp01(accumulatedPull / 300f); // 300是参考值，可调整

            // 应用拉伸效果
            ApplyStretch(stretchRatio);

            // 更新上一帧鼠标位置
            lastMouseY = currentMouseY;
        }
    }

    void OnMouseUp()
    {
        isPulling = false;
        if (outed == false)
        {
            StartCoroutine(ReboundToOriginal());
        }
    }

    void ApplyStretch(float stretchRatio)
    {
        // 计算目标拉伸值（使用平滑曲线）
        float targetStretch = Mathf.Lerp(minStretch, maxStretch, stretchRatio);

        // 计算当前拉伸值（平滑过渡）
        float currentStretch = Mathf.Lerp(transform.localScale.y / originalScale.y, targetStretch, Time.deltaTime * 10f);

        // 应用Y轴拉伸
        Vector3 newScale = originalScale;
        newScale.y = originalScale.y * currentStretch;
        transform.localScale = newScale;

        // 向上移动（根据拉伸比例）
        float heightOffset = (newScale.y - originalScale.y) * 0.5f;
        Vector3 newPosition = originalPosition;
        newPosition.y = originalPosition.y + heightOffset * moveSpeed;
        transform.position = newPosition + openoffset;
    }

    System.Collections.IEnumerator ReboundToOriginal()
    {
        Vector3 startScale = transform.localScale;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * elasticity;
            float t = Mathf.SmoothStep(0f, 1f, progress);

            // 缩放回弹
            transform.localScale = Vector3.Lerp(startScale, originalScale, t);

            // 位置回弹
            transform.position = Vector3.Lerp(startPosition, originalPosition, t);

            yield return null;
        }

        // 确保精确回到原始状态
        transform.localScale = originalScale;
        transform.position = originalPosition;
        transform.rotation = Quaternion.identity;
    }
}