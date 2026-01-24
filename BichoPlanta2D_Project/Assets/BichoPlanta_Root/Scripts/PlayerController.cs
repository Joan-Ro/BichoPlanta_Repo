using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Vida")]
    public PlayerHealth playerHealth; // Asignar en Inspector

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    [Header("Chequeo suelo")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Ataque")]
    public GameObject lenguaCollider;
    public float tiempoLengua = 0.3f;

    Rigidbody2D rb;
    Animator anim;

    InputAction moveAction;
    InputAction jumpingAction;
    InputAction attackAction;
    InputAction hitAction;

    float moveInput;
    bool isGrounded;
    Vector3 originalScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;

        // Buscar acciones (tu sistema original)
        moveAction = InputSystem.actions.FindAction("Move");
        jumpingAction = InputSystem.actions.FindAction("Jumping");
        attackAction = InputSystem.actions.FindAction("Attack");
        hitAction = InputSystem.actions.FindAction("Hit");

        // Suscribir eventos (tu sistema original)
        if (jumpingAction != null)
            jumpingAction.performed += OnJump;
        if (attackAction != null)
            attackAction.performed += OnAttack;
        if (hitAction != null)
            hitAction.performed += OnHit;
    }

    void OnEnable()
    {
        if (moveAction != null) moveAction.Enable();
        if (jumpingAction != null) jumpingAction.Enable();
        if (attackAction != null) attackAction.Enable();
        if (hitAction != null) hitAction.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.Disable();
        if (jumpingAction != null) jumpingAction.Disable();
        if (attackAction != null) attackAction.Disable();
        if (hitAction != null) hitAction.Disable();
    }

    void Update()
    {
        // Movimiento (tu sistema original)
        moveInput = 0f;
        if (moveAction != null)
            moveInput = moveAction.ReadValue<float>();
        if (Mathf.Abs(moveInput) < 0.01f)
            moveInput = Input.GetAxisRaw("Horizontal");

        // Ground check
        if (groundCheck != null)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Animaciones
        bool isRunning = Mathf.Abs(moveInput) > 0.01f;
        anim.SetBool("Running", isRunning);
        anim.SetBool("Jumping", !isGrounded);

        // Flip (tu sistema original)
        if (isRunning)
        {
            if (moveInput > 0f)
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            else if (moveInput < 0f)
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Tus métodos originales
    void OnJump(InputAction.CallbackContext ctx)
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("Attack");
        ActivarLengua(); // ¡NUEVO!
    }

    void OnHit(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("Hit");
    }

    // LENGUA (nuevo)
    public void ActivarLengua()
    {
        if (lenguaCollider != null)
        {
            lenguaCollider.SetActive(true);
            Invoke("DesactivarLengua", tiempoLengua);
        }
    }

    public void DesactivarLengua()
    {
        if (lenguaCollider != null)
            lenguaCollider.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
