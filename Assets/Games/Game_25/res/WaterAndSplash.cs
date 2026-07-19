using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAndSplash : MonoBehaviour
{
    public GameObject Water;
    public GameObject Splash;
    public GameObject puddle;

    public float ScaleFactor = 1f;
    public float fadeDuration;

    Vector3 waterOri;
    Vector3 splashOri;

    AudioSource waterSound;

    private void Start()
    {
        waterSound = GetComponent<AudioSource>();
        waterOri = Water.transform.localScale;
        splashOri = Splash.transform.localScale;
    }

    private void Update()
    {
        if (ScaleFactor < 0.01)
        {
            waterSound.Stop();
            ScaleFactor = 0;
            puddle.transform.GetComponent<SpriteRenderer>().DOFade(0, fadeDuration);
        }

        if (Water != null && Splash != null)
        {
            Water.transform.localScale = new Vector3(waterOri.x * ScaleFactor, waterOri.y, waterOri.z);
            Splash.transform.localScale = splashOri * ScaleFactor;
        }
    }
}
