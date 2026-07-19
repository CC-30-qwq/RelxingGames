using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_15 : MonoBehaviour
{
    public GameManager gameManager;

    public List<GameObject> a;
    public GameObject win;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("a"))
        {
            a.Add(item);
        }
    }

    void Update()
    {
        foreach (var item in a)
        {
            if (item == null)
            {
                a.Remove(item);
            }
        }

        if (a.Count == 0)
        {
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
        SceneManager.LoadScene("Game_16");
    }
}
