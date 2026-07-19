using System.Collections;
using UnityEngine;

public class ball : MonoBehaviour
{
    Vector3 orignPos;
    public float resetTime;
    public Vector2 maxVelocity;
    bool hasReseted;

    public hand hand;
    public float forceMultiply;

    bool finish;

    public Win_5 Win_5;

    AudioSource AudioSource;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        finish = false;
        hasReseted = false;
        orignPos = transform.position;
    }

    private void Update()
    {
        if (transform.GetComponent<Rigidbody2D>().velocity.x > maxVelocity.x)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(maxVelocity.x, transform.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (transform.GetComponent<Rigidbody2D>().velocity.y > maxVelocity.y)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.GetComponent<Rigidbody2D>().velocity.x, maxVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.transform.CompareTag("Player"))
        {
            AudioSource.Play();

            Vector2 transformPos = new(transform.position.x, transform.position.y);
            transform.GetComponent<Rigidbody2D>().AddForce(-(collision.contacts[0].point - transformPos) * hand.rotationSpeed * forceMultiply);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn") && !hasReseted)
        {
            hasReseted = true;
            StartCoroutine(ResetPos());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn") && !hasReseted)
        {
            hasReseted = true;
            StartCoroutine(ResetPos());
        }


        if (collision.CompareTag("Finish") && !finish)
        {
            finish = true;
            Win_5.a += 1;
            ResetPos();
        }

    }

    private IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(resetTime);
        finish = false;
        hasReseted = false;
        transform.position = orignPos;
        transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
