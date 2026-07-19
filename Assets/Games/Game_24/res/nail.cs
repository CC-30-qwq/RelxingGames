using System.Collections;
using DG.Tweening;
using UnityEngine;

public class nail : MonoBehaviour
{
    [Header("动画参数")]
    [SerializeField] private float rotateAngle = 90f;
    [SerializeField] private float rotateTime = 0.8f;
    [SerializeField] private Vector2 moveDistance = new Vector2(2f, -1f);
    Vector3 randompos;
    [SerializeField] private float moveTime = 0.4f;
    [SerializeField] private float delayTime = 0.2f;

    private Sequence animationSequence;
    private bool isAnimating = false;
    private Vector3 startPos;

    public bool yes;

    public Transform nextNail;
   public float timer = 0f;

    void Start()
    {
        randompos = new Vector3(Random.Range(0, moveDistance.x), Random.Range(0, moveDistance.y), 0);
        startPos = transform.position;
        yes = false;
    }

    private void Update()
    {
        if (yes)
        {
            timer += Time.deltaTime;
            if (nextNail != null && timer > 0.5f)
            {
                nextNail.GetComponent<Collider2D>().enabled = true;
            }

            if (!isAnimating)
            {
                StartAnimation();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            yes = true;
        }
    }

    private void StartAnimation()
    {
        isAnimating = true;

        // 创建动画序列
        animationSequence = DOTween.Sequence();

        // 1. 旋转动画（先快后慢）
        animationSequence.Append(
            transform.DORotate(new Vector3(0, 0, rotateAngle), rotateTime)
            .SetEase(Ease.OutQuad)
        );

        // 2. 延迟
        animationSequence.AppendInterval(delayTime);

        // 3. 移动动画
        animationSequence.Append(
            transform.DOMove(startPos + randompos, moveTime)
        );

        // 动画完成时重置状态
        animationSequence.OnComplete(() =>
        {
            isAnimating = false;
        });

        animationSequence.Play();
    }

    private void OnDestroy()
    {
        if (animationSequence != null)
            animationSequence.Kill();
    }
}
