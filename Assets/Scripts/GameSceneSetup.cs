using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
#endif

public class GameSceneSetup : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Pac-Man/Setup Level_01 Scene")]
    public static void SetupLevel01Scene()
    {
        // Load Level_01 scene
        string level01Path = "Assets/Scenes/Level_01.unity";
        Scene level01Scene = EditorSceneManager.OpenScene(level01Path, OpenSceneMode.Single);

        // Clear existing canvases
        Canvas[] existingCanvases = FindObjectsByType<Canvas>();
        foreach (Canvas c in existingCanvases)
        {
            DestroyImmediate(c.gameObject);
        }

        // Clear existing characters (Chomp and Ghosts) - destroy by NAME to catch all
        Transform[] allObjects = FindObjectsByType<Transform>();
        foreach (Transform t in allObjects)
        {
            if (t != null && (t.name == "Chomp" || t.name.StartsWith("Ghost_") || 
                t.name.Contains("Dot") || t.name.Contains("Pellet_Spawned")))
            {
                DestroyImmediate(t.gameObject);
            }
        }

        // Also clear by component types
        PlayerController[] existingPlayers = FindObjectsByType<PlayerController>();
        foreach (PlayerController p in existingPlayers)
        {
            DestroyImmediate(p.gameObject);
        }

        GhostAI[] existingGhosts = FindObjectsByType<GhostAI>();
        foreach (GhostAI g in existingGhosts)
        {
            DestroyImmediate(g.gameObject);
        }

        // Save scene to persist cleanup
        EditorSceneManager.SaveScene(level01Scene);
        Debug.Log("Cleaned up duplicates and saved");

        // Add GameManager if missing
        if (FindAnyObjectByType<GameManager>() == null)
        {
            GameObject gmObj = new GameObject("GameManager");
            gmObj.AddComponent<GameManager>();
            Debug.Log("Added GameManager to Level_01");
        }

        // Add GameplayManager if missing
        if (FindAnyObjectByType<GameplayManager>() == null)
        {
            GameObject gpObj = new GameObject("GameplayManager");
            gpObj.AddComponent<GameplayManager>();
            Debug.Log("Added GameplayManager to Level_01");
        }

        // Create Game Level UI (pause menu, game over, win screen, pellet counter)
        GameLevelUISetup.CreateGameLevelUI();

        // Create MazeGenerator for procedural maze generation
        MazeGenerator mazeGen = FindAnyObjectByType<MazeGenerator>();
        if (mazeGen == null)
        {
            GameObject mazeGenObj = new GameObject("MazeGenerator");
            mazeGen = mazeGenObj.AddComponent<MazeGenerator>();
            mazeGen.GenerateMaze();
            Debug.Log("Created MazeGenerator and generated maze");
        }
        else
        {
            // Regenerate if already exists
            mazeGen.GenerateMaze();
        }

        // Instantiate Chomp (Player) if not already present
        GameObject existingChomp = GameObject.Find("Chomp");
        if (existingChomp == null && FindAnyObjectByType<PlayerController>() == null)
        {
            GameObject chompPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Characters/Chomp.prefab");
            if (chompPrefab != null)
            {
                GameObject chomp = (GameObject)PrefabUtility.InstantiatePrefab(chompPrefab);
                chomp.name = "Chomp";
                // Position player in center of maze (safe spawn point)
                chomp.transform.position = new Vector3(0, 0.5f, 0);
                Debug.Log($"Instantiated Chomp (Player) at {chomp.transform.position}");
            }
        }
        else if (existingChomp != null)
        {
            Debug.Log("Chomp already exists, repositioning...");
            existingChomp.transform.position = new Vector3(0, 0.5f, 0);
        }

        // Instantiate Ghosts if not already present - check both by name and by component
        GhostAI[] existingGhostAIs = FindObjectsByType<GhostAI>();
        if (existingGhostAIs.Length == 0 && 
            GameObject.Find("Ghost_1") == null &&
            GameObject.Find("Ghost_2") == null &&
            GameObject.Find("Ghost_3") == null &&
            GameObject.Find("Ghost_4") == null)
        {
            string[] ghostVariants = new string[]
            {
                "Assets/Prefabs/Characters/Ghost_Red Variant.prefab",
                "Assets/Prefabs/Characters/Ghost_Pink Variant.prefab",
                "Assets/Prefabs/Characters/Ghost_LitBlue Variant.prefab",
                "Assets/Prefabs/Characters/Ghost_Orange Variant.prefab"
            };

            for (int i = 0; i < ghostVariants.Length; i++)
            {
                GameObject ghostPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(ghostVariants[i]);
                if (ghostPrefab != null)
                {
                    GameObject ghost = (GameObject)PrefabUtility.InstantiatePrefab(ghostPrefab);
                    ghost.name = "Ghost_" + (i + 1);
                    
                    // Position ghosts in ghost house area (center of map)
                    if (i == 0) ghost.transform.position = new Vector3(-0.5f, 0, -0.5f);
                    else if (i == 1) ghost.transform.position = new Vector3(0.5f, 0, -0.5f);
                    else if (i == 2) ghost.transform.position = new Vector3(-0.5f, 0, 0.5f);
                    else if (i == 3) ghost.transform.position = new Vector3(0.5f, 0, 0.5f);
                    
                    // Assign ghost types
                    GhostAI ghostAI = ghost.GetComponent<GhostAI>();
                    if (ghostAI == null)
                        ghostAI = ghost.AddComponent<GhostAI>();
                    
                    // Use reflection to set the ghost type
                    System.Reflection.FieldInfo ghostTypeField = typeof(GhostAI).GetField("ghostType", 
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (ghostTypeField != null)
                    {
                        ghostTypeField.SetValue(ghostAI, (GhostAI.GhostType)i);
                        Debug.Log($"Set {ghost.name} type to {(GhostAI.GhostType)i}");
                    }
                    
                    Debug.Log($"Instantiated {ghost.name} at {ghost.transform.position}");
                }
            }
        }
        else if (existingGhostAIs.Length > 0)
        {
            Debug.Log($"Ghosts already exist: {existingGhostAIs.Length} found");
        }

        // Check if Main Camera exists
        Camera mainCamera = FindAnyObjectByType<Camera>();
        if (mainCamera == null)
        {
            GameObject cameraObj = new GameObject("Main Camera");
            Camera camera = cameraObj.AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.backgroundColor = new Color(0.05f, 0.05f, 0.1f, 1f);
            
            // Position camera to see the entire maze from isometric view
            cameraObj.transform.position = new Vector3(0, 16, 10);
            cameraObj.transform.LookAt(Vector3.zero);
            camera.orthographic = false;
            camera.fieldOfView = 60;
            camera.farClipPlane = 1000;
            camera.nearClipPlane = 0.1f;
            
            // Add camera follower to make it follow the player
            cameraObj.AddComponent<CameraFollower>();
            
            Debug.Log("Created Main Camera viewing maze");
        }
        else
        {
            // Reposition existing camera to see maze
            mainCamera.transform.position = new Vector3(0, 16, 10);
            mainCamera.transform.LookAt(Vector3.zero);
            mainCamera.fieldOfView = 60;
            mainCamera.backgroundColor = new Color(0.05f, 0.05f, 0.1f, 1f);
            
            // Add camera follower if not already present
            if (mainCamera.GetComponent<CameraFollower>() == null)
            {
                mainCamera.gameObject.AddComponent<CameraFollower>();
            }
            Debug.Log("Repositioned Main Camera to view maze");
        }

        // Add lighting if missing
        Light[] lights = FindObjectsByType<Light>();
        if (lights.Length == 0)
        {
            GameObject lightObj = new GameObject("Main Light");
            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            lightObj.transform.rotation = Quaternion.Euler(60, 45, 0);
            Debug.Log("Created Main Light");
        }

        // Add UIManager if missing
        if (FindAnyObjectByType<UIManager>() == null)
        {
            GameObject uiManagerObj = new GameObject("UIManager");
            uiManagerObj.AddComponent<UIManager>();
            Debug.Log("Added UIManager to Level_01");
        }

        // Add PauseMenuUI if missing
        if (FindAnyObjectByType<PauseMenuUI>() == null)
        {
            GameObject pauseMenuObj = new GameObject("PauseMenuUI");
            pauseMenuObj.AddComponent<PauseMenuUI>();
            Debug.Log("Added PauseMenuUI to Level_01");
        }

        // Add AudioManager if missing
        if (FindAnyObjectByType<AudioManager>() == null)
        {
            GameObject audioManagerObj = new GameObject("AudioManager");
            audioManagerObj.AddComponent<AudioManager>();
            Debug.Log("Added AudioManager to Level_01");
        }

        // Add PowerPelletManager if missing
        if (FindAnyObjectByType<PowerPelletManager>() == null)
        {
            GameObject powerPelletObj = new GameObject("PowerPelletManager");
            powerPelletObj.AddComponent<PowerPelletManager>();
            Debug.Log("Added PowerPelletManager to Level_01");
        }

        // Add LevelManager if missing
        if (FindAnyObjectByType<LevelManager>() == null)
        {
            GameObject levelObj = new GameObject("LevelManager");
            levelObj.AddComponent<LevelManager>();
            Debug.Log("Added LevelManager to Level_01");
        }

        // Add DifficultyManager if missing
        if (FindAnyObjectByType<DifficultyManager>() == null)
        {
            GameObject difficultyObj = new GameObject("DifficultyManager");
            difficultyObj.AddComponent<DifficultyManager>();
            Debug.Log("Added DifficultyManager to Level_01");
        }

        // Clean up colliders on managers (they shouldn't have any)
        CleanupManagerColliders();

        // Add GameInitializer if missing
        if (FindAnyObjectByType<GameInitializer>() == null)
        {
            GameObject initObj = new GameObject("GameInitializer");
            initObj.AddComponent<GameInitializer>();
            Debug.Log("Added GameInitializer to Level_01");
        }

        // Save scene
        EditorSceneManager.SaveScene(level01Scene);

        Debug.Log("✓ Level_01 Pac-Man game scene setup complete!");
    }

    private static void CleanupManagerColliders()
    {
        // Remove colliders from manager GameObjects as they shouldn't collide with anything
        PowerPelletManager ppm = FindAnyObjectByType<PowerPelletManager>();
        if (ppm != null)
        {
            Collider col = ppm.gameObject.GetComponent<Collider>();
            if (col != null)
            {
                DestroyImmediate(col);
                Debug.Log("Removed collider from PowerPelletManager");
            }
        }

        DifficultyManager dm = FindAnyObjectByType<DifficultyManager>();
        if (dm != null)
        {
            Collider col = dm.gameObject.GetComponent<Collider>();
            if (col != null)
            {
                DestroyImmediate(col);
                Debug.Log("Removed collider from DifficultyManager");
            }
        }

        AudioManager am = FindAnyObjectByType<AudioManager>();
        if (am != null)
        {
            Collider col = am.gameObject.GetComponent<Collider>();
            if (col != null)
            {
                DestroyImmediate(col);
                Debug.Log("Removed collider from AudioManager");
            }
        }

        UIManager um = FindAnyObjectByType<UIManager>();
        if (um != null)
        {
            Collider col = um.gameObject.GetComponent<Collider>();
            if (col != null)
            {
                DestroyImmediate(col);
                Debug.Log("Removed collider from UIManager");
            }
        }
    }
#endif
}
