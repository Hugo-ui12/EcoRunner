using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.instance.PlayGameOver();

            Invoke(nameof(RestartGame), 0.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Recycle"))
        {
            ScoreManager.score += 10;

            AudioManager.instance.PlayCollect();

            Debug.Log("Puntos: " + ScoreManager.score);

            Destroy(other.gameObject);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}

