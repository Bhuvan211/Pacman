using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Start()
    {
        // Verify managers exist in scene (they should be added via editor setup)
        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm == null)
        {
            Debug.LogWarning("GameManager not found! Use 'Pac-Man/Complete Setup' to setup scenes.");
            return;
        }

        GameplayManager gp = FindAnyObjectByType<GameplayManager>();
        if (gp == null)
        {
            Debug.LogWarning("GameplayManager not found! Use 'Pac-Man/Complete Setup' to setup scenes.");
            return;
        }

        // Add AudioListener if missing (needed for audio to work)
        if (FindAnyObjectByType<AudioListener>() == null)
        {
            gameObject.AddComponent<AudioListener>();
            Debug.Log("✓ Added AudioListener to GameInitializer");
        }

        // Only setup if characters don't already exist
        if (FindAnyObjectByType<PlayerController>() == null)
        {
            SetupPlayer();
        }

        if (FindObjectsByType<GhostAI>().Length == 0)
        {
            SetupGhosts();
        }

        // Pellets are now spawned by MazeGenerator, no need to setup
        Debug.Log("✓ Pellets spawned by procedural maze generator");

        Debug.Log("✓ Game scene initialized!");
    }

    private void SetupPlayer()
    {
        // Try to find existing Chomp in scene
        Transform chompTransform = FindChildByName(null, "Chomp");
        if (chompTransform == null)
        {
            // Create a simple placeholder
            GameObject player = new GameObject("Chomp");
            player.transform.position = Vector3.zero;
            chompTransform = player.transform;
        }

        GameObject chomp = chompTransform.gameObject;
        
        // Add PlayerController if missing
        if (chomp.GetComponent<PlayerController>() == null)
            chomp.AddComponent<PlayerController>();

        // Add PlayerCollisionHandler if missing
        if (chomp.GetComponent<PlayerCollisionHandler>() == null)
            chomp.AddComponent<PlayerCollisionHandler>();

        // Add Rigidbody if missing
        if (chomp.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = chomp.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            rb.mass = 1f;
            rb.linearDamping = 0.5f;
        }

        // Add Collider if missing (as trigger for pellet detection)
        if (chomp.GetComponent<Collider>() == null)
        {
            CapsuleCollider col = chomp.AddComponent<CapsuleCollider>();
            col.radius = 0.4f;
            col.height = 1f;
            col.isTrigger = true;  // Must be trigger to detect pellets
        }
        else
        {
            // Ensure existing collider is set as trigger
            Collider col = chomp.GetComponent<Collider>();
            if (col is CapsuleCollider capsule)
            {
                capsule.isTrigger = true;
            }
        }

        Debug.Log("✓ Player setup complete");
    }

    private void SetupGhosts()
    {
        // Find all transforms with "Ghost" in their name
        Transform[] allTransforms = FindObjectsByType<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.name.Contains("Ghost"))
            {
                GameObject ghost = t.gameObject;

                // Add GhostAI if missing
                if (ghost.GetComponent<GhostAI>() == null)
                    ghost.AddComponent<GhostAI>();

                // Add Rigidbody if missing
                if (ghost.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = ghost.AddComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
                    rb.mass = 1f;
                    rb.linearDamping = 0.5f;
                }

                // Add Collider if missing
                if (ghost.GetComponent<Collider>() == null)
                {
                    CapsuleCollider col = ghost.AddComponent<CapsuleCollider>();
                    col.radius = 0.4f;
                    col.height = 1f;
                }
            }
        }

        Debug.Log("✓ Ghosts setup complete");
    }

    private Transform FindChildByName(Transform parent, string name)
    {
        Transform[] allTransforms = FindObjectsByType<Transform>();
        foreach (Transform t in allTransforms)
        {
            if (t.name == name)
                return t;
        }
        return null;
    }
}
