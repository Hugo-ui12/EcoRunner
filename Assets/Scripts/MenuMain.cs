using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject instrucciones;
    public GameObject ajustes;

    public void AbrirInstrucciones()
    {
        if (instrucciones != null)
        {
            instrucciones.SetActive(true);
        }
    }

    public void RegresarMenu()
    {
        if (instrucciones != null)
        {
            instrucciones.SetActive(false);
        }

        if (ajustes != null)
        {
            ajustes.SetActive(false);
        }
    }

    public void AbrirAjustes()
    {
        if (ajustes != null)
        {
            ajustes.SetActive(true);
        }
    }

    public void Jugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }
}