using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public TextMeshProUGUI scoreText;

    private int highScore = 0;

    private PlayerMovement player;

    void Start()
    {
        score = 0;

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        player = FindFirstObjectByType<PlayerMovement>();

        ActualizarMarcador();
    }

    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        ActualizarMarcador();
    }

    void ActualizarMarcador()
    {
        if (player != null)
        {
            scoreText.text =
       "★ Puntos: " + score +
       "     ★ Record: " + highScore +
       "     ★ Velocidad: " + player.speed;
        }
    }
}