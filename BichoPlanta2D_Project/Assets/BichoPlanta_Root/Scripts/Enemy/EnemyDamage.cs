using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Daño al jugador")]
    public int danio = 1; // configurable en Inspector

    [Header("Animaciones")]
    public Animator animator;

    private bool estaMuerto = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    // Método que se llama cuando el jugador toca al enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaMuerto) return;

        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.RecibirDanio(danio);
        }
    }

    public void RecibirGolpe()
    {
        if (estaMuerto) return;
        estaMuerto = true;
        if (animator != null)
            animator.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }
}
