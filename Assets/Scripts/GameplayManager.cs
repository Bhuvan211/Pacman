using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }
    
    [SerializeField] private int pelletsNeeded = 50;
    [SerializeField] private int pelletsPointValue = 10;
    [SerializeField] private int ghostPointValue = 200;
    [SerializeField] private int startLives = 3;
    
    private int pelletsCollected = 0;
    private int currentScore = 0;
    private int lives = 0;
    private int currentLevel = 1;
    private bool gameWon = false;
    private bool fruitSpawned = false;

    private UIManager uiManager;
    private AudioManager audioManager;

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
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found in scene!");
        }

        uiManager = FindAnyObjectByType<UIManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        
        // Initialize lives and level
        lives = startLives;
        currentLevel = 1;
        Debug.Log($"Game Started - Lives: {lives}, Level: {currentLevel}");
    }

    public void AddPellet()
    {
        pelletsCollected++;
        AddScore(pelletsPointValue);
        Debug.Log($"Pellets: {pelletsCollected}/{pelletsNeeded} | Score: {currentScore}");

        // Spawn fruit when 50% of pellets collected
        if (pelletsCollected == pelletsNeeded / 2 && !fruitSpawned)
        {
            fruitSpawned = true;
            SpawnFruit();
        }

        // Play sound effect
        if (audioManager != null)
            audioManager.PlayPelletPickup();

        if (pelletsCollected >= pelletsNeeded && !gameWon)
        {
            WinGame();
        }
    }

    public void OnPlayerCaughtByGhost()
    {
        lives--;
        Debug.Log($"Hit by ghost! Lives remaining: {lives}");
        
        // Play sound effect
        if (audioManager != null)
            audioManager.PlayGhostCatch();
        
        if (lives <= 0)
        {
            Debug.Log("GAME OVER - No lives left!");
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);

            // Show game over screen
            if (uiManager != null)
            {
                Debug.Log("Calling UIManager.ShowGameOver()");
                uiManager.ShowGameOver();
            }
            else
            {
                Debug.LogError("UIManager not found!");
            }
        }
        else
        {
            // Respawn player at start position
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player != null)
            {
                player.transform.position = new Vector3(0, 0.5f, 0);
                if (player.TryGetComponent<PlayerController>(out var pc))
                    pc.ResetDirection();
            }
        }
    }

    private void WinGame()
    {
        gameWon = true;
        GameManager.Instance.SetGameState(GameManager.GameState.Won);
        Debug.Log("YOU WIN!");

        // Play sound effect
        if (audioManager != null)
            audioManager.PlayWinSound();

        // Show win screen
        if (uiManager != null)
            uiManager.ShowWinScreen();
    }

    public int GetPelletsCollected() => pelletsCollected;
    public int GetPelletsNeeded() => pelletsNeeded;
    public int GetScore() => currentScore;
    public int GetLives() => lives;
    public int GetLevel() => currentLevel;

    public void AddScore(int points)
    {
        currentScore += points;
        if (uiManager != null)
            uiManager.UpdateScore(currentScore);
    }

    public void OnGhostEaten(GameObject ghost)
    {
        Debug.Log($"Ghost {ghost.name} has been eaten! +{ghostPointValue} points");
        AddScore(ghostPointValue);
        
        if (audioManager != null)
            audioManager.PlayGhostCatch();
        
        // Respawn ghost at center (ghost house)
        Vector3 ghostHousePos = new Vector3(0, 0.5f, 0);
        ghost.transform.position = ghostHousePos;
        
        // Re-enable the ghost
        GhostAI ghostAI = ghost.GetComponent<GhostAI>();
        if (ghostAI != null)
        {
            ghostAI.SetVulnerable(false);
        }
    }

    private void SpawnFruit()
    {
        Vector3 fruitPos = new Vector3(
            Random.Range(-6f, 6f),
            0.5f,
            Random.Range(-6f, 6f)
        );
        
        GameObject fruitObj = new GameObject($"Fruit_Bonus");
        fruitObj.transform.position = fruitPos;
        
        // Add FruitBonus component
        FruitBonus fruit = fruitObj.AddComponent<FruitBonus>();
        
        // Add mesh for visualization
        GameObject sphere = new GameObject("Visual");
        sphere.transform.parent = fruitObj.transform;
        sphere.transform.localPosition = Vector3.zero;
        MeshFilter mf = sphere.AddComponent<MeshFilter>();
        MeshRenderer mr = sphere.AddComponent<MeshRenderer>();
        mf.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
        sphere.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        
        // Set material color
        Material fruitMat = new Material(Shader.Find("Standard"));
        fruitMat.color = new Color(1f, 0.2f, 0.2f); // Red fruit
        mr.material = fruitMat;
        
        Debug.Log($"Spawned fruit bonus at {fruitPos}!");
    }
}
