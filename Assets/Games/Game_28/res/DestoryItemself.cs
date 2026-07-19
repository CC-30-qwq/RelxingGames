using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryItemself : MonoBehaviour
{
    public float time = 2f;
    float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            Destroy(gameObject);
        }
    }
}
