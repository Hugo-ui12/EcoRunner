
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

        float volumenGuardado = PlayerPrefs.GetFloat("VolumenEfectos", 1f);

        if (audioSource != null)
        {
            audioSource.volume = volumenGuardado;
        }
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

    public void CambiarVolumenEfectos(float volumen)
    {
        if (audioSource != null)
        {
            audioSource.volume = volumen;
        }
    }
}
