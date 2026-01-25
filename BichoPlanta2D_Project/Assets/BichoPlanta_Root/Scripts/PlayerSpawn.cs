using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float tiempoRespawn = 1.2f;

    Rigidbody2D rb;
    Animator anim;
    PlayerHealth health;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
    }

    public void Morir()
    {
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        anim.SetTrigger("Death");
        Invoke(nameof(Respawnear), tiempoRespawn);
    }

    void Respawnear()
    {
        transform.position = spawnPoint.position;

        rb.simulated = true;
        health.ReiniciarVida();

        anim.ResetTrigger("Death");
    }
}

