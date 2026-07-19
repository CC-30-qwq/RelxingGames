using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class fold : MonoBehaviour
{
    [Header("透明度设置")]
    [SerializeField] private float fadedAlpha = 0.3f;    // 触发时的透明度
    [SerializeField] private float fadeDuration = 0.5f;  // 过渡时间

    [Header("目标组件")]
    [SerializeField] private Image targetImage;          // UI Image组件
    [SerializeField] private SpriteRenderer spriteRenderer; // 精灵渲染器
    [SerializeField] private CanvasGroup canvasGroup;    // Canvas Group组件

    private Tween fadeTween;  // 当前透明度动画
    private bool isTriggered = false; // 是否被触发

    AudioSource audioSource;
    public GameObject gas;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // 自动获取组件（如果没有手动指定）
        if (targetImage == null) targetImage = GetComponent<Image>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    // 2D触发器进入
    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.Play();
        gas.GetComponent<AudioSource>().Stop();
        // 可以根据标签过滤
        if (other.CompareTag("Player"))
        {
            StartFade();
        }
    }

    // 2D触发器退出
    private void OnTriggerExit2D(Collider2D other)
    {
        audioSource.Stop();
        gas.GetComponent<AudioSource>().Play();
        if (other.CompareTag("Player"))
        {
            StopFade();
        }
    }

    // 开始透明度变化
    private void StartFade()
    {
        if (isTriggered) return; // 已经触发则不重复执行

        isTriggered = true;
        StopCurrentFade();

        // 根据组件类型执行不同的淡入动画
        if (canvasGroup != null)
        {
            fadeTween = canvasGroup.DOFade(fadedAlpha, fadeDuration)
                .SetEase(Ease.OutQuad);
        }
        else if (targetImage != null)
        {
            fadeTween = targetImage.DOFade(fadedAlpha, fadeDuration)
                .SetEase(Ease.OutQuad);
        }
        else if (spriteRenderer != null)
        {
            fadeTween = spriteRenderer.DOFade(fadedAlpha, fadeDuration)
                .SetEase(Ease.OutQuad);
        }

        // 监听动画完成
        if (fadeTween != null)
        {
            fadeTween.OnComplete(() =>
            {
                // 动画完成后，如果需要持续闪烁效果，可以在这里添加
                // Debug.Log("淡入完成");
            });
        }
    }

    // 停止透明度变化（立即停止并恢复）
    private void StopFade()
    {
        if (!isTriggered) return; // 没有被触发则不执行

        isTriggered = false;
        StopCurrentFade();
    }

    // 停止当前动画
    private void StopCurrentFade()
    {
        if (fadeTween != null && fadeTween.IsActive())
        {
            fadeTween.Kill();
            fadeTween = null;
        }
    }

    void OnDestroy()
    {
        StopCurrentFade(); // 清理资源
    }
}