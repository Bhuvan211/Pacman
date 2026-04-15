using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

public class MenuSceneSetup : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Pac-Man/Setup Main Menu Scene")]
    public static void SetupMainMenuScene()
    {
        // Create or load MainMenu scene
        string mainMenuPath = "Assets/Scenes/MainMenu.unity";
        Scene mainMenuScene;

        // Check if MainMenu scene exists
        if (AssetDatabase.LoadAssetAtPath(mainMenuPath, typeof(SceneAsset)) != null)
        {
            mainMenuScene = EditorSceneManager.OpenScene(mainMenuPath, OpenSceneMode.Single);
            Debug.Log("Loaded existing MainMenu scene");
        }
        else
        {
            mainMenuScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(mainMenuScene, mainMenuPath);
            Debug.Log("Created new MainMenu scene");
        }

        // Clear existing canvases
        Canvas[] existingCanvases = FindObjectsByType<Canvas>();
        foreach (Canvas c in existingCanvases)
        {
            DestroyImmediate(c.gameObject);
        }

        // Create UI in this scene
        MainMenuSetup.CreateMainMenuUI();

        // Save scene
        EditorSceneManager.SaveScene(mainMenuScene);

        Debug.Log("✓ Main Menu scene setup complete!");
    }

    [MenuItem("Pac-Man/Setup Build Settings")]
    public static void SetupBuildSettings()
    {
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[2];

        // MainMenu (Scene 0)
        scenes[0] = new EditorBuildSettingsScene("Assets/Scenes/MainMenu.unity", true);

        // Level_01 (Scene 1)
        scenes[1] = new EditorBuildSettingsScene("Assets/Scenes/Level_01.unity", true);

        EditorBuildSettings.scenes = scenes;

        Debug.Log("✓ Build settings configured!");
        Debug.Log("Scene 0 (Startup): MainMenu");
        Debug.Log("Scene 1: Level_01");
    }

    [MenuItem("Pac-Man/Complete Setup")]
    public static void CompleteSetup()
    {
        SetupMainMenuScene();
        GameSceneSetup.SetupLevel01Scene();
        SetupBuildSettings();
        Debug.Log("✓ All setup complete!");
    }
#endif
}
