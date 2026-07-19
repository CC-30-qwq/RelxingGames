using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tv : MonoBehaviour
{
    public Transform antenna;
    public Transform screen404;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float currentAngle;
    public float maxAngle;

    void Start()
    {
        spriteRenderer = screen404.GetComponent<SpriteRenderer>();
        maxAngle = antenna.GetComponent<antenna>().maxAngle;
    }

    void Update()
    {
        currentAngle = antenna.GetComponent<antenna>().currentRotation;

        ChangeAlpha(1 - ((currentAngle + maxAngle) / 2) / maxAngle);
    }

    public void ChangeAlpha(float alpha)
    {
        if (spriteRenderer == null) return;

        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp01(alpha); // 确保在0-1范围内
        spriteRenderer.color = color;
    }
}
