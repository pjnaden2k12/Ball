using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Menu : MonoBehaviour
{
    public GameObject highScorePanel;
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;

    public void OnPlayButton()
    {
        HideAllPanels();
        SceneManager.LoadScene("ScenePlay"); // Đổi tên scene nếu cần
    }

    public void OnHighScoreButton()
    {
        HideAllPanels();
        highScorePanel.SetActive(true);
    }

    public void OnSettingButton()
    {
        HideAllPanels();
        settingsPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        highScorePanel.SetActive(false);
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        if (highScorePanel != null) highScorePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
    }
}