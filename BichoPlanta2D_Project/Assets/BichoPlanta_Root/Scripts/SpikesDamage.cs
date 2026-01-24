using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int danio = 3; // daño fijo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.RecibirDanio(danio);
        }
    }
}
