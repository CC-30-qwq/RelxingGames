using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inner : MonoBehaviour
{
    public string tagName;
    public float count;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            source.Play();
            count += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagName))
        {
            count -= 1;
        }
    }
}
