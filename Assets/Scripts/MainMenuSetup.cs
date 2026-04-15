using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class MainMenuSetup : MonoBehaviour
{
#if UNITY_EDITOR
    public static void CreateMainMenuUI()
    {
        // Create root canvas
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create background panel
        GameObject panelObj = new GameObject("Panel");
        panelObj.transform.SetParent(canvasObj.transform, false);
        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.2f, 1f);
        
        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // Create title text
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(panelObj.transform, false);
        TextMeshProUGUI titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = "PAC-MAN";
        titleText.fontSize = 80;
        titleText.alignment = TextAlignmentOptions.Top;
        titleText.color = Color.yellow;
        
        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, -100);
        titleRect.sizeDelta = new Vector2(800, 200);

        // Create high score text
        GameObject scoreObj = new GameObject("HighScoreText");
        scoreObj.transform.SetParent(panelObj.transform, false);
        TextMeshProUGUI scoreText = scoreObj.AddComponent<TextMeshProUGUI>();
        scoreText.text = $"High Score: {HighScoreManager.GetHighScore()}";
        scoreText.fontSize = 40;
        scoreText.alignment = TextAlignmentOptions.Center;
        scoreText.color = Color.white;
        
        RectTransform scoreRect = scoreObj.GetComponent<RectTransform>();
        scoreRect.anchoredPosition = new Vector2(0, -300);
        scoreRect.sizeDelta = new Vector2(800, 100);

        // Create Play Button
        GameObject playButtonObj = new GameObject("PlayButton");
        playButtonObj.transform.SetParent(panelObj.transform, false);
        
        Image playImage = playButtonObj.AddComponent<Image>();
        playImage.color = new Color(0.2f, 0.8f, 0.2f, 1f);
        
        Button playButton = playButtonObj.AddComponent<Button>();
        playButton.targetGraphic = playImage;
        
        RectTransform playRect = playButtonObj.GetComponent<RectTransform>();
        playRect.anchoredPosition = new Vector2(0, -450);
        playRect.sizeDelta = new Vector2(300, 80);

        // Play button text
        GameObject playTextObj = new GameObject("Text");
        playTextObj.transform.SetParent(playButtonObj.transform, false);
        TextMeshProUGUI playText = playTextObj.AddComponent<TextMeshProUGUI>();
        playText.text = "PLAY";
        playText.fontSize = 60;
        playText.alignment = TextAlignmentOptions.Center;
        
        RectTransform playTextRect = playTextObj.GetComponent<RectTransform>();
        playTextRect.anchorMin = Vector2.zero;
        playTextRect.anchorMax = Vector2.one;
        playTextRect.offsetMin = Vector2.zero;
        playTextRect.offsetMax = Vector2.zero;

        // Create Quit Button
        GameObject quitButtonObj = new GameObject("QuitButton");
        quitButtonObj.transform.SetParent(panelObj.transform, false);
        
        Image quitImage = quitButtonObj.AddComponent<Image>();
        quitImage.color = new Color(0.8f, 0.2f, 0.2f, 1f);
        
        Button quitButton = quitButtonObj.AddComponent<Button>();
        quitButton.targetGraphic = quitImage;
        
        RectTransform quitRect = quitButtonObj.GetComponent<RectTransform>();
        quitRect.anchoredPosition = new Vector2(0, -580);
        quitRect.sizeDelta = new Vector2(300, 80);

        // Quit button text
        GameObject quitTextObj = new GameObject("Text");
        quitTextObj.transform.SetParent(quitButtonObj.transform, false);
        TextMeshProUGUI quitText = quitTextObj.AddComponent<TextMeshProUGUI>();
        quitText.text = "QUIT";
        quitText.fontSize = 60;
        quitText.alignment = TextAlignmentOptions.Center;
        
        RectTransform quitTextRect = quitTextObj.GetComponent<RectTransform>();
        quitTextRect.anchorMin = Vector2.zero;
        quitTextRect.anchorMax = Vector2.one;
        quitTextRect.offsetMin = Vector2.zero;
        quitTextRect.offsetMax = Vector2.zero;

        // Add MainMenuManager
        canvasObj.AddComponent<MainMenuManager>();

        EditorGUIUtility.PingObject(canvasObj);
        Selection.activeGameObject = canvasObj;

        Debug.Log("✓ Main Menu UI created successfully!");
    }
#endif
}
