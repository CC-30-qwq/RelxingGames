using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryLight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
        Destroy(gameObject);

        }
    }
}
