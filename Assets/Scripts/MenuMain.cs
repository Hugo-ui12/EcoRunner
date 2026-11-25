
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject instrucciones;
    public GameObject ajustes;

    [Header("Elementos que se ocultan en Configuración y Modo de juego")]
    public GameObject logo;
    public GameObject hoja;
    public GameObject botonEscenario;
    public GameObject Marcogrieta;

    public GameObject botonJugar;

    public void AbrirInstrucciones()
    {
        if (instrucciones != null)
            instrucciones.SetActive(true);

        OcultarElementosMenu();
    }

    public void RegresarMenu()
    {
        if (instrucciones != null)
            instrucciones.SetActive(false);

        if (ajustes != null)
            ajustes.SetActive(false);

        MostrarElementosMenu();
    }

    public void AbrirAjustes()
    {
        if (ajustes != null)
            ajustes.SetActive(true);

        OcultarElementosMenu();
    }

    public void OcultarElementosMenu()
    {
        if (logo != null)
            logo.SetActive(false);

        if (hoja != null)
            hoja.SetActive(false);

        if (botonEscenario != null)
            botonEscenario.SetActive(false);

        if (Marcogrieta != null)
            Marcogrieta.SetActive(false);

        if (botonJugar != null)
            botonJugar.SetActive(false);
    }

    public void MostrarElementosMenu()
    {
        if (logo != null)
            logo.SetActive(true);

        if (hoja != null)
            hoja.SetActive(true);

        if (botonEscenario != null)
            botonEscenario.SetActive(true);

        if (Marcogrieta != null)
            Marcogrieta.SetActive(true);

        if (botonJugar != null)
            botonJugar.SetActive(true);
    }

    public void Jugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }
}
