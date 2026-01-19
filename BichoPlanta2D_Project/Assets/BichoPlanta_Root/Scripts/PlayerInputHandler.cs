using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    //Variables de referencia
    private InputAction _moveAction, _jumpAction;


    //Variables de estadisticas del player


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        _jumpAction.performed += Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Llamar al salto del Pleyer
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector._moveAction.ReadValue<Vector2>();
    }
       
}
