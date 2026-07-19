using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class getPopcorn : MonoBehaviour
{
    [Header("移动物体")]
    public GameObject mover;          // 要移动的物体
    public GameObject carriedObject;  // 被携带的物体
    public Transform targetPosition;  // 目标位置

    [Header("条件列表")]
    public List<GameObject> conditionObjects; // 需要为true的物体列表

    [Header("动画设置")]
    public float moveDuration = 2f;   // 移动时间
    public float returnDelay = 0.5f;  // 返回延迟

    private Vector3 startPos;         // 起始位置
    public bool hasMoved = false;    // 是否已经移动过

    public GameObject outhand;
    public GameObject inhand;

    void Start()
    {
        if (mover != null)
        {
            startPos = mover.transform.position;
        }
    }

    void Update()
    {
        // 检查所有条件物体是否都激活
        if (!hasMoved && CheckConditions())
        {
            StartMoveAnimation();
            hasMoved = true;
        }
    }

    // 检查所有条件
    bool CheckConditions()
    {
        foreach (GameObject obj in conditionObjects)
        {
            if (obj != null && obj.activeSelf)
                return false;
        }
        return true;
    }

    // 开始移动动画
    void StartMoveAnimation()
    {
        if (mover == null) return;

        // 创建动画序列
        Sequence sequence = DOTween.Sequence();

        // 1. 移动到目标位置
        sequence.Append(mover.transform.DOMove(targetPosition.position, moveDuration));

        // 2. 短暂延迟
        sequence.AppendInterval(returnDelay);
        sequence.AppendCallback(() =>
        {
            foreach (GameObject obj in conditionObjects)
            {
                obj.isStatic = true;
            }

            // 切换手持状态
            outhand.SetActive(false);
            inhand.SetActive(true);
        });

        // 3. 携带物体返回
        if (carriedObject != null)
        {
            // 携带物体一起返回
            Vector3 targetpos = targetPosition.position;
            targetpos.y = 0;
            sequence.Append(carriedObject.transform.DOMove(startPos - targetpos, moveDuration));
            sequence.Join(mover.transform.DOMove(startPos, moveDuration));
        }
        else
        {
            // 独自返回
            sequence.Append(mover.transform.DOMove(startPos, moveDuration));
        }
    }

    // 手动触发移动（按钮调用）
    public void TriggerMove()
    {
        StartMoveAnimation();
    }

    // 重置状态
    public void Reset()
    {
        hasMoved = false;
        if (mover != null)
        {
            mover.transform.position = startPos;
        }
    }
}