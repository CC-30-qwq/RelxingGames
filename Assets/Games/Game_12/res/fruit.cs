using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class fruit : MonoBehaviour
{
    public GameObject bottle;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;

    [Header("Drag Settings")]
    [SerializeField] private float releaseForce = 5f;
    [SerializeField] private bool useLerp = true;
    [SerializeField] private float lerpSpeed = 15f;

    fruitdrop fruitdrop;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        fruitdrop = GetComponent<fruitdrop>();
    }

    void OnMouseDown()
    {
        if (fruitdrop.droped)
        {
            Vector3 mousePos = GetMouseWorldPos();
            offset = transform.position - mousePos;
            isDragging = true;

            // 开始拖拽时暂停物理模拟
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = GetMouseWorldPos();
            Vector3 targetPosition = mousePos + offset;

            if (useLerp)
            {
                // 使用插值平滑移动
                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
                rb.MovePosition(newPosition);
            }
            else
            {
                // 直接移动到目标位置
                rb.MovePosition(targetPosition);
            }
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            // 添加释放时的力
            if (releaseForce > 0)
            {
                Vector3 mouseVelocity = GetMouseWorldVelocity();
                rb.velocity = mouseVelocity * releaseForce;
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    Vector3 GetMouseWorldVelocity()
    {
        // 计算鼠标移动速度（简化版本）
        Vector3 currentPos = GetMouseWorldPos();
        Vector3 lastPos = GetMouseWorldPos() - (Vector3)Input.mousePosition.normalized * 0.1f;
        return (currentPos - lastPos) / Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && !isDragging)
        {
        }
    }
}