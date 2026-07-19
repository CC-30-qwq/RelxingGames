using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cut : MonoBehaviour
{
    public float moveDis;
    public float duration;

    public float dropTime;
    public GameObject upper;
    public GameObject lower;
    public float lowermoveDis;
    public float lowerduration;

    public GameObject nextPart;
    public GameObject nextup;
    public GameObject nextlow;

    bool hasCuted = false;

    public AudioSource audioSource;

    private void OnMouseDown()
    {
        if (!hasCuted)
        {
            hasCuted = true;
            StartCoroutine(StartCut());
        }
    }

    IEnumerator StartCut()
    {
        audioSource.Play();
        transform.DOMoveY(moveDis, duration);
        yield return new WaitForSeconds(dropTime);
        upper.SetActive(false);
        lower.SetActive(true);
        lower.transform.DOMoveY(lowermoveDis, lowerduration).SetRelative(true);
        yield return new WaitForSeconds(duration - dropTime);
        transform.gameObject.SetActive(false);
        if (nextPart != null)
        {
            nextPart.SetActive(true);
        }
        else
        {
            nextup.SetActive(false);
            nextlow.SetActive(true);
            nextlow.transform.DOMoveY(lowermoveDis, lowerduration).SetRelative(true);
        }
    }
}
