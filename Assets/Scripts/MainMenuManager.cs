using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        // Auto-find buttons if not assigned
        if (playButton == null)
            playButton = FindButtonByName("PlayButton");
        if (quitButton == null)
            quitButton = FindButtonByName("QuitButton");

        // Setup button listeners
        if (playButton != null)
            playButton.onClick.AddListener(PlayGame);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        // Display high score
        UpdateHighScoreDisplay();
    }

    private void PlayGame()
    {
        Time.timeScale = 1; // Ensure game time is normal
        SceneManager.LoadScene("Level_01");
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            int highScore = HighScoreManager.GetHighScore();
            highScoreText.text = $"High Score: {highScore}";
        }
    }

    private Button FindButtonByName(string name)
    {
        Button[] allButtons = FindObjectsByType<Button>();
        foreach (Button b in allButtons)
        {
            if (b.name == name)
                return b;
        }
        return null;
    }
}
