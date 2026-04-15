using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public enum GhostType { Blinky, Pinky, Inky, Clyde }
    
    [SerializeField] private GhostType ghostType = GhostType.Blinky;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float chaseDistance = 8f;
    [SerializeField] private float changeDirectionTime = 1.5f;
    [SerializeField] private Rigidbody rb;

    private Vector3 moveDirection = Vector3.forward;
    private float directionChangeTimer = 0;
    private Transform playerTransform;
    private bool isVulnerable = false;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("GhostAI: Rigidbody not found on " + gameObject.name);
            enabled = false;
            return;
        }

        // Get player reference
        if (GameManager.Instance != null)
        {
            GameObject player = GameManager.Instance.GetPlayer();
            if (player != null)
                playerTransform = player.transform;
        }

        // Set initial movement direction
        SetRandomDirection();
        directionChangeTimer = changeDirectionTime;
        
        Debug.Log($"Ghost '{gameObject.name}' initialized with direction: {moveDirection}");
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.GetGameState() != GameManager.GameState.Playing)
            return;

        UpdateAI();
    }

    private void LateUpdate()
    {
        // Tunnel wrap-around for ghosts
        if (transform.position.x > 10f)
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
        else if (transform.position.x < -10f)
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        
        if (transform.position.z > 10f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        else if (transform.position.z < -10f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
    }

    private void UpdateAI()
    {
        // If vulnerable (power-up active), flee from player!
        if (isVulnerable && playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < 20f) // Flee if player is nearby
            {
                // Move away from player (opposite direction)
                moveDirection = (transform.position - playerTransform.position).normalized;
                rb.linearVelocity = moveDirection * moveSpeed;
                
                // Rotate to face
                if (moveDirection.magnitude > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
                }
                return;
            }
        }

        // Chase player if in range (only when NOT vulnerable)
        if (playerTransform != null && !isVulnerable)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < chaseDistance)
            {
                // Different behavior based on ghost type
                switch (ghostType)
                {
                    case GhostType.Blinky: // Aggressive direct chase
                        moveDirection = (playerTransform.position - transform.position).normalized;
                        break;
                    
                    case GhostType.Pinky: // Ambush - go ahead of player
                        Vector3 playerDir = (playerTransform.position - transform.position).normalized;
                        moveDirection = playerDir + playerDir * 2f; // Aim 2 units ahead
                        break;
                    
                    case GhostType.Inky: // Unpredictable - chase with random offset
                        moveDirection = (playerTransform.position - transform.position).normalized;
                        moveDirection += new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
                        break;
                    
                    case GhostType.Clyde: // Random behavior sometimes chases
                        if (Random.value > 0.5f)
                            moveDirection = (playerTransform.position - transform.position).normalized;
                        else
                            SetRandomDirection();
                        break;
                }
                
                rb.linearVelocity = moveDirection.normalized * moveSpeed;
                
                // Rotate to face
                if (moveDirection.magnitude > 0)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
                }
                return;
            }
        }

        // Random patrol when not chasing
        directionChangeTimer -= Time.fixedDeltaTime;
        if (directionChangeTimer <= 0)
        {
            SetRandomDirection();
            directionChangeTimer = changeDirectionTime;
        }

        // Always move in current direction
        if (moveDirection.magnitude > 0)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }

        // Rotate to face movement direction
        if (moveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
        }
    }

    private void SetRandomDirection(int attempts = 0)
    {
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        // Pick a random direction
        moveDirection = directions[Random.Range(0, directions.Length)];
        Debug.Log($"Ghost {gameObject.name} picked direction: {moveDirection}");
    }

    private bool CanMove(Vector3 direction)
    {
        // Simplified: always return true, let physics handle collisions
        return true;
    }

    public void SetVulnerable(bool vulnerable)
    {
        isVulnerable = vulnerable;
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;
        
        if (vulnerable)
        {
            // Change to blue when vulnerable
            renderer.material.color = new Color(0.3f, 0.3f, 1f);
            Debug.Log($"Ghost {gameObject.name} ({ghostType}) is now VULNERABLE!");
        }
        else
        {
            // Restore ghost color based on type
            Color ghostColor = ghostType switch
            {
                GhostType.Blinky => new Color(1f, 0f, 0f),      // Red
                GhostType.Pinky => new Color(1f, 0.5f, 1f),     // Pink
                GhostType.Inky => new Color(0f, 1f, 1f),        // Cyan
                GhostType.Clyde => new Color(1f, 0.8f, 0f),     // Orange
                _ => Color.white
            };
            renderer.material.color = ghostColor;
            Debug.Log($"Ghost {gameObject.name} ({ghostType}) is back to normal.");
        }
    }

    public bool IsVulnerable()
    {
        return isVulnerable;
    }
}
