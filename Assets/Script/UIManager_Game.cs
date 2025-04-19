using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Game : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI highScoreText;

    public void ShowGameOver(int currentScore, int highScore)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        scoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;
    }

    public void UpdateScoreUI(int currentScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SceneMenu");
    }
}