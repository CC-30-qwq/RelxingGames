using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_2 : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject a;
    public GameObject win;

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (a.activeSelf == true)
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
        SceneManager.LoadScene("Game_3");
    }
}
