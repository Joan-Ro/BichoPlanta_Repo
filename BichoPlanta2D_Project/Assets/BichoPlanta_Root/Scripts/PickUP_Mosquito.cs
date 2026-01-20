using UnityEngine;

public class MosquitoPickup : MonoBehaviour
{
    public float speed = 1f;
    public float minX = -75f;
    public float maxX = -70f;

    int direction = -1;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x <= minX)
            direction = 1;
        else if (transform.position.x >= maxX)
            direction = -1;

        animator.SetInteger("direction", direction);

    }
}

