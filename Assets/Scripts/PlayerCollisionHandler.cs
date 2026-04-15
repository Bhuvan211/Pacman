using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private GameplayManager gameplayManager;
    private bool canCollectPellets = true;

    private void Start()
    {
        gameplayManager = FindAnyObjectByType<GameplayManager>();
        if (gameplayManager == null)
        {
            Debug.LogError("GameplayManager not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Find GameplayManager if not already found
        if (gameplayManager == null)
        {
            gameplayManager = FindAnyObjectByType<GameplayManager>();
            if (gameplayManager == null)
            {
                Debug.LogError("GameplayManager not found - cannot process collision!");
                return;
            }
        }

        // Ignore collisions with non-game objects
        if (other.name.Contains("Stage") || other.name.Contains("Manager") || other.name == "Chomp")
        {
            return;
        }

        Debug.Log($"Player collision with: {other.name}");

        // Detect pellets by name pattern - ONLY accept "Dot" prefixed pellets
        if (other.name.Contains("Dot") && canCollectPellets)
        {
            Debug.Log($"Collected pellet: {other.name}");
            gameplayManager.AddPellet();
            Destroy(other.gameObject);
            return;
        }

        // Detect ghosts by name pattern
        if (other.name.Contains("Ghost"))
        {
            // If player has power, eat the ghost!
            if (PowerPelletManager.PlayerHasPower)
            {
                Debug.Log($"EATING GHOST: {other.name}!");
                gameplayManager.OnGhostEaten(other.gameObject);
                return;
            }
            else
            {
                Debug.Log($"HIT BY GHOST: {other.name}! GAME OVER!");
                gameplayManager.OnPlayerCaughtByGhost();
                canCollectPellets = false;
                return;
            }
        }

        // Detect power-ups by name pattern
        if (other.name.Contains("PowerUp"))
        {
            Debug.Log($"Collected power-up: {other.name}");
            PowerPelletManager ppm = FindAnyObjectByType<PowerPelletManager>();
            if (ppm != null)
            {
                ppm.ActivatePowerUp();
            }
            Destroy(other.gameObject);
            return;
        }

        // Detect fruit bonuses
        FruitBonus fruit = other.GetComponent<FruitBonus>();
        if (fruit != null)
        {
            Debug.Log($"Collected fruit! +{fruit.GetPointValue()} points");
            gameplayManager.AddScore(fruit.GetPointValue());
            Destroy(other.gameObject);
            return;
        }
    }

    public void ResetCollisionState()
    {
        canCollectPellets = true;
    }
}
