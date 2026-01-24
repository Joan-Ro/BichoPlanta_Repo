    using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int vidasMaximas = 3;
    private int vidasActuales;

    [Header("UI Vida")]
    public Image barraVida;

    [Header("PickUp")]
    public int monedas = 0;

    [Header("UI Monedas")]
    public Text textoMonedas;

    void Start()
    {
        vidasActuales = vidasMaximas;
        ActualizarBarra();
        ActualizarTextoMonedas();
    }

    // M�todo p�blico para recibir da�o
    public void RecibirDanio(int danio)
    {
        vidasActuales -= danio;
        vidasActuales = Mathf.Clamp(vidasActuales, 0, vidasMaximas);
        ActualizarBarra();

        if (vidasActuales <= 0)
        {
            Morir();
        }
    }

    void ActualizarBarra()
    {
        if (barraVida != null)
            barraVida.fillAmount = (float)vidasActuales / vidasMaximas;
    }

    void ActualizarTextoMonedas()
    {
        if (textoMonedas != null)
            textoMonedas.text = monedas.ToString();
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");
        // Aqu� puedes hacer respawn, reiniciar nivel o reproducir animaci�n
        // Destroy(gameObject);
    }

    // M�todo para recoger monedas
    public void RecogerMoneda()
    {
        monedas++;
        ActualizarTextoMonedas();
    }
}
