using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Variables de referencia
    private Rigidbody2D playerRB;
    private Animator anim;
    private float horizontalInput;


    //Variables de estadisticas del player
    public float speed;
    public float jumpforce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();





    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRB.linearVelocity = new Vector2(horizontalInput * speed,playerRB.linearVelocity.y);
    }
    void Jump ()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
        }
        
        

    }
}
