using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCount : MonoBehaviour
{
    public GameCheck check;
    bool isCheck;

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!isCheck)
            {
                isCheck = true;
                check.count += 1;
            }
        }
    }
}
