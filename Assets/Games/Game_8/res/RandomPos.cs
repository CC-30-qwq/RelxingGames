using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    bool spawned = false;

    void Update()
    {
        float x = Random.Range(-1.5f, 1.5f);
        if (Mathf.Abs(x) > 1f && !spawned)
        {
            spawned = true;
            transform.position = new Vector3(x, transform.position.y);
        }
    }
}
