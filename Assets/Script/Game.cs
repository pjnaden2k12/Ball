using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public BoxSpawn boxSpawner;
    public UIManager_Game uiManager;

    private bool gameOver = false;
    private int score = 0;

    public int Score => score;

    private void Start()
    {
        boxSpawner = FindObjectOfType<BoxSpawn>();
        uiManager = FindObjectOfType<UIManager_Game>();
        uiManager.UpdateScoreUI(score);
    }

    public void AddScore(int amount)
    {
        score += amount;
        uiManager.UpdateScoreUI(score);
    }

    public void EndTurn()
    {
        if (gameOver) return;

        Box[] boxes = FindObjectsOfType<Box>();
        foreach (Box box in boxes)
        {
            box.OnEndTurn();
        }

        boxSpawner.MoveBoxesDownWithFreeze();

        if (!boxSpawner.DoesAnyBoxExist())
        {
            Debug.Log("Win!");
        }
        else if (boxSpawner.DoesAnyBoxReachBottom())
        {
            gameOver = true;

            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                PlayerPrefs.Save();
                highScore = score;
            }

            uiManager.ShowGameOver(score, highScore);
            Debug.Log("Lose!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SceneMenu");
    }
}