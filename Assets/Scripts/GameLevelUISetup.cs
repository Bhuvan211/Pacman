using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class GameLevelUISetup : MonoBehaviour
{
#if UNITY_EDITOR
    public static void CreateGameLevelUI()
    {
        // Create pause menu canvas
        GameObject pauseCanvasObj = new GameObject("PauseMenuCanvas");
        Canvas pauseCanvas = pauseCanvasObj.AddComponent<Canvas>();
        pauseCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler canvasScaler = pauseCanvasObj.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        pauseCanvasObj.AddComponent<GraphicRaycaster>();

        // Create pause background panel
        GameObject pausePanelObj = new GameObject("Panel");
        pausePanelObj.transform.SetParent(pauseCanvasObj.transform, false);
        Image pausePanelImage = pausePanelObj.AddComponent<Image>();
        pausePanelImage.color = new Color(0.1f, 0.1f, 0.2f, 0.8f);
        
        RectTransform pausePanelRect = pausePanelObj.GetComponent<RectTransform>();
        pausePanelRect.anchorMin = Vector2.zero;
        pausePanelRect.anchorMax = Vector2.one;
        pausePanelRect.offsetMin = Vector2.zero;
        pausePanelRect.offsetMax = Vector2.zero;

        // Create pause text
        GameObject pauseTextObj = new GameObject("PauseText");
        pauseTextObj.transform.SetParent(pausePanelObj.transform, false);
        TextMeshProUGUI pauseText = pauseTextObj.AddComponent<TextMeshProUGUI>();
        pauseText.text = "PAUSED";
        pauseText.fontSize = 80;
        pauseText.alignment = TextAlignmentOptions.Top;
        pauseText.color = Color.yellow;
        
        RectTransform pauseTextRect = pauseTextObj.GetComponent<RectTransform>();
        pauseTextRect.anchoredPosition = new Vector2(0, -100);
        pauseTextRect.sizeDelta = new Vector2(800, 200);

        // Resume Button
        GameObject resumeButtonObj = new GameObject("ResumeButton");
        resumeButtonObj.transform.SetParent(pausePanelObj.transform, false);
        
        Image resumeImage = resumeButtonObj.AddComponent<Image>();
        resumeImage.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        
        Button resumeButton = resumeButtonObj.AddComponent<Button>();
        resumeButton.targetGraphic = resumeImage;
        
        RectTransform resumeRect = resumeButtonObj.GetComponent<RectTransform>();
        resumeRect.anchoredPosition = new Vector2(0, -350);
        resumeRect.sizeDelta = new Vector2(300, 80);

        // Resume button text
        GameObject resumeTextObj = new GameObject("Text");
        resumeTextObj.transform.SetParent(resumeButtonObj.transform, false);
        TextMeshProUGUI resumeText = resumeTextObj.AddComponent<TextMeshProUGUI>();
        resumeText.text = "RESUME";
        resumeText.fontSize = 50;
        resumeText.alignment = TextAlignmentOptions.Center;
        
        RectTransform resumeTextRect = resumeTextObj.GetComponent<RectTransform>();
        resumeTextRect.anchorMin = Vector2.zero;
        resumeTextRect.anchorMax = Vector2.one;
        resumeTextRect.offsetMin = Vector2.zero;
        resumeTextRect.offsetMax = Vector2.zero;

        // Main Menu Button
        GameObject mainMenuButtonObj = new GameObject("MainMenuButton");
        mainMenuButtonObj.transform.SetParent(pausePanelObj.transform, false);
        
        Image mainMenuImage = mainMenuButtonObj.AddComponent<Image>();
        mainMenuImage.color = new Color(0.8f, 0.6f, 0.2f, 1f);
        
        Button mainMenuButton = mainMenuButtonObj.AddComponent<Button>();
        mainMenuButton.targetGraphic = mainMenuImage;
        
        RectTransform mainMenuRect = mainMenuButtonObj.GetComponent<RectTransform>();
        mainMenuRect.anchoredPosition = new Vector2(0, -460);
        mainMenuRect.sizeDelta = new Vector2(300, 80);

        // Main Menu button text
        GameObject mainMenuTextObj = new GameObject("Text");
        mainMenuTextObj.transform.SetParent(mainMenuButtonObj.transform, false);
        TextMeshProUGUI mainMenuText = mainMenuTextObj.AddComponent<TextMeshProUGUI>();
        mainMenuText.text = "MAIN MENU";
        mainMenuText.fontSize = 40;
        mainMenuText.alignment = TextAlignmentOptions.Center;
        
        RectTransform mainMenuTextRect = mainMenuTextObj.GetComponent<RectTransform>();
        mainMenuTextRect.anchorMin = Vector2.zero;
        mainMenuTextRect.anchorMax = Vector2.one;
        mainMenuTextRect.offsetMin = Vector2.zero;
        mainMenuTextRect.offsetMax = Vector2.zero;

        // Quit Button
        GameObject quitButtonObj = new GameObject("QuitButton");
        quitButtonObj.transform.SetParent(pausePanelObj.transform, false);
        
        Image quitImage = quitButtonObj.AddComponent<Image>();
        quitImage.color = new Color(0.8f, 0.2f, 0.2f, 1f);
        
        Button quitButton = quitButtonObj.AddComponent<Button>();
        quitButton.targetGraphic = quitImage;
        
        RectTransform quitRect = quitButtonObj.GetComponent<RectTransform>();
        quitRect.anchoredPosition = new Vector2(0, -570);
        quitRect.sizeDelta = new Vector2(300, 80);

        // Quit button text
        GameObject quitTextObj = new GameObject("Text");
        quitTextObj.transform.SetParent(quitButtonObj.transform, false);
        TextMeshProUGUI quitText = quitTextObj.AddComponent<TextMeshProUGUI>();
        quitText.text = "QUIT";
        quitText.fontSize = 50;
        quitText.alignment = TextAlignmentOptions.Center;
        
        RectTransform quitTextRect = quitTextObj.GetComponent<RectTransform>();
        quitTextRect.anchorMin = Vector2.zero;
        quitTextRect.anchorMax = Vector2.one;
        quitTextRect.offsetMin = Vector2.zero;
        quitTextRect.offsetMax = Vector2.zero;

        // Create game over canvas
        GameObject gameOverCanvasObj = new GameObject("GameOverCanvas");
        Canvas gameOverCanvas = gameOverCanvasObj.AddComponent<Canvas>();
        gameOverCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler gameOverScaler = gameOverCanvasObj.AddComponent<CanvasScaler>();
        gameOverScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        gameOverCanvasObj.AddComponent<GraphicRaycaster>();

        // Game Over panel
        GameObject gameOverPanelObj = new GameObject("Panel");
        gameOverPanelObj.transform.SetParent(gameOverCanvasObj.transform, false);
        Image gameOverImage = gameOverPanelObj.AddComponent<Image>();
        gameOverImage.color = new Color(0.2f, 0.1f, 0.1f, 0.9f);
        
        RectTransform gameOverRect = gameOverPanelObj.GetComponent<RectTransform>();
        gameOverRect.anchorMin = Vector2.zero;
        gameOverRect.anchorMax = Vector2.one;
        gameOverRect.offsetMin = Vector2.zero;
        gameOverRect.offsetMax = Vector2.zero;

        // Game Over text
        GameObject gameOverTextObj = new GameObject("GameOverText");
        gameOverTextObj.transform.SetParent(gameOverPanelObj.transform, false);
        TextMeshProUGUI gameOverText = gameOverTextObj.AddComponent<TextMeshProUGUI>();
        gameOverText.text = "GAME OVER";
        gameOverText.fontSize = 80;
        gameOverText.alignment = TextAlignmentOptions.Center;
        gameOverText.color = Color.red;
        
        RectTransform gameOverTextRect = gameOverTextObj.GetComponent<RectTransform>();
        gameOverTextRect.anchoredPosition = new Vector2(0, 100);
        gameOverTextRect.sizeDelta = new Vector2(800, 200);

        // Game Over Restart Button
        GameObject goRestartObj = new GameObject("RestartButton");
        goRestartObj.transform.SetParent(gameOverPanelObj.transform, false);
        
        Image goRestartImage = goRestartObj.AddComponent<Image>();
        goRestartImage.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        
        Button goRestartButton = goRestartObj.AddComponent<Button>();
        goRestartButton.targetGraphic = goRestartImage;
        
        RectTransform goRestartRect = goRestartObj.GetComponent<RectTransform>();
        goRestartRect.anchoredPosition = new Vector2(0, -100);
        goRestartRect.sizeDelta = new Vector2(300, 80);

        // Game Over Restart Button text
        GameObject goRestartTextObj = new GameObject("Text");
        goRestartTextObj.transform.SetParent(goRestartObj.transform, false);
        TextMeshProUGUI goRestartText = goRestartTextObj.AddComponent<TextMeshProUGUI>();
        goRestartText.text = "RESTART";
        goRestartText.fontSize = 50;
        goRestartText.alignment = TextAlignmentOptions.Center;
        
        RectTransform goRestartTextRect = goRestartTextObj.GetComponent<RectTransform>();
        goRestartTextRect.anchorMin = Vector2.zero;
        goRestartTextRect.anchorMax = Vector2.one;
        goRestartTextRect.offsetMin = Vector2.zero;
        goRestartTextRect.offsetMax = Vector2.zero;

        // Create win canvas
        GameObject winCanvasObj = new GameObject("WinCanvas");
        Canvas winCanvas = winCanvasObj.AddComponent<Canvas>();
        winCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler winScaler = winCanvasObj.AddComponent<CanvasScaler>();
        winScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        winCanvasObj.AddComponent<GraphicRaycaster>();

        // Win panel
        GameObject winPanelObj = new GameObject("Panel");
        winPanelObj.transform.SetParent(winCanvasObj.transform, false);
        Image winImage = winPanelObj.AddComponent<Image>();
        winImage.color = new Color(0.1f, 0.2f, 0.1f, 0.9f);
        
        RectTransform winRect = winPanelObj.GetComponent<RectTransform>();
        winRect.anchorMin = Vector2.zero;
        winRect.anchorMax = Vector2.one;
        winRect.offsetMin = Vector2.zero;
        winRect.offsetMax = Vector2.zero;

        // Win text
        GameObject winTextObj = new GameObject("WinText");
        winTextObj.transform.SetParent(winPanelObj.transform, false);
        TextMeshProUGUI winText = winTextObj.AddComponent<TextMeshProUGUI>();
        winText.text = "YOU WIN!";
        winText.fontSize = 80;
        winText.alignment = TextAlignmentOptions.Center;
        winText.color = Color.green;
        
        RectTransform winTextRect = winTextObj.GetComponent<RectTransform>();
        winTextRect.anchoredPosition = new Vector2(0, 100);
        winTextRect.sizeDelta = new Vector2(800, 200);

        // Win Restart Button
        GameObject winRestartObj = new GameObject("RestartButton");
        winRestartObj.transform.SetParent(winPanelObj.transform, false);
        
        Image winRestartImage = winRestartObj.AddComponent<Image>();
        winRestartImage.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        
        Button winRestartButton = winRestartObj.AddComponent<Button>();
        winRestartButton.targetGraphic = winRestartImage;
        
        RectTransform winRestartRect = winRestartObj.GetComponent<RectTransform>();
        winRestartRect.anchoredPosition = new Vector2(0, -100);
        winRestartRect.sizeDelta = new Vector2(300, 80);

        // Win Restart Button text
        GameObject winRestartTextObj = new GameObject("Text");
        winRestartTextObj.transform.SetParent(winRestartObj.transform, false);
        TextMeshProUGUI winRestartText = winRestartTextObj.AddComponent<TextMeshProUGUI>();
        winRestartText.text = "RESTART";
        winRestartText.fontSize = 50;
        winRestartText.alignment = TextAlignmentOptions.Center;
        
        RectTransform winRestartTextRect = winRestartTextObj.GetComponent<RectTransform>();
        winRestartTextRect.anchorMin = Vector2.zero;
        winRestartTextRect.anchorMax = Vector2.one;
        winRestartTextRect.offsetMin = Vector2.zero;
        winRestartTextRect.offsetMax = Vector2.zero;

        // Create pellet counter canvas
        GameObject uiCanvasObj = new GameObject("UICanvas");
        Canvas uiCanvas = uiCanvasObj.AddComponent<Canvas>();
        uiCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler uiScaler = uiCanvasObj.AddComponent<CanvasScaler>();
        uiScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        uiCanvasObj.AddComponent<GraphicRaycaster>();

        // Pellet counter text
        GameObject pelletCountObj = new GameObject("PelletCountText");
        pelletCountObj.transform.SetParent(uiCanvasObj.transform, false);
        TextMeshProUGUI pelletCountText = pelletCountObj.AddComponent<TextMeshProUGUI>();
        pelletCountText.text = "Pellets: 0/50";
        pelletCountText.fontSize = 40;
        pelletCountText.alignment = TextAlignmentOptions.TopLeft;
        pelletCountText.color = Color.white;
        
        RectTransform pelletCountRect = pelletCountObj.GetComponent<RectTransform>();
        pelletCountRect.anchoredPosition = new Vector2(50, -50);
        pelletCountRect.sizeDelta = new Vector2(400, 100);

        // IMPORTANT: Disable pause, game over, and win canvases at start
        pauseCanvasObj.SetActive(false);
        gameOverCanvasObj.SetActive(false);
        winCanvasObj.SetActive(false);

        Debug.Log("✓ Game level UI created successfully!");
    }
#endif
}
