using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickBreak : MonoBehaviour
{
    public GameObject oriObj;
    public GameObject breakObj;
    public SpriteRenderer spriteRenderer;

    Win_29 win_29;
    bool isbreak = false;
    AudioSource s;

    private void Start()
    {
        s = GetComponent<AudioSource>();
    }
    private void Update()
    {
        win_29 = GameObject.Find("GameManager").GetComponent<Win_29>();

        if (isbreak)
        {
            isbreak = false;
            s.Play();
            win_29.a++;
        }
    }

    private void OnMouseDown()
    {
        oriObj.SetActive(false);
        breakObj.SetActive(true);
        transform.GetComponent<Rigidbody2D>().drag = 10;
        spriteRenderer.DOFade(0, 1f);
        Destroy(gameObject, 1f);
        isbreak = true;
    }
}
