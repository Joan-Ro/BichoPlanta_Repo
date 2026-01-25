using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Daño al jugador")]
    public int danio = 1;

    [Header("Animaciones")]
    public Animator animator;

    private bool estaMuerto = false;
    private float ultimoDaño = 0f;
    public float tiempoEntreDaños = 1f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)  // O OnTriggerStay2D si trigger
    {
        if (estaMuerto || Time.time < ultimoDaño + tiempoEntreDaños) return;

        Collider2D col = collision.collider;
        if (col.CompareTag("Player") && col.name != "TongueCollider")  // Ignora lengua por nombre
        {
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();  // O tu script de vida
            if (playerHealth != null)
            {
                playerHealth.RecibirDanio(danio);
                ultimoDaño = Time.time;
                Debug.Log("Daño a jugador desde enemigo");
            }
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
