using UnityEngine;
using UnityEngine.UI;

public class Prueba_Vida : MonoBehaviour
{
    [Header("Vida")]
    public int vidasMaximas = 3;
    private int vidasActuales;

    [Header("UI Vida")]
    public Image barraVida;

    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Header("PickUps")]
    public int monedas = 0;

    [Header("UI Monedas")]
    public Text textoMonedas;

    private Rigidbody2D rb;

    void Start()
    {
        vidasActuales = vidasMaximas;
        rb = GetComponent<Rigidbody2D>();

        ActualizarBarra();
        ActualizarTextoMonedas();
    }

    void Update()
    {
        // PRUEBA: quitar vida con espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecibirDanio(1);
        }

        Movimiento();
        Salto();
    }

    void Movimiento()
    {
        float movimiento = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);
    }

    void Salto()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUP"))
        {
            monedas++;
            ActualizarTextoMonedas();
            Destroy(other.gameObject);
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

    void ActualizarTextoMonedas()
{
    if (textoMonedas != null)
    {
        textoMonedas.text = monedas.ToString();
    }
}

    void Morir()
    {
        Debug.Log("Jugador muerto");
        // Destroy(gameObject);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
