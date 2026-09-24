using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip gameOverSound;

    void Awake()
    {
        instance = this;
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayCollect()
    {
        audioSource.PlayOneShot(collectSound);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}