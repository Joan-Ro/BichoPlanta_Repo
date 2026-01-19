using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f, _jumpForce = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody.GetComponent<Rigidbody2D>();
    }

  
    // Update is called once per frame
    private void FixedUpdate()
    {
       
    }

    public void Move(Vector2 moveVector);
    
}
