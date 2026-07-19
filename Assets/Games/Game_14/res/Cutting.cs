using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public float maxDistance = 2f;
    public float moveSpeed = 10f;

    private Vector3 originalPos;
    private bool isPressed = false;

    public float cutTime1;
    public float cutTime2;
    public GameObject Big;
    public GameObject Small;
    public Transform cucumber;
    public float timer;
    bool hasDroped = false;
    public int order;

    public int count = 0;
    AudioSource audioSource;

    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        originalPos = transform.position;
    }

    void Update()
    {
        Cut();

        Drop();
    }

    private void Drop()
    {
        if (isPressed)
        {
            timer += Time.deltaTime;

            if (!hasDroped)
            {
                if (timer >= cutTime2)
                {
                    audioSource.Play();
                    order += 1;
                    count++;
                    GameObject big = Instantiate(Big, cucumber.position, cucumber.rotation);
                    big.transform.GetComponent<SpriteRenderer>().sortingOrder = order; 
                    hasDroped = true;
                }
            }
        }
        else
        {
            if (!hasDroped)
            {
                if (timer >= cutTime1)
                {
                    audioSource.Play();
                    order += 1;
                    count++;
                    GameObject small = Instantiate(Small, cucumber.position, cucumber.rotation);
                    small.transform.GetComponent<SpriteRenderer>().sortingOrder = order;
                    hasDroped = true;
                }
            }

            hasDroped = false;
            timer = 0;
        }
    }

    private void Cut()
    {
        // 检测输入
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }

        // 更新位置
        if (isPressed)
        {
            // 向下移动
            Vector3 targetPos = originalPos + Vector3.down * maxDistance;
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            // 返回原位置
            transform.position = Vector3.MoveTowards(
                transform.position,
                originalPos,
                moveSpeed * Time.deltaTime
            );
        }
    }
}
