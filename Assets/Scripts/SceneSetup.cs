using UnityEngine;

public class SceneBootstrap : MonoBehaviour
{
    private void Awake()
    {
        // Create GameManager singleton
        if (FindAnyObjectByType<GameManager>() == null)
        {
            GameObject gmObj = new GameObject("GameManager");
            gmObj.AddComponent<GameManager>();
            DontDestroyOnLoad(gmObj);
        }

        // Create GameplayManager
        if (FindAnyObjectByType<GameplayManager>() == null)
        {
            GameObject gpObj = new GameObject("GameplayManager");
            gpObj.AddComponent<GameplayManager>();
        }

        Debug.Log("Bootstrap complete - Game ready!");
    }
}
