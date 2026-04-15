using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Easy, Normal, Hard }

    [SerializeField] private Difficulty currentDifficulty = Difficulty.Normal;
    
    private GhostAI[] ghosts;

    private void Start()
    {
        ghosts = FindObjectsByType<GhostAI>();
        ApplyDifficulty(currentDifficulty);
    }

    public void ApplyDifficulty(Difficulty difficulty)
    {
        currentDifficulty = difficulty;

        foreach (GhostAI ghost in ghosts)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    // Slower, less aggressive
                    break;
                case Difficulty.Normal:
                    // Default behavior
                    break;
                case Difficulty.Hard:
                    // Faster, more aggressive
                    break;
            }
        }

        Debug.Log($"Difficulty set to: {difficulty}");
    }

    public Difficulty GetCurrentDifficulty() => currentDifficulty;
}
