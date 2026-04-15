using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private int mazeWidth = 15;
    [SerializeField] private int mazeHeight = 15;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private Material wallMaterial;
    
    private bool[,] maze;
    private GameObject mazeContainer;
    private System.Collections.Generic.List<Vector3> pathPositions = new System.Collections.Generic.List<Vector3>();

    public void GenerateMaze()
    {
        Debug.Log("Generating new maze...");
        
        // Destroy old maze if exists
        Transform oldMaze = transform.Find("GeneratedMaze");
        if (oldMaze != null)
        {
            Destroy(oldMaze.gameObject);
        }
        
        // Clear path positions
        pathPositions.Clear();
        
        // Create new maze container
        mazeContainer = new GameObject("GeneratedMaze");
        mazeContainer.transform.parent = transform;
        
        // Initialize maze array
        maze = new bool[mazeWidth, mazeHeight];
        
        // Set all cells as walls initially
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                maze[x, z] = true; // true = wall
            }
        }
        
        // Carve paths using recursive backtracking
        CarvePathsRecursive(1, 1);
        
        // Create visual representation
        CreateMazeVisuals();
        
        // Spawn pellets in paths
        SpawnPelletsInPaths();
        
        Debug.Log($"Maze generated: {mazeWidth}x{mazeHeight} with {pathPositions.Count} valid positions");
    }

    private void CarvePathsRecursive(int x, int z)
    {
        maze[x, z] = false; // Mark as path (not wall)
        
        // Directions: Up, Right, Down, Left
        int[] dx = { 0, 2, 0, -2 };
        int[] dz = { -2, 0, 2, 0 };
        
        // Shuffle directions for randomness
        int[] order = { 0, 1, 2, 3 };
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(i, 4);
            int temp = order[i];
            order[i] = order[randomIndex];
            order[randomIndex] = temp;
        }
        
        // Try each direction
        for (int i = 0; i < 4; i++)
        {
            int dir = order[i];
            int nx = x + dx[dir];
            int nz = z + dz[dir];
            
            // Check bounds
            if (nx > 0 && nx < mazeWidth - 1 && nz > 0 && nz < mazeHeight - 1 && maze[nx, nz])
            {
                // Carve wall between current and next
                int wallX = x + dx[dir] / 2;
                int wallZ = z + dz[dir] / 2;
                maze[wallX, wallZ] = false;
                
                // Recurse
                CarvePathsRecursive(nx, nz);
            }
        }
    }

    private void CreateMazeVisuals()
    {
        float offsetX = -(mazeWidth * cellSize) / 2f;
        float offsetZ = -(mazeHeight * cellSize) / 2f;
        
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeHeight; z++)
            {
                if (maze[x, z]) // If wall
                {
                    GameObject wall = new GameObject($"Wall_{x}_{z}");
                    wall.transform.parent = mazeContainer.transform;
                    wall.transform.position = new Vector3(
                        offsetX + x * cellSize,
                        0.5f,
                        offsetZ + z * cellSize
                    );
                    
                    // Add cube collider for physics
                    BoxCollider collider = wall.AddComponent<BoxCollider>();
                    collider.size = new Vector3(cellSize, 1f, cellSize);
                    
                    // Add mesh for visibility (optional visual)
                    MeshFilter mf = wall.AddComponent<MeshFilter>();
                    MeshRenderer mr = wall.AddComponent<MeshRenderer>();
                    mf.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
                    
                    Material mat = new Material(Shader.Find("Standard"));
                    mat.color = new Color(0.3f, 0.3f, 0.3f); // Dark gray walls
                    mr.material = mat;
                    
                    wall.transform.localScale = new Vector3(cellSize, 0.5f, cellSize);
                }
                else // Path - store position for pellet spawning
                {
                    Vector3 pathPos = new Vector3(
                        offsetX + x * cellSize,
                        0.5f,
                        offsetZ + z * cellSize
                    );
                    pathPositions.Add(pathPos);
                }
            }
        }
        
        Debug.Log("Maze visuals created");
    }

    private void SpawnPelletsInPaths()
    {
        // Get available path positions
        System.Collections.Generic.List<Vector3> availablePaths = new System.Collections.Generic.List<Vector3>(pathPositions);
        
        // Shuffle available paths
        for (int i = 0; i < availablePaths.Count; i++)
        {
            Vector3 temp = availablePaths[i];
            int randomIndex = Random.Range(i, availablePaths.Count);
            availablePaths[i] = availablePaths[randomIndex];
            availablePaths[randomIndex] = temp;
        }
        
        // Spawn pellets in random path positions
        int pelletsToSpawn = Mathf.Min(50, availablePaths.Count); // Max 50 pellets
        for (int i = 0; i < pelletsToSpawn; i++)
        {
            Vector3 pelletPos = availablePaths[i];
            
            GameObject pellet = new GameObject("Dot_" + i);
            pellet.transform.parent = mazeContainer.transform;
            pellet.transform.position = pelletPos;
            
            // Add rigidbody
            Rigidbody rb = pellet.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.mass = 0.1f;
            
            // Add collider
            SphereCollider col = pellet.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.3f;
            
            // Add visual sphere
            GameObject sphere = new GameObject("Visual");
            sphere.transform.parent = pellet.transform;
            sphere.transform.localPosition = Vector3.zero;
            MeshFilter mf = sphere.AddComponent<MeshFilter>();
            MeshRenderer mr = sphere.AddComponent<MeshRenderer>();
            mf.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
            
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(1f, 0.8f, 0f); // Yellow pellets
            mr.material = mat;
            
            sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        
        Debug.Log($"Spawned {pelletsToSpawn} pellets in maze");
    }

    public System.Collections.Generic.List<Vector3> GetPathPositions()
    {
        return pathPositions;
    }

    private void Start()
    {
        GenerateMaze();
    }
}

