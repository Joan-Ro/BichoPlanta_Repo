using UnityEngine;

public class EnemyPatroLvl3 : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 1.5f;
    public float puntoIzquierdo = -3f;
    public float puntoDerecho = 3f;

    [Header("Vida")]
    public int vidaMaxima = 1;

    private int vidaActual;
    private Animator animator;
    private bool moviendoDerecha = true;
    private bool estaMuerto = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("¡No hay Animator en el enemigo!");
            return;
        }

        vidaActual = vidaMaxima;

        animator.applyRootMotion = false;
        animator.SetBool("Walk", true);

        if (moviendoDerecha && transform.localScale.x > 0)
            Flip();
    }

    void FixedUpdate()
    {
        if (estaMuerto) return;

        Patrullar();
    }

    void Patrullar()
    {
        float direccion = moviendoDerecha ? 1f : -1f;
        Vector3 nuevaPos = transform.position;
        nuevaPos.x += direccion * velocidad * Time.fixedDeltaTime;

        if (moviendoDerecha && nuevaPos.x >= puntoDerecho)
        {
            Flip();
            moviendoDerecha = false;
            nuevaPos.x = puntoDerecho;
        }
        else if (!moviendoDerecha && nuevaPos.x <= puntoIzquierdo)
        {
            Flip();
            moviendoDerecha = true;
            nuevaPos.x = puntoIzquierdo;
        }

        transform.position = nuevaPos;
    }

    void Flip()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    public void RecibirDanio(int danio)
    {
        if (estaMuerto) return;

        vidaActual -= danio;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaMuerto = true;

        animator.SetBool("Walk", false);
        animator.SetTrigger("Death");

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    // Llamar desde Animation Event
    public void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}



