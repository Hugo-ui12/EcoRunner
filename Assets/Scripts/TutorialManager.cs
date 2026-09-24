using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] paginas;
    public GameObject panelComoJugar;

    private int paginaActual = 0;

    void Start()
    {
        MostrarPagina();
    }

    void MostrarPagina()
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            paginas[i].SetActive(i == paginaActual);
        }
    }

    public void Abrir()
    {
        panelComoJugar.SetActive(true);

        paginaActual = 0;
        MostrarPagina();
    }

    public void Siguiente()
    {
        if (paginaActual < paginas.Length - 1)
        {
            paginaActual++;
            MostrarPagina();
        }
    }

    public void Anterior()
    {
        if (paginaActual > 0)
        {
            paginaActual--;
            MostrarPagina();
        }
    }

    public void Volver()
    {
        panelComoJugar.SetActive(false);
    }
}
