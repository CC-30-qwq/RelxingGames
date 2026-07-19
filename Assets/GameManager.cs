using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void StartGame(int index)
    {
        SceneManager.LoadScene("Game_" + index);
    }
}
