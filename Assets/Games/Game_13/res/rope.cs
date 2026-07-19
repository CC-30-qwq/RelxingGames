using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    void Update()
    {
        if(transform.parent == null)
        {
            Destroy(transform.gameObject, 2f);
        }
    }
}
