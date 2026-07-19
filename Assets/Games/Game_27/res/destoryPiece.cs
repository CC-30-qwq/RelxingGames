using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryPiece : MonoBehaviour
{
    public bool canDestroy = true;
    public Cookie Cookie;
    public AudioSource source;

    private void Update()
    {
        canDestroy = Cookie.canDestroy;
    }

    private void OnMouseDown()
    {
        if (canDestroy)
        {
            source.Play();
            transform.gameObject.SetActive(false);
        }
    }
}
