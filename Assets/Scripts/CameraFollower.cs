using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0, 16, 10);
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private bool lookAtPlayer = true;

    private void Start()
    {
        // Find the player
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerTransform = playerController.transform;
            Debug.Log("✓ CameraFollower found player: " + playerTransform.name);
        }
        else
        {
            Debug.LogWarning("⚠ CameraFollower: Player not found!");
        }
    }

    private void LateUpdate()
    {
        if (playerTransform == null)
            return;

        // Calculate desired camera position (player position + offset)
        Vector3 desiredPosition = playerTransform.position + offset;

        // Smoothly move camera to desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Look at player if enabled
        if (lookAtPlayer)
        {
            transform.LookAt(playerTransform.position + Vector3.up * 0.5f);
        }
    }
}
