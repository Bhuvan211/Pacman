using UnityEngine;

public class DebugStatus : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("=== GAME DEBUG INFO ===");
        Debug.Log($"GameManager exists: {FindAnyObjectByType<GameManager>() != null}");
        Debug.Log($"GameplayManager exists: {FindAnyObjectByType<GameplayManager>() != null}");
        Debug.Log($"Maze exists: {FindAnyObjectByType<Mesh>() != null}");
        
        GameObject chomp = GameObject.Find("Chomp");
        Debug.Log($"Chomp found: {chomp != null}");
        if (chomp != null)
        {
            Debug.Log($"  - Has PlayerController: {chomp.GetComponent<PlayerController>() != null}");
            Debug.Log($"  - Has Rigidbody: {chomp.GetComponent<Rigidbody>() != null}");
            Debug.Log($"  - Has Collider: {chomp.GetComponent<Collider>() != null}");
        }

        // Check for ghosts
        Transform[] allTransforms = FindObjectsByType<Transform>();
        int ghostCount = 0;
        foreach (Transform t in allTransforms)
        {
            if (t.name.Contains("Ghost"))
            {
                ghostCount++;
                Debug.Log($"Ghost found: {t.name}");
                Debug.Log($"  - Has GhostAI: {t.GetComponent<GhostAI>() != null}");
            }
        }
        Debug.Log($"Total ghosts found: {ghostCount}");

        Debug.Log("======================");
    }
}
