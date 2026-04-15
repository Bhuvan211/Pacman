using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI pauseText;

    private bool isPaused = false;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();

        // Auto-find canvas if not assigned
        if (pauseCanvas == null)
            pauseCanvas = FindCanvasByName("PauseMenuCanvas");

        // Auto-find buttons if not assigned
        if (resumeButton == null)
            resumeButton = FindButtonByName("ResumeButton");
        if (mainMenuButton == null)
            mainMenuButton = FindButtonByName("MainMenuButton");
        if (quitButton == null)
            quitButton = FindButtonByName("QuitButton");

        // Setup button listeners
        if (resumeButton != null)
            resumeButton.onClick.AddListener(Resume);
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (quitButton != null)
            quitButton.onClick.AddListener(Quit);

        // Hide pause menu initially - CRITICAL
        if (pauseCanvas != null)
            pauseCanvas.enabled = false;
        
        isPaused = false;
        Time.timeScale = 1; // Make sure game is running
        Debug.Log("PauseMenuUI started - game should be playing");
    }

    private void Update()
    {
        // Listen for pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        
        if (pauseCanvas != null)
            pauseCanvas.enabled = true;
        
        GameManager.Instance.SetGameState(GameManager.GameState.Paused);
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void Resume()
    {
        isPaused = false;
        
        if (pauseCanvas != null)
            pauseCanvas.enabled = false;
        
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
        Time.timeScale = 1;
        Debug.Log("Game Resumed");
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1; // Unpause before loading scene
        SceneManager.LoadScene("MainMenu");
    }

    private void Quit()
    {
        Time.timeScale = 1; // Unpause before quitting
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private Canvas FindCanvasByName(string name)
    {
        Canvas[] allCanvases = FindObjectsByType<Canvas>();
        foreach (Canvas c in allCanvases)
        {
            if (c.name == name)
                return c;
        }
        return null;
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

    public bool IsPaused() => isPaused;
}
