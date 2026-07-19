using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public GameObject ballon;
    public GameObject breakeffect;
    public GameObject rope;
    public float duration;

    public Win_13 win_13;

    private void Start()
    {
        win_13 = GameObject.Find("GameManager").GetComponent<Win_13>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            win_13.a++;
            rigidbody.velocity = Vector2.zero;
            rigidbody.gravityScale = 0;
            ballon.SetActive(false);
            breakeffect.SetActive(true);
            StartCoroutine(DestoryBallon());
            rope.transform.parent = null;
            rope.AddComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(transform.gameObject);
    }

    IEnumerator DestoryBallon()
    {
        yield return new WaitForSeconds(duration);
        Destroy(transform.gameObject);
    }
}
