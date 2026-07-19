using UnityEngine;
using DG.Tweening;

public class LeafController : MonoBehaviour
{
    [Header("动画参数")]
    [SerializeField] private float rotateAngle = 90f;
    [SerializeField] private float rotateTime = 0.8f;
    [SerializeField] private Vector2 moveDistance = new Vector2(2f, -1f);
    [SerializeField] private float moveTime = 0.4f;
    [SerializeField] private float delayTime = 0.2f;

    private Sequence animationSequence;
    private bool isAnimating = false;
    public bool isPaused = false;
    private Vector3 startPos;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPaused = true;
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        if (!isAnimating)
        {
            StartAnimation();
        }
        else if (isPaused && animationSequence != null)
        {
            animationSequence.Play();
            isPaused = false;
        }
    }

    private void OnMouseUp()
    {
        if (isAnimating && !isPaused && animationSequence != null)
        {
            animationSequence.Pause();
            isPaused = true;
        }
    }

    private void StartAnimation()
    {
        isAnimating = true;
        isPaused = false;

        // 创建主动画序列
        animationSequence = DOTween.Sequence();

        // 1. 旋转动画
        animationSequence.Append(
            transform.DORotate(new Vector3(0, 0, rotateAngle), rotateTime)
            .SetEase(Ease.OutQuad)
        );

        // 2. 延迟
        animationSequence.AppendInterval(delayTime);

        // 3. 在延迟结束后立即启动独立移动动画
        animationSequence.AppendCallback(() => {
            audioSource.Play();
            // 这个动画独立于主序列，不会被暂停
            transform.DOMove(startPos + (Vector3)moveDistance, moveTime);
        });

        // 动画完成时重置状态
        animationSequence.OnComplete(() =>
        {
            isAnimating = false;
            isPaused = true;

        });

        animationSequence.Play();
    }

    private void OnDestroy()
    {
        if (animationSequence != null)
            animationSequence.Kill();
    }
}