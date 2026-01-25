using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int danio = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.RecibirDanio(danio);
        }
    }
}

