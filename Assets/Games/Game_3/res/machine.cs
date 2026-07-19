using UnityEngine;

public class machine : MonoBehaviour
{
    public GameObject gas;

    [Header("拖拽设置")]
    public bool returnToOrigin = true;
    public float returnSpeed = 10f;

    [Header("效果设置")]
    public bool scaleEffect = true;
    public float dragScale = 1.1f;
    public float scaleSpeed = 10f;

    private bool isDragging = false;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Vector3 offset;
    private float zCoord;
    private bool isReturning = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnMouseDown()
    {
        gas.SetActive(true);
        gas.GetComponent<AudioSource>().Play();

        originalPosition = transform.position;
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();

        isDragging = true;
        isReturning = false;

        if (scaleEffect)
        {
            StartCoroutine(ScaleTo(originalScale * dragScale));
        }
    }

    void OnMouseDrag()
    {
        if (isDragging && !isReturning)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void OnMouseUp()
    {
        gas.SetActive(false);
        gas.GetComponent<AudioSource>().Stop();

        if (isDragging)
        {
            isDragging = false;

            if (scaleEffect)
            {
                StartCoroutine(ScaleTo(originalScale));
            }

            if (returnToOrigin)
            {
                StartCoroutine(ReturnToOriginalPosition());
            }
        }
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
        transform.localScale = targetScale;
    }

    private System.Collections.IEnumerator ReturnToOriginalPosition()
    {
        isReturning = true;

        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, Time.deltaTime * returnSpeed);
            yield return null;
        }

        transform.position = originalPosition;
        isReturning = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}