using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cornDropPlus : MonoBehaviour
{
    public List<GameObject> popcorn;
    public Transform parent;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        source.Play();
        transform.gameObject.SetActive(false);
        GameObject pop = Instantiate(popcorn[Random.Range(0, popcorn.Count)], transform.position, transform.rotation, parent);
        pop.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}