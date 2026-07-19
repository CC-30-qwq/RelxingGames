using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    public float count = 0;
    public float targetCount = 7;

    public GameObject a;
    public GameObject b;
    public GameObject fire;
    public GameObject smoke;
    public Transform head;
    bool smoked = false;

    private void Update()
    {
        if (count >= targetCount)
        {
            a.SetActive(false);
            b.SetActive(true);
            fire.SetActive(false);
            if (!smoked)
            {
                GameObject smokeeffect = Instantiate(smoke, head.position, Quaternion.identity, head);
                smoked = true;
            }
        }
    }
}
