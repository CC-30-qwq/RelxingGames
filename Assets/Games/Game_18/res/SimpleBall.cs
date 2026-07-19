using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float v;
    public float k;

    public float maxV;

    [Header("Swipe Settings")]
    public float forceMultiplier = 5f; // 力的大小
    public float minSwipeDistance = 30f; // 最小滑动距离
    public bool canswipe;
    private Vector2 startPos;

    AudioSource audioSource;
    public AudioSource cup;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        canswipe = true;
    }

    private void Update()
    {
        v = rb.velocity.magnitude;

        if (v > 0)
        {
            rb.drag = k / v;
        }
        else
        {
            rb.drag = 0;
        }

        if (rb.velocity.magnitude > maxV)
        {
            rb.velocity = rb.velocity.normalized * maxV;
        }

        if (canswipe)
        {
            swipe();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            cup.Play();
            maxV = 1;
            canswipe = false;
        }
    }

    void swipe()
    {
        // 鼠标/触摸开始
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        // 鼠标/触摸结束
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;
            Vector2 swipe = endPos - startPos;

            // 检查滑动距离
            if (swipe.magnitude > minSwipeDistance)
            {
                audioSource.Play();
                // 转换为世界坐标方向
                Vector2 direction = Camera.main.ScreenToWorldPoint(endPos) -
                                   Camera.main.ScreenToWorldPoint(startPos);

                // 施加力
                rb.AddForce(direction.normalized * swipe.magnitude * forceMultiplier,
                           ForceMode2D.Impulse);
            }
        }
    }
}