using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 1.5f;
    public float puntoIzquierdo = -3f;
    public float puntoDerecho = 3f;

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

        animator.applyRootMotion = false;
        animator.SetBool("Walk", true);

        // Flip inicial correcto
        if (moviendoDerecha && transform.localScale.x > 0)
            Flip();
    }

    void FixedUpdate()
    {
        if (estaMuerto || animator == null) return;

        Patrullar();
    }

    void Patrullar()
    {
        float direccion = moviendoDerecha ? 1f : -1f;
        Vector3 nuevaPos = transform.position;
        nuevaPos.x += direccion * velocidad * Time.fixedDeltaTime;

        // Invertido: flipar ANTES de cambiar dirección
        if (moviendoDerecha && nuevaPos.x >= puntoDerecho)
        {
            Flip();  // Primero flip
            moviendoDerecha = false;
            nuevaPos.x = puntoDerecho;
        }
        else if (!moviendoDerecha && nuevaPos.x <= puntoIzquierdo)
        {
            Flip();  // Primero flip
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

    public void RecibirGolpe()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        animator.SetTrigger("Death");
    }

    public void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}



