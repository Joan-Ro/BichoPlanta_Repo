using UnityEngine;

public class LenguaAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Lengua tocó: " + other.name);

        EnemyPatrol enemigo = other.GetComponent<EnemyPatrol>();
        if (enemigo != null)
        {
            Debug.Log("🐸 MATANDO " + other.name);
            enemigo.RecibirGolpe();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // También mientras está encima
        EnemyPatrol enemigo = other.GetComponent<EnemyPatrol>();
        if (enemigo != null)
            enemigo.RecibirGolpe();
    }
}
