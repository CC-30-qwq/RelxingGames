using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuiziCtrl : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    public Transform head;
    private Collider2D collider2D;
    private Animator animator;  
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider2D = head.GetComponent<Collider2D>(); 
        animator = head.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDragging)
        {
            collider2D.enabled = true;
            animator.Play("tuizi");
        }
        else
        {
            collider2D.enabled = false;
            animator.Play("ept");
        }
    }
    void OnMouseDown()
    {
        audioSource.Play();
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();

        isDragging = true;
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
        audioSource.Stop();

        if (isDragging)
        {
            isDragging = false;
        }
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
