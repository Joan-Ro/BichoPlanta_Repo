using UnityEngine;

public class EnemySerpiente : MonoBehaviour, IDanable
{
    [Header("Movimiento")]
    public float velocidad = 1.5f;
    public float minX;
    public float maxX;

    [Header("Vida")]
    public int vida = 1;

    private bool moviendoDerecha = true;
    private bool muerto = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Walk", true);
    }

    void Update()
    {
        if (muerto) return;

        Mover();
    }

    void Mover()
    {
        float direccion = moviendoDerecha ? 1f : -1f;
        transform.position += Vector3.right * direccion * velocidad * Time.deltaTime;

        if (moviendoDerecha && transform.position.x >= maxX)
        {
            Girar();
            moviendoDerecha = false;
        }
        else if (!moviendoDerecha && transform.position.x <= minX)
        {
            Girar();
            moviendoDerecha = true;
        }
    }

    void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    // 💥 DAÑO
    public void RecibirDanio(int danio)
    {
        if (muerto) return;

        vida -= danio;

        if (vida <= 0)
        {
            Morir();
            Collider2D col = GetComponentInChildren<Collider2D>();
            if (col != null)
                col.enabled = false;

        }
    }

    void Morir()
    {
        muerto = true;
        anim.SetBool("Walk", false);
        anim.SetTrigger("Death");
    }

    // 🎬 Animation Event
    public void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}

