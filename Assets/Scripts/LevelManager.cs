using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int maxLevel = 10;
    
    private int pelletsPerLevel = 50;
    private float ghostSpeedMultiplier = 1f;
    private float playerSpeedBoost = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelNum)
    {
        currentLevel = Mathf.Clamp(levelNum, 1, maxLevel);
        
        // Scale difficulty with level
        ghostSpeedMultiplier = 1f + (currentLevel - 1) * 0.1f;
        playerSpeedBoost = 1f;
        pelletsPerLevel = 50 + (currentLevel - 1) * 5;
        
        Debug.Log($"===== LEVEL {currentLevel} =====");
        Debug.Log($"Ghost Speed Multiplier: {ghostSpeedMultiplier}x");
        Debug.Log($"Pellets to collect: {pelletsPerLevel}");
        
        // Update ghost speeds
        GhostAI[] ghosts = FindObjectsByType<GhostAI>();
        foreach (GhostAI ghost in ghosts)
        {
            // You'd need to expose this in GhostAI
            Debug.Log($"Ghost {ghost.gameObject.name} difficulty adjusted for level {currentLevel}");
        }
    }

    public void NextLevel()
    {
        if (currentLevel < maxLevel)
        {
            LoadLevel(currentLevel + 1);
        }
        else
        {
            Debug.Log("YOU BEAT THE GAME!");
        }
    }

    public int GetLevel() => currentLevel;
    public float GetGhostSpeedMultiplier() => ghostSpeedMultiplier;
    public int GetPelletsNeeded() => pelletsPerLevel;
    public float GetPlayerSpeedBoost() => playerSpeedBoost;

    public void SpawnFruit()
    {
        // Spawn fruit at random location
        Vector3 fruitPos = new Vector3(
            Random.Range(-6f, 6f),
            0.5f,
            Random.Range(-6f, 6f)
        );
        
        GameObject fruitObj = new GameObject($"Fruit_Level{currentLevel}");
        fruitObj.transform.position = fruitPos;
        
        FruitBonus fruit = fruitObj.AddComponent<FruitBonus>();
        
        // Add mesh for visualization
        GameObject sphere = new GameObject("Visual");
        sphere.transform.parent = fruitObj.transform;
        sphere.transform.localPosition = Vector3.zero;
        MeshFilter mf = sphere.AddComponent<MeshFilter>();
        MeshRenderer mr = sphere.AddComponent<MeshRenderer>();
        mf.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
        sphere.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        
        Debug.Log($"Spawned fruit at {fruitPos}");
    }
}
