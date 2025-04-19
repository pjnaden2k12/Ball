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

        // Cập nhật UI điểm khởi đầu
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
            // Có thể gọi UI chiến thắng nếu bạn muốn
        }
        else if (boxSpawner.DoesAnyBoxReachBottom())
        {
            gameOver = true;

            // Cập nhật highscore
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                PlayerPrefs.Save();
                highScore = score; // Cập nhật để hiển thị đúng
            }

            uiManager.ShowGameOver(score, highScore);
            Debug.Log("Lose!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Nếu bạn từng dừng game bằng Time.timeScale
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Khôi phục lại thời gian nếu bị dừng
        SceneManager.LoadScene("SceneMenu");
    }
}