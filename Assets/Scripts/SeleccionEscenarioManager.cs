
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeleccionEscenarioManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelSeleccion;
    public GameObject menuPrincipal;

    public MenuPrincipal controladorMenu;

    [Header("Botones de escenarios")]
    public Button botonCiudad;
    public Button botonBosque;

    [Header("Imágenes de escenarios")]
    public Image imagenCiudad;
    public Image imagenBosque;

    [Header("Color de selección")]
    public Color colorNormal = Color.white;
    public Color colorSeleccionado = new Color(1f, 0.45f, 0f, 1f);

    private string escenaSeleccionada = "MainGame";

    private void Start()
    {
        SeleccionarCiudad();
    }

    public void SeleccionarCiudad()
    {
        escenaSeleccionada = "MainGame";
        ActualizarSeleccion();
    }

    public void SeleccionarBosque()
    {
        escenaSeleccionada = "ForestGame";
        ActualizarSeleccion();
    }

    private void ActualizarSeleccion()
    {
        if (imagenCiudad != null)
            imagenCiudad.color = escenaSeleccionada == "MainGame"
                ? colorSeleccionado : colorNormal;

        if (imagenBosque != null)
            imagenBosque.color = escenaSeleccionada == "ForestGame"
                ? colorSeleccionado : colorNormal;
    }

    public void AbrirSeleccion()
    {
        if (controladorMenu != null)
            controladorMenu.OcultarElementosMenu();

        if (menuPrincipal != null)
            menuPrincipal.SetActive(false);

        if (panelSeleccion != null)
            panelSeleccion.SetActive(true);
    }

    public void VolverAlMenu()
    {
        if (panelSeleccion != null)
            panelSeleccion.SetActive(false);

        if (menuPrincipal != null)
            menuPrincipal.SetActive(true);

        if (controladorMenu != null)
            controladorMenu.MostrarElementosMenu();
    }

    public void JugarEscenarioSeleccionado()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(escenaSeleccionada);
    }
}
