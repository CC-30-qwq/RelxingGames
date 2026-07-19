using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_13 : MonoBehaviour
{
    public GameManager gameManager;

    public float a;
    public GameObject win;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (a >= 10)
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
        SceneManager.LoadScene("Game_14");
    }
}