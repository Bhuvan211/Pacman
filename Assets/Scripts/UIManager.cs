using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pelletCountText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    private GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = FindAnyObjectByType<GameplayManager>();
        
        // Ensure EventSystem exists for button clicks to work
        if (FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            Debug.Log("✓ Created EventSystem");
        }
        
        // Find or create canvases - search thoroughly
        if (gameOverCanvas == null)
        {
            gameOverCanvas = FindCanvasByName("GameOverCanvas");
            if (gameOverCanvas != null)
                Debug.Log("✓ Found GameOverCanvas");
            else
            {
                Debug.LogWarning("⚠ GameOverCanvas not found! Creating at runtime.");
                gameOverCanvas = CreateGameOverCanvas();
            }
        }
        
        if (winCanvas == null)
        {
            winCanvas = FindCanvasByName("WinCanvas");
            if (winCanvas != null)
                Debug.Log("✓ Found WinCanvas");
            else
            {
                Debug.LogWarning("⚠ WinCanvas not found! Creating at runtime.");
                winCanvas = CreateWinCanvas();
            }
        }
        
        // Find or create text
        if (pelletCountText == null)
            pelletCountText = FindTextByName("PelletCountText");

        // Find restart buttons
        if (restartButton == null)
            restartButton = FindButtonByName("RestartButton");
        if (mainMenuButton == null)
            mainMenuButton = FindButtonByName("MainMenuButton");

        // Hide menus initially
        if (gameOverCanvas != null) gameOverCanvas.enabled = false;
        if (winCanvas != null) winCanvas.enabled = false;

        // Setup button listeners
        if (restartButton != null)
        {
            restartButton.interactable = true; // Ensure button is interactive
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("✓ RestartButton listener attached");
        }
        else
        {
            Debug.LogError("RestartButton not found!");
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.interactable = true;
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            Debug.Log("✓ MainMenuButton listener attached");
        }
        else
        {
            Debug.LogError("MainMenuButton not found!");
        }
            
        Debug.Log("UIManager initialized");
    }

    private void Update()
    {
        // Update all UI elements
        UpdatePelletDisplay();
        UpdateScoreDisplay();
        UpdateLivesDisplay();
        UpdateLevelDisplay();
        
        // Fallback: Press R to restart when game over
        if (gameOverCanvas != null && gameOverCanvas.enabled && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed - restarting!");
            RestartGame();
        }
    }

    private void UpdatePelletDisplay()
    {
        if (pelletCountText != null && gameplayManager != null)
        {
            int collected = gameplayManager.GetPelletsCollected();
            int needed = gameplayManager.GetPelletsNeeded();
            pelletCountText.text = $"Pellets: {collected}/{needed}";
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null && gameplayManager != null)
        {
            scoreText.text = $"Score: {gameplayManager.GetScore()}";
        }
    }

    private void UpdateLivesDisplay()
    {
        if (livesText != null && gameplayManager != null)
        {
            livesText.text = $"Lives: {gameplayManager.GetLives()}";
        }
    }

    private void UpdateLevelDisplay()
    {
        if (levelText != null && gameplayManager != null)
        {
            levelText.text = $"Level: {gameplayManager.GetLevel()}";
        }
    }

    public void UpdateScore(int newScore)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {newScore}";
    }

    public void ShowGameOver()
    {
        if (gameOverCanvas != null)
        {
            Debug.Log("Showing Game Over screen");
            
            // Ensure canvas is fully visible and interactive
            gameOverCanvas.enabled = true;
            gameOverCanvas.gameObject.SetActive(true);
            
            // Make sure the button is clickable
            if (restartButton != null)
            {
                restartButton.gameObject.SetActive(true);
                restartButton.interactable = true;
                Debug.Log("✓ RestartButton is active and interactable");
            }
            
            Time.timeScale = 0; // Pause the game
            
            Debug.Log("GAME OVER screen displayed. Press R or click RESTART to continue.");
        }
        else
        {
            Debug.LogError("GameOverCanvas not found!");
        }
    }

    public void ShowWinScreen()
    {
        if (winCanvas != null)
        {
            Debug.Log("Showing Win screen");
            winCanvas.enabled = true;
            winCanvas.gameObject.SetActive(true);  // Ensure it's active
            Time.timeScale = 0; // Pause the game
        }
        else
        {
            Debug.LogError("WinCanvas not found!");
        }
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame() called!");
        Time.timeScale = 1; // Unpause the game
        Debug.Log("Time.timeScale set to 1");
        
        // Use coroutine to ensure proper timing
        StartCoroutine(PerformRestart());
    }

    private System.Collections.IEnumerator PerformRestart()
    {
        Debug.Log("PerformRestart coroutine started");
        yield return null; // Wait one frame to ensure time is unpaused
        
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Debug.Log($"Reloading scene: {sceneName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        Debug.Log("Going to Main Menu...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Debug.Log("Quitting game...");
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

    private TextMeshProUGUI FindTextByName(string name)
    {
        TextMeshProUGUI[] allTexts = FindObjectsByType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in allTexts)
        {
            if (t.name == name)
                return t;
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

    private Canvas CreateGameOverCanvas()
    {
        // Create Game Over Canvas
        GameObject gameOverCanvasObj = new GameObject("GameOverCanvas");
        Canvas gameOverCanvas = gameOverCanvasObj.AddComponent<Canvas>();
        gameOverCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = gameOverCanvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        gameOverCanvasObj.AddComponent<GraphicRaycaster>();

        // Create panel
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(gameOverCanvasObj.transform, false);
        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0.2f, 0.1f, 0.1f, 0.9f);
        
        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // Create text
        GameObject textObj = new GameObject("GameOverText");
        textObj.transform.SetParent(panelObj.transform, false);
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = "GAME OVER";
        text.fontSize = 80;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.red;
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector2(0, 100);
        textRect.sizeDelta = new Vector2(800, 200);

        // Create Restart button
        GameObject restartObj = new GameObject("RestartButton");
        restartObj.transform.SetParent(panelObj.transform, false);
        Image restartImage = restartObj.AddComponent<Image>();
        restartImage.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        Button restartBtn = restartObj.AddComponent<Button>();
        restartBtn.targetGraphic = restartImage;
        restartBtn.interactable = true;
        
        RectTransform restartRect = restartObj.GetComponent<RectTransform>();
        restartRect.anchoredPosition = new Vector2(0, -100);
        restartRect.sizeDelta = new Vector2(300, 80);
        
        restartButton = restartBtn;
        // Listener will be added in Start() method

        // Restart button text
        GameObject restartTextObj = new GameObject("Text");
        restartTextObj.transform.SetParent(restartObj.transform, false);
        TextMeshProUGUI restartText = restartTextObj.AddComponent<TextMeshProUGUI>();
        restartText.text = "RESTART";
        restartText.fontSize = 50;
        restartText.alignment = TextAlignmentOptions.Center;
        
        RectTransform restartTextRect = restartTextObj.GetComponent<RectTransform>();
        restartTextRect.anchorMin = Vector2.zero;
        restartTextRect.anchorMax = Vector2.one;
        restartTextRect.offsetMin = Vector2.zero;
        restartTextRect.offsetMax = Vector2.zero;

        Debug.Log("✓ Created GameOverCanvas at runtime");
        return gameOverCanvas;
    }

    private Canvas CreateWinCanvas()
    {
        // Create Win Canvas
        GameObject winCanvasObj = new GameObject("WinCanvas");
        Canvas winCanvas = winCanvasObj.AddComponent<Canvas>();
        winCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = winCanvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        winCanvasObj.AddComponent<GraphicRaycaster>();

        // Create panel
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(winCanvasObj.transform, false);
        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.2f, 0.1f, 0.9f);
        
        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // Create text
        GameObject textObj = new GameObject("WinText");
        textObj.transform.SetParent(panelObj.transform, false);
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = "YOU WIN!";
        text.fontSize = 80;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.green;
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector2(0, 100);
        textRect.sizeDelta = new Vector2(800, 200);

        // Create Restart button
        GameObject restartObj = new GameObject("RestartButton");
        restartObj.transform.SetParent(panelObj.transform, false);
        Image restartImage = restartObj.AddComponent<Image>();
        restartImage.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        Button restartBtn = restartObj.AddComponent<Button>();
        restartBtn.targetGraphic = restartImage;
        
        RectTransform restartRect = restartObj.GetComponent<RectTransform>();
        restartRect.anchoredPosition = new Vector2(0, -100);
        restartRect.sizeDelta = new Vector2(300, 80);
        
        restartButton = restartBtn;
        restartButton.onClick.AddListener(RestartGame);

        // Restart button text
        GameObject restartTextObj = new GameObject("Text");
        restartTextObj.transform.SetParent(restartObj.transform, false);
        TextMeshProUGUI restartText = restartTextObj.AddComponent<TextMeshProUGUI>();
        restartText.text = "RESTART";
        restartText.fontSize = 50;
        restartText.alignment = TextAlignmentOptions.Center;
        
        RectTransform restartTextRect = restartTextObj.GetComponent<RectTransform>();
        restartTextRect.anchorMin = Vector2.zero;
        restartTextRect.anchorMax = Vector2.one;
        restartTextRect.offsetMin = Vector2.zero;
        restartTextRect.offsetMax = Vector2.zero;

        Debug.Log("✓ Created WinCanvas at runtime");
        return winCanvas;
    }
}
