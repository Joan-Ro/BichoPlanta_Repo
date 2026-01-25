using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Daño al jugador")]
    public int danio = 1;

    [Header("Vida")]
    public int vida = 1;

    [Header("Animaciones")]
    public Animator animator;

    protected bool estaMuerto = false;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // 💥 Daño al jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaMuerto) return;

        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.RecibirDanio(danio);
        }
    }

    // 🐸 Daño recibido (lengua)
    public virtual void RecibirDanio(int danioRecibido)
    {
        if (estaMuerto) return;

        vida -= danioRecibido;

        if (vida <= 0)
        {
            Morir();
        }
    }

    protected virtual void Morir()
    {
        estaMuerto = true;

        if (animator != null)
            animator.SetTrigger("Death");
    }

    // 🎬 Animation Event
    public void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}

