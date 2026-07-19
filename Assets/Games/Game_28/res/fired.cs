using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fired : MonoBehaviour
{
    public GameObject fire;
    public matchCtrl matchCtrl;
    public float velocityThreshold = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (matchCtrl.velocity.magnitude > matchCtrl.targetV * velocityThreshold)
            {
                fire.SetActive(true);
            }
        }

        if (collision.transform.CompareTag("Finish"))
        {
            fire.SetActive(true);
        }
    }
}