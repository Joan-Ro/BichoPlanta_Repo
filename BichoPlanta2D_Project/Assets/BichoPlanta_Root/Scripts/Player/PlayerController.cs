using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Vida")]
    public PlayerHealth playerHealth;

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

        moveAction = InputSystem.actions.FindAction("Move");
        jumpingAction = InputSystem.actions.FindAction("Jumping");
        attackAction = InputSystem.actions.FindAction("Attack");
        hitAction = InputSystem.actions.FindAction("Hit");

        if (jumpingAction != null)
            jumpingAction.performed += OnJump;
        if (attackAction != null)
            attackAction.performed += OnAttackInput;
        if (hitAction != null)
            hitAction.performed += OnHit;
    }

    void OnEnable()
    {
        moveAction?.Enable();
        jumpingAction?.Enable();
        attackAction?.Enable();
        hitAction?.Enable();
    }

    void OnDisable()
    {
        moveAction?.Disable();
        jumpingAction?.Disable();
        attackAction?.Disable();
        hitAction?.Disable();
    }

    void Update()
    {
        moveInput = moveAction != null ? moveAction.ReadValue<float>() : Input.GetAxisRaw("Horizontal");

        if (groundCheck != null)
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        bool isRunning = Mathf.Abs(moveInput) > 0.01f;
        anim.SetBool("Running", isRunning);
        anim.SetBool("Jumping", !isGrounded);

        if (isRunning)
        {
            if (moveInput > 0)
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            else
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // INPUT SYSTEM
    void OnJump(InputAction.CallbackContext ctx)
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnAttackInput(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("Attack");
    }

    void OnHit(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("Hit");
    }

        public void OnAttack()
    {
        ActivarLengua();
    }

    public void ActivarLengua()
    {
        if (lenguaCollider != null)
        {
            lenguaCollider.SetActive(true);
            Debug.Log("Lengua ACTIVADA");
            Invoke("DesactivarLengua", tiempoLengua);
        }
    }

    public void DesactivarLengua()
    {
        if (lenguaCollider != null)
        {
            lenguaCollider.SetActive(false);
            Debug.Log("Lengua DESACTIVADA");
        }
    }



    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}

