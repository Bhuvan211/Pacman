using UnityEngine;

public class PowerPelletManager : MonoBehaviour
{
    [SerializeField] private float powerDuration = 10f;
    [SerializeField] private int numPowerUps = 3;
    
    private bool playerHasPower = false;
    private float powerTimeRemaining = 0;
    private PlayerController playerController;
    private GhostAI[] ghosts;
    private GameObject[] powerUpObjects;

    public static bool PlayerHasPower { get; private set; } = false;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        ghosts = FindObjectsByType<GhostAI>();
        
        SpawnPowerUps();
    }

    private void Update()
    {
        if (playerHasPower)
        {
            powerTimeRemaining -= Time.deltaTime;
            
            // Flashing effect when power is about to expire
            if (powerTimeRemaining < 3f && powerTimeRemaining > 0)
            {
                // Flash ghosts
                float flashSpeed = 5f;
                bool isFlashing = (Time.time * flashSpeed) % 1f > 0.5f;
                
                foreach (GhostAI ghost in ghosts)
                {
                    if (ghost != null)
                    {
                        Renderer renderer = ghost.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            if (isFlashing)
                                renderer.material.color = new Color(0.3f, 0.3f, 1f);
                            else
                                renderer.material.color = new Color(1f, 1f, 1f); // Flicker white
                        }
                    }
                }
            }
            
            if (powerTimeRemaining <= 0)
            {
                DeactivatePower();
            }
        }
    }

    private void SpawnPowerUps()
    {
        powerUpObjects = new GameObject[numPowerUps];
        
        for (int i = 0; i < numPowerUps; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(-8f, 8f),
                0.5f,
                Random.Range(-8f, 8f)
            );
            
            GameObject powerUp = new GameObject($"PowerUp_{i}");
            powerUp.transform.position = spawnPos;
            
            // Add visual sphere
            GameObject sphere = new GameObject("Visual");
            sphere.transform.parent = powerUp.transform;
            sphere.transform.localPosition = Vector3.zero;
            MeshFilter mf = sphere.AddComponent<MeshFilter>();
            MeshRenderer mr = sphere.AddComponent<MeshRenderer>();
            mf.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
            
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(1f, 0.84f, 0f); // Gold color
            mr.material = mat;
            sphere.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            
            // Add collider
            SphereCollider col = powerUp.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.5f;
            
            // Add rigidbody
            Rigidbody rb = powerUp.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            
            powerUpObjects[i] = powerUp;
            Debug.Log($"Spawned power-up at {spawnPos}");
        }
    }

    public void ActivatePowerUp()
    {
        playerHasPower = true;
        powerTimeRemaining = powerDuration;
        PlayerHasPower = true;
        
        foreach (GhostAI ghost in ghosts)
        {
            if (ghost != null)
                ghost.SetVulnerable(true);
        }
        
        Debug.Log($"POWER UP ACTIVATED! Ghosts are now vulnerable for {powerDuration}s!");
    }

    private void DeactivatePower()
    {
        playerHasPower = false;
        PlayerHasPower = false;
        
        foreach (GhostAI ghost in ghosts)
        {
            if (ghost != null)
                ghost.SetVulnerable(false);
        }
        
        Debug.Log("Power-up expired! Ghosts are back to normal.");
    }
}
