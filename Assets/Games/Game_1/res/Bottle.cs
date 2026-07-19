using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] int maxCount;
    public int count;

    public Transform mouth;
    public GameObject bubble_small;
    public GameObject bubble_big;

    private GameObject bubble_current;

    public bool hasShaked;

    [Header("主震动设置")]
    public float shakeDuration = 0.8f;
    [Tooltip("只设置你想摇晃的轴。例如(0,0,15)代表只绕Z轴左右摇摆")]
    public Vector3 rotationStrength = new Vector3(0, 0, 15f);
    public int vibrato = 10;
    public float randomness = 90f;
    public bool fadeOut = true; // 通常为true，使晃动自然衰减

    [Header("摇晃结束后的微小回弹")]
    public bool enableBounceBack = true;
    public float bounceDuration = 0.3f;

    private Tween _currentTween;

    void Start()
    {
        count = maxCount;
    }

    void Update()
    {
        if (count > maxCount / 2)
        {
            bubble_current = bubble_small;
        }
        else
        {
            bubble_current = bubble_big;
        }

        if (count == 0 && !hasShaked)
        {
            hasShaked = true;
            TriggerShake();
            return;
        }
    }

    public void TriggerShake()
    {
        // 终止之前的晃动，防止叠加
        if (_currentTween != null && _currentTween.IsActive())
        {
            _currentTween.Kill();
        }

        // 执行旋转震荡：这是实现“限制轴向”的关键
        // rotationStrength中非零的轴，即为瓶子摇晃的轴
        _currentTween = transform.DOShakeRotation(shakeDuration, rotationStrength, vibrato, randomness, fadeOut)
            .OnComplete(() =>
            {
                // 可选：晃动结束后，轻微回弹到原位，使动作更生动
                if (enableBounceBack)
                {
                    transform.DORotate(Vector3.zero, bounceDuration).SetEase(Ease.OutBack);
                }
            });
    }

    void OnDestroy()
    {
        // 清理动画
        if (_currentTween != null && _currentTween.IsActive())
        {
            _currentTween.Kill();
        }
    }

    public void InstantiateBubble()
    {
        GameObject bubble = Instantiate(bubble_current, mouth.position, Quaternion.LookRotation(Vector3.up), mouth);
    }
}
