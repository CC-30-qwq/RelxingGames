using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    bool fire;
    public GameObject bullets;
    public Transform muzzle;
    public float cooldown;
    private float timer;

    [Header("力度设置")]
    [SerializeField] private float minForce = 5f;    // 最小力度
    [SerializeField] private float maxForce = 10f;   // 最大力度

    [Header("角度范围")]
    [SerializeField] private float angleRange = 120f; // 120度区间

    public float count;
    public Text text;

    public bool finish;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        text.text = count.ToString();
        timer += Time.deltaTime;

        if (count >= 999)
        {
            finish = true;
        }

        if (fire && timer > cooldown && !finish)
        {
            timer = 0;
            count++;

            GameObject bullet = Instantiate(bullets, muzzle.position, muzzle.rotation);

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
            bullet.GetComponent<Rigidbody2D>().AddForce(forceDirection * randomForce, ForceMode2D.Impulse);
        }
    }

    void OnMouseDown()
    {
        audioSource.Play();
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();

        isDragging = true;

        fire = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.GetComponent<Rigidbody2D>().MovePosition(GetMouseWorldPos() + offset);
        }
    }

    void OnMouseUp()
    {
        audioSource.Pause();
        if (isDragging)
        {
            isDragging = false;
        }

        fire = false;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
