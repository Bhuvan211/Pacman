using UnityEngine;
using UnityEngine.AI;

public class PelletSetup : MonoBehaviour
{
    public static void SetupAllPellets()
    {
        try
        {
            Transform[] allTransforms = FindObjectsByType<Transform>();
            int setupCount = 0;
            
            // First pass: Look for explicit pellet names
            foreach (Transform t in allTransforms)
            {
                if (t == null) continue;
                
                // Check various pellet naming patterns (case-insensitive)
                string nameLower = t.name.ToLower();
                bool isPellet = nameLower.Contains("dot") || 
                               nameLower.Contains("pellet") || 
                               nameLower.Contains("pickup") ||
                               nameLower.Contains("food");
                
                if (isPellet)
                {
                    if (SetupPellet(t.gameObject))
                        setupCount++;
                }
            }
            
            Debug.Log($"✓ Pellets found and configured: {setupCount} pellets");
            
            // Fallback: If very few pellets found, spawn them in a grid pattern
            if (setupCount < 20)
            {
                Debug.LogWarning($"⚠ Only {setupCount} pellets found! Spawning additional pellets in grid pattern.");
                setupCount += SpawnPelletsInGrid();
                Debug.Log($"✓ Total pellets now: {setupCount}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error setting up pellets: {e.Message}");
        }
    }

    private static bool SetupPellet(GameObject pellet)
    {
        if (pellet == null) return false;
        
        // Skip if already setup (has both rigidbody and collider)
        Rigidbody existingRb = pellet.GetComponent<Rigidbody>();
        Collider existingCol = pellet.GetComponent<Collider>();
        if (existingRb != null && existingCol != null && existingCol.isTrigger)
        {
            return true; // Already setup
        }
        
        // Add Rigidbody if missing
        Rigidbody rb = existingRb;
        if (rb == null)
        {
            rb = pellet.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.mass = 0.1f;
        }
        
        // Setup collider as trigger
        Collider col = existingCol;
        if (col == null)
        {
            SphereCollider sphereCol = pellet.AddComponent<SphereCollider>();
            sphereCol.isTrigger = true;
            sphereCol.radius = 0.4f;
        }
        else
        {
            col.isTrigger = true;
        }
        
        return true;
    }

    private static int SpawnPelletsInGrid()
    {
        int spawnedCount = 0;
        int maxPelletsToSpawn = 50; // Limit to 50 pellets
        
        // Create pellets in grid pattern - spawn everywhere for now
        for (int x = -8; x <= 8 && spawnedCount < maxPelletsToSpawn; x += 2)
        {
            for (int z = -8; z <= 8 && spawnedCount < maxPelletsToSpawn; z += 2)
            {
                Vector3 spawnPos = new Vector3(x, 1f, z);
                
                GameObject pellet = new GameObject("Dot_Spawned");
                pellet.transform.position = spawnPos;
                
                // Add rigidbody
                Rigidbody rb = pellet.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.mass = 0.1f;
                rb.constraints = RigidbodyConstraints.FreezePosition;
                
                // Add sphere collider as trigger
                SphereCollider col = pellet.AddComponent<SphereCollider>();
                col.isTrigger = true;
                col.radius = 0.2f;
                
                // Add visual mesh (white sphere)
                GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                visual.transform.parent = pellet.transform;
                visual.transform.localPosition = Vector3.zero;
                visual.transform.localScale = Vector3.one * 0.35f;
                visual.name = "Visual";
                
                // Remove collider from visual
                Collider visualCol = visual.GetComponent<Collider>();
                if (visualCol != null)
                    Object.Destroy(visualCol);
                
                // Set material to bright white
                Renderer renderer = visual.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material mat = new Material(Shader.Find("Standard"));
                    mat.color = Color.white;
                    mat.SetFloat("_Glossiness", 0.8f);
                    renderer.material = mat;
                }
                
                spawnedCount++;
            }
        }
        
        Debug.Log($"✓ Spawned {spawnedCount} pellets in grid pattern with visuals");
        return spawnedCount;
    }

}


