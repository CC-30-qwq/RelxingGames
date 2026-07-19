using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neilCtrl : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private float zCoord;
    public float x;

    private void Update()
    {
        if (transform.position.x > x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -x)
        {
            transform.position = new Vector3(-x, transform.position.y, transform.position.z);
        }
    }

    void OnMouseDown()
    {
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
