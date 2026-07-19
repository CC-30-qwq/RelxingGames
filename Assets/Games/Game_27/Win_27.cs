using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_27 : MonoBehaviour
{
    public GameManager gameManager;

    public List<GameObject> a;
    public GameObject win;
    public Cookie cookie;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("a"))
        {
            if (!a.Contains(item) && item.activeSelf)
            {
                a.Add(item);
            }
        }
    }

    void Update()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("a"))
        {
            if (!a.Contains(item) && item.activeSelf)
            {
                a.Add(item);
            }
        }

        foreach (var item in a)
        {
            if (!item.activeSelf)
            {
                a.Remove(item);
            }
        }

        if (a.Count == 0)
        {
            cookie.canDestroy = false;
            Invoke("Win", 1f);
            Invoke("StartGame", 2f);
        }
    }

    public void Win()
    {
        win.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game_28");
    }
}
