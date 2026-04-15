using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    private void Awake()
    {
        // Check if we're on the MainMenu scene
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene != "MainMenu" && currentScene != "Level_01")
        {
            Debug.LogWarning($"Unknown scene: {currentScene}. Loading Level_01 instead.");
            SceneManager.LoadScene("Level_01");
        }

        // Ensure MainMenuManager exists on MainMenu scene
        if (currentScene == "MainMenu" && FindAnyObjectByType<MainMenuManager>() == null)
        {
            GameObject mmObj = new GameObject("MainMenuManager");
            mmObj.AddComponent<MainMenuManager>();
        }
    }
}
