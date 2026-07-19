using DG.Tweening;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveDistance = 5f; // Y轴移动距离
    [SerializeField] private float minAngle = -30f;   // 最小角度（度）
    [SerializeField] private float maxAngle = 30f;    // 最大角度（度）
    [SerializeField] private float duration = 2f;     // 移动时间

    void Start()
    {
        MoveWithRandomAngle();
    }

    public void MoveWithRandomAngle()
    {
        // 生成随机角度
        float randomAngle = Random.Range(minAngle, maxAngle);

        // 将角度转换为弧度
        float angleInRad = randomAngle * Mathf.Deg2Rad;

        // 计算目标位置
        // Y轴固定向下移动指定距离
        // X轴根据角度计算偏移
        Vector2 startPos = transform.position;
        float yOffset = -moveDistance; // 向下移动为负值
        float xOffset = moveDistance * Mathf.Tan(angleInRad);

        Vector2 targetPos = startPos + new Vector2(xOffset, yOffset);

        // 使用DOTween移动
        transform.DOMove(targetPos, duration)
            .SetEase(Ease.OutQuad);
    }
}