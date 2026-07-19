using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallon : MonoBehaviour
{
    bool respawned = false;
    public GameObject ballon;
    float timer;
    public float cooldown;
    public Vector2 Randompos;

    void Update()
    {
        timer += Time.deltaTime;
        float x = Random.Range(Randompos.x, Randompos.y);
        if (!respawned)
        {
            timer = 0;
            respawned = true;
            GameObject ballons = Instantiate(ballon, new Vector3(x, transform.position.y), transform.rotation);
        }

        if (timer > cooldown)
        {
            respawned = false;
        }
    }
}
