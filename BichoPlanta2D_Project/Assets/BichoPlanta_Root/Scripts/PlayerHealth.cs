using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int vidasMaximas = 3;
    private int vidasActuales;

    [Header("UI Vida")]
    public Image barraVida;

    [Header("PickUps")]
    public int monedas = 0;

    [Header("UI Monedas")]
    public Text textoMonedas;

    void Start()
    {
        vidasActuales = vidasMaximas;
        ActualizarBarra();
        ActualizarTextoMonedas();
    }

    // Método público para recibir daño
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
        // Aquí puedes hacer respawn, reiniciar nivel o reproducir animación
        // Destroy(gameObject);
    }

    // Método para recoger monedas
    public void RecogerMoneda()
    {
        monedas++;
        ActualizarTextoMonedas();
    }
}
