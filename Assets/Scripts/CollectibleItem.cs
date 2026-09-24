using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar puntos
            ScoreManager.score += points;

            // Reproducir sonido
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayCollect();
            }

            // Destruir reciclable
            Destroy(gameObject);
        }
    }
}