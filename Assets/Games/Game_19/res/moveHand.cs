using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveHand : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;
    private float zCoord;

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
