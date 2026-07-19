using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landWin : MonoBehaviour
{
    public bool land = false;
    public float timer = 0f;

    bool landing;

    private void Update()
    {
        if (landing)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }

        if ((timer > 1.5f))
        {
            land = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            landing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        landing = false;
    }
}
