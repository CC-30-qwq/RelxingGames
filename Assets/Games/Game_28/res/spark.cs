using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour
{
    public GameObject sparkEffect;
    public bool sparked = false;
    public GameObject fire;
    public float timer = 0f;
    AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Finish"))
        {
            if (!sparked && timer > .5f && !fire.activeSelf)
            {
                sparked = true;
                timer = 0f;
                AudioSource.Play();
                GameObject spark = Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sparked = false;
    }
}
