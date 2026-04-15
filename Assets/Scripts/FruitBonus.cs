using UnityEngine;

public class FruitBonus : MonoBehaviour
{
    public enum FruitType { Cherry, Strawberry, Peach, Pineapple, Apple, Melon, Banana, Galaxian }
    
    [SerializeField] private FruitType fruitType = FruitType.Cherry;
    [SerializeField] private int pointValue = 100;
    [SerializeField] private float lifetime = 9f; // Fruits disappear after 9 seconds
    
    private float spawnTime;
    private Renderer fruitRenderer;

    private void Start()
    {
        spawnTime = Time.time;
        fruitRenderer = GetComponent<Renderer>();
        
        // Set fruit color based on type
        if (fruitRenderer != null)
        {
            fruitRenderer.material.color = GetFruitColor();
        }
        
        // Add collider if missing
        if (GetComponent<SphereCollider>() == null)
        {
            SphereCollider col = gameObject.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.4f;
        }
        
        // Add rigidbody if missing
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        
        Debug.Log($"Spawned {fruitType} fruit worth {pointValue} points");
    }

    private void Update()
    {
        // Remove fruit after lifetime expires
        if (Time.time - spawnTime > lifetime)
        {
            Debug.Log($"{fruitType} fruit disappeared");
            Destroy(gameObject);
        }
    }

    private Color GetFruitColor()
    {
        return fruitType switch
        {
            FruitType.Cherry => new Color(1f, 0f, 0f),           // Red
            FruitType.Strawberry => new Color(1f, 0.2f, 0.2f),   // Light red
            FruitType.Peach => new Color(1f, 0.7f, 0.5f),        // Peach
            FruitType.Pineapple => new Color(1f, 1f, 0f),        // Yellow
            FruitType.Apple => new Color(0f, 1f, 0f),            // Green
            FruitType.Melon => new Color(0f, 1f, 0.5f),          // Cyan-green
            FruitType.Banana => new Color(1f, 1f, 0.5f),         // Light yellow
            FruitType.Galaxian => new Color(1f, 0f, 1f),         // Magenta
            _ => Color.white
        };
    }

    public int GetPointValue()
    {
        return pointValue;
    }
}
