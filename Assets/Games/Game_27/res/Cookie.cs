using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public List<GameObject> cookiePieces;
    public List<GameObject> umbrellaPieces;

    public float resetDelay = 1f;
    public float timer = 0f;
    public bool canDestroy = true;

    private void Start()
    {
        canDestroy = true;
    }

    void Update()
    {
        foreach (var piece in umbrellaPieces)
        {
            if (!piece.activeSelf)
            {
                canDestroy = false;
                ResetPieces();
            }
        }
    }

    void ResetPieces()
    {
        timer += Time.deltaTime;

        if (timer > resetDelay)
        {
            timer = 0;

            foreach (var piece in cookiePieces)
            {
                piece.SetActive(true);
            }
            foreach (var piece in umbrellaPieces)
            {
                piece.SetActive(true);
            }

            canDestroy = true;
        }
    }
}
