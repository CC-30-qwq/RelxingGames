using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipper : MonoBehaviour
{
    public GameObject trigger;
    public float duration;
    bool triggered;
    float timer;

    moveHand handScript;
    void Start()
    {
        handScript = GetComponent<moveHand>();
    }

    void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;
        }
        else
        {
            trigger.SetActive(false);
        }

        if (timer > duration) { triggered = false; }
    }

    public void StartTrigger()
    {
        if (!triggered && !handScript.isDragging)
        {
            timer = 0;
            triggered = true;
            trigger.SetActive(true);
        }
    }
}
