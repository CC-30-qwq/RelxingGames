using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public float z;
    public float targetZ;
    public Transform head;

    Quaternion orir;

    public bool drop;

    private void Start()
    {
        drop = false;
        orir = transform.rotation;
    }

    private void Update()
    {
        Vector2 dir = transform.up;
        z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        if (Mathf.Abs(z) < targetZ)
        {
            if (!drop)
            {
                transform.position = head.position;
            }
        }
        else
        {
            drop = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Respawn"))
        {
            resetPos();
        }
    }
    private void resetPos()
    {
        transform.position = head.position;
        transform.rotation = orir;
        transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        drop = false;
    }
}
