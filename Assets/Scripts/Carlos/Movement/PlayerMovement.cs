using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    //Variables
    
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundmask;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGrounded;
    
    void Update()
    {
        CharacterMovement();
    }

    private void CharacterMovement()
    {
        // La booleana IsGrounded se activará siempre y cuando detecte que su collider toque una mesh con el nombre de layer "Ground".
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        // Si estamos en el suelo y no estamos saltando, pondremos en negativo el eje vertical de nuestro player para que tenga gravedad.
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Recogemos los valores del teclado en X e Y para el movimiento horizontal y vertical del player.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // El vector3 Move = la posicion horizontal dependiendo del valor en X (positivo o negativo) y
        // la posicion Vertical dependiendo del valor en z (positivo o negativo).
        Vector3 move = transform.right * x + transform.forward * z;

        // El movimiento del player dependerá del eje en el que se mueva y su velocidad.
        controller.Move(move * speed * Time.deltaTime);

        // Si saltamos con Espacio y estamos en el suelo nuestro eje vertical será igual que (nuestro jumpHeight * -2 * gravedad).
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }
        
        // Sumamos la gravedad * Time.deltaTime al eje vertical del player.
        velocity.y += gravity * Time.deltaTime;

        // El movimiento del player dependerá también su vector3 * Time.deltaTime.
        controller.Move(velocity * Time.deltaTime);
    }
}
