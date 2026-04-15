using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float collisionCheckDistance = 1f;

    private Vector3 currentDirection = Vector3.zero;
    private Vector3 inputDirection = Vector3.zero;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerController: Rigidbody not found on " + gameObject.name);
            enabled = false;
            return;
        }
        
        // Setup rigidbody for smooth movement
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        rb.mass = 1f;
        rb.linearDamping = 5f; // Higher drag stops sliding
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        // Stop moving if game is not playing
        if (GameManager.Instance != null && GameManager.Instance.GetGameState() != GameManager.GameState.Playing)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            inputDirection = Vector3.zero;
            return;
        }

        Move();
    }

    private void HandleInput()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        if (inputX != 0 || inputZ != 0)
        {
            inputDirection = new Vector3(inputX, 0, inputZ).normalized;
        }
        else
        {
            inputDirection = Vector3.zero;
        }
    }

    private void Move()
    {
        // If player is pressing a direction, try to move there
        if (inputDirection.magnitude > 0)
        {
            // Check if we can move in the input direction
            if (CanMoveInDirection(inputDirection))
            {
                currentDirection = inputDirection;
            }
            // Otherwise keep current direction if still valid
            else if (!CanMoveInDirection(currentDirection))
            {
                currentDirection = Vector3.zero;
            }
        }
        else
        {
            // No input - stop moving
            currentDirection = Vector3.zero;
        }

        // Apply velocity
        if (currentDirection.magnitude > 0)
        {
            rb.linearVelocity = new Vector3(currentDirection.x * moveSpeed, rb.linearVelocity.y, currentDirection.z * moveSpeed);
            
            // Rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(currentDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }
        else
        {
            // Stop immediately when no input/blocked
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    private bool CanMoveInDirection(Vector3 direction)
    {
        if (direction.magnitude == 0)
            return false;

        Vector3 rayStart = transform.position + Vector3.up * 0.5f;
        
        // Single raycast ahead - check for solid (non-trigger) colliders
        Ray rayAhead = new Ray(rayStart, direction);
        RaycastHit[] hits = Physics.RaycastAll(rayAhead, collisionCheckDistance);
        
        foreach (RaycastHit hit in hits)
        {
            // Ignore trigger colliders (pellets, etc)
            if (!hit.collider.isTrigger)
            {
                return false;  // Hit a solid wall
            }
        }

        return true;
    }

    public Vector3 GetMoveDirection() => currentDirection;
    public float GetSpeed() => moveSpeed;

    public void ResetDirection()
    {
        currentDirection = Vector3.zero;
        inputDirection = Vector3.zero;
        if (rb != null)
            rb.linearVelocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        // Tunnel wrap-around
        if (transform.position.x > 10f)
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
        else if (transform.position.x < -10f)
            transform.position = new Vector3(10f, transform.position.y, transform.position.z);
        
        if (transform.position.z > 10f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        else if (transform.position.z < -10f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 10f);
    }
}
