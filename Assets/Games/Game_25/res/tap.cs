using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tap : MonoBehaviour
{
    public GameObject nextPicture;

    public WaterAndSplash scriptTap;

    public AudioSource source;

    private void OnMouseDown()
    {
        if (nextPicture != null)
        {
            source.Play();
            scriptTap.ScaleFactor = scriptTap.ScaleFactor - (1.0f / 22.0f);
            transform.gameObject.SetActive(false);
            nextPicture.SetActive(true);
        }
    }
}
