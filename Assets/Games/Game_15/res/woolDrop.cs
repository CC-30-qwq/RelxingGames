using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woolDrop : MonoBehaviour
{
    [Header("力度设置")]
    [SerializeField] private float minForce = 5f;    // 最小力度
    [SerializeField] private float maxForce = 10f;   // 最大力度

    [Header("角度范围")]
    [SerializeField] private float angleRange = 120f; // 120度区间

    private Rigidbody2D rb;
    Collider2D collider2D;
    public bool droped;

    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.isTrigger = true;
        droped = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // 确保有Rigidbody2D
            if (rb == null)
            {
                rb = GetComponent<Rigidbody2D>();
                if (rb == null)
                {
                    Debug.LogError("需要Rigidbody2D组件！");
                    return;
                }
            }

            rb.gravityScale = 1;

            // 计算随机角度（以正上方为0度，左右各60度）
            // 随机角度范围：90 - 60 = 30度 到 90 + 60 = 150度
            // 转化为弧度制：30度 = 30 * Mathf.Deg2Rad
            float randomAngle = Random.Range(
                (90f - angleRange / 2f) * Mathf.Deg2Rad,
                (90f + angleRange / 2f) * Mathf.Deg2Rad
            );

            // 计算力向量
            Vector2 forceDirection = new Vector2(
                Mathf.Cos(randomAngle),
                Mathf.Sin(randomAngle)
            );

            // 计算随机力度
            float randomForce = Random.Range(minForce, maxForce);

            // 施加力
            if (!droped)
            {
                droped = true;
                rb.AddForce(forceDirection * randomForce, ForceMode2D.Impulse);
            }
        }

        if (collision.transform.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
