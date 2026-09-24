using UnityEngine;

public class AjustesManager : MonoBehaviour
{
    public GameObject panelAjustes;

    public void AbrirAjustes()
    {
        panelAjustes.SetActive(true);
    }

    public void Volver()
    {
        panelAjustes.SetActive(false);
    }
}