
using UnityEngine;
using UnityEngine.UI;

public class SelectorMusica : MonoBehaviour
{
    [Header("Casilla de selección")]
    public Toggle toggleCancion1;

    private const string CLAVE_MUSICA = "CancionSeleccionada";

    void Start()
    {
        // Recuperar la canción guardada
        int cancionGuardada = PlayerPrefs.GetInt(CLAVE_MUSICA, 0);

        // Por ahora solo tenemos una canción
        toggleCancion1.isOn = (cancionGuardada == 0);

        // Escuchar los cambios de selección
        toggleCancion1.onValueChanged.AddListener(SeleccionarCancion1);
    }

    public void SeleccionarCancion1(bool seleccionada)
    {
        if (seleccionada)
        {
            PlayerPrefs.SetInt(CLAVE_MUSICA, 0);
            PlayerPrefs.Save();

            Debug.Log("Canción 1 seleccionada");
        }
    }
}

