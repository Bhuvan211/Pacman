using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip pelletPickupSound;
    [SerializeField] private AudioClip ghostCatchSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private float soundVolume = 0.7f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayPelletPickup()
    {
        PlaySound(pelletPickupSound);
    }

    public void PlayGhostCatch()
    {
        PlaySound(ghostCatchSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip, soundVolume);
        }
    }
}
