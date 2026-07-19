using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_25 : MonoBehaviour
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
        if (a.activeSelf)
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
        SceneManager.LoadScene("Game_26");
    }
}
