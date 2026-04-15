using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform[] ghostSpawns;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ghostPrefab;

    private GameObject player;
    private GameObject[] ghosts;
    private GameState currentState = GameState.Playing;

    public enum GameState { Playing, Paused, GameOver, Won }

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
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Find existing player
        player = FindAnyObjectByType<PlayerController>()?.gameObject;
        if (player == null)
        {
            player = GameObject.Find("Chomp");
        }

        // Find existing ghosts
        GhostAI[] ghostAIs = FindObjectsByType<GhostAI>();
        ghosts = new GameObject[ghostAIs.Length];
        for (int i = 0; i < ghostAIs.Length; i++)
        {
            ghosts[i] = ghostAIs[i].gameObject;
        }

        Debug.Log($"✓ Found {ghosts.Length} ghosts in scene");
        currentState = GameState.Playing;
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;
        Time.timeScale = (newState == GameState.Paused) ? 0 : 1;
    }

    public GameState GetGameState() => currentState;

    public GameObject GetPlayer() => player;
    public GameObject[] GetGhosts() => ghosts;
}
