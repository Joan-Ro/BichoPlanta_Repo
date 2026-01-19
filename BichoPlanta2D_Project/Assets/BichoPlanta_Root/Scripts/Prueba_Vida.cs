using UnityEngine;
using UnityEngine.UI;

public class Prueba_Vida : MonoBehaviour
{
    [Header("Vida")]
    public int vidasMaximas = 3;
    private int vidasActuales;

    [Header("UI")]
    public Image barraVida; // Arrastrar Life_bar (imagen roja)

    void Start()
    {
        vidasActuales = vidasMaximas;
        ActualizarBarra();
    }

    void Update()
    {
        // PRUEBA: quitar vida con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecibirDanio(1);
        }
    }

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
        {
            barraVida.fillAmount = (float)vidasActuales / vidasMaximas;
        }
    }

    void Morir()
    {
        Debug.Log("Jugador muerto");
        // Destroy(gameObject);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


