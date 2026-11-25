
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConfiguracionAudio : MonoBehaviour
{
    [Header("Música")]
    public AudioSource musica;
    public Button botonSubirMusica;
    public Button botonBajarMusica;
    public TMP_Text textoVolumenMusica;

    [Header("Efectos")]
    public Button botonSubirEfectos;
    public Button botonBajarEfectos;
    public TMP_Text textoVolumenEfectos;

    public float incremento = 0.1f;

    private float volumenMusica;
    private float volumenEfectos;

    void Start()
    {
        volumenMusica = PlayerPrefs.GetFloat("VolumenMusica", 1f);
        volumenEfectos = PlayerPrefs.GetFloat("VolumenEfectos", 1f);

        AplicarMusica();
        ActualizarTextoEfectos();

        botonSubirMusica.onClick.AddListener(SubirMusica);
        botonBajarMusica.onClick.AddListener(BajarMusica);

        botonSubirEfectos.onClick.AddListener(SubirEfectos);
        botonBajarEfectos.onClick.AddListener(BajarEfectos);
    }

    public void SubirMusica()
    {
        volumenMusica = Mathf.Clamp01(volumenMusica + incremento);
        AplicarMusica();
    }

    public void BajarMusica()
    {
        volumenMusica = Mathf.Clamp01(volumenMusica - incremento);
        AplicarMusica();
    }

    public void SubirEfectos()
    {
        volumenEfectos = Mathf.Clamp01(volumenEfectos + incremento);
        GuardarEfectos();
    }

    public void BajarEfectos()
    {
        volumenEfectos = Mathf.Clamp01(volumenEfectos - incremento);
        GuardarEfectos();
    }

    void AplicarMusica()
    {
        if (musica != null)
            musica.volume = volumenMusica;

        PlayerPrefs.SetFloat("VolumenMusica", volumenMusica);
        ActualizarTextoMusica();
    }

    void GuardarEfectos()
    {
        PlayerPrefs.SetFloat("VolumenEfectos", volumenEfectos);
        ActualizarTextoEfectos();
    }

    void ActualizarTextoMusica()
    {
        if (textoVolumenMusica != null)
            textoVolumenMusica.text =
                Mathf.RoundToInt(volumenMusica * 100) + "%";
    }

    void ActualizarTextoEfectos()
    {
        if (textoVolumenEfectos != null)
            textoVolumenEfectos.text =
                Mathf.RoundToInt(volumenEfectos * 100) + "%";
    }
}
