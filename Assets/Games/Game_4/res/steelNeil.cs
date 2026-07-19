using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steelNeil : MonoBehaviour
{
    public float force;
    Rigidbody2D rb;
    public float forceMultiplier = 0.1f;
    public Transform targetY;

    public bool finish;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        finish = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y <= targetY.position.y)
        {
            transform.position = new Vector3(transform.position.x, targetY.position.y, transform.position.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            force = collision.relativeVelocity.magnitude;
            if (force > 3)
            {
                source.Play();
                if (transform.position.y > targetY.position.y)
                {
                    rb.MovePosition(transform.position + Vector3.down * force * forceMultiplier * Time.deltaTime);
                }
            }
        }

        if (collision.transform.CompareTag("Finish"))
        {
            finish = true;
        }
    }
}