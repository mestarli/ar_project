using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

/*[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]*/

public class Player_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed;
    [SerializeField] private float sensibiltyMouse;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float jumpForce;
    
    public bool iCanJump;

    private Vector2 inputMov;
    private Vector2 inputRot;
    private Rigidbody rb;
    private float rotX;

    //public Animator anim;
    //private float x, y;
    
    void Start()
    {
        // Negamos que podamos saltar
        iCanJump = false;
        
        // Recuperamos los componentes Rigidbody y Animator 
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        
        // Recuperación de la rotación "vertical" de la camara
        rotX = playerCamera.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Llamada de los métodos de movimiento, correr y saltar
        Player_Movement();
        Player_Run();
        Player_Jump();
    }

    #region Player Movement

    private void Player_Movement()
    {
        // Leemos los inputs para el desplazamiento del player
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");
        
        // Leemos los inputs para la rotació del player
        inputRot.x = Input.GetAxis("Mouse X") * sensibiltyMouse;
        inputRot.y = Input.GetAxis("Mouse Y") * sensibiltyMouse;

        // Método que se encarga de actualizar la rotación de la cámara
        MovePlayerCamera();

        rb.velocity = transform.forward * speed * inputMov.y // Movimiento hacia adelante y atrás del player 
                      + transform.right * speed * inputMov.x // Movimiento hacia izquierda y derecha del player
                      + new Vector3(0, rb.velocity.y, 0); // Para hacer que baje por la gravedad
        
        /*
        // Recuperamos los floats del animator para que se ejecuten las animaciones del player
        anim.SetFloat("Speed_X", x);
        anim.SetFloat("Speed_Y", y);
        
        // Movimiento player
        transform.Translate(0, 0, y * Time.deltaTime * speed);

        // Rotación player
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        */
    }

    private void MovePlayerCamera()
    {
        rotX -= inputRot.y;
        
        // Establecemos límites en la rotación vertical de la cámara
        rotX = Mathf.Clamp(rotX, 10, 25);
        
        // Rotación horizontal de la cámara
        transform.Rotate(0, inputRot.x * sensibiltyMouse, 0f);
        
        // Rotación vertical de la cámara
        playerCamera.transform.localRotation=Quaternion.Euler(rotX, 0f, 0f);
    }

    #endregion

    #region Player Run

    private void Player_Run()
    {
        // Si mantenemos presionado shift izquierdo se aumenta la speed y se activa la animación de correr
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10f;
            //anim.SetBool("Run", true);
        }
        
        // Si se deja de presionar shift izquierdo la velocidad se resetea a la velocidad inicial y se desactiva
        // la animación de correr
        else
        {
            speed = 5f;
            //anim.SetBool("Run", false);
        }
    }

    #endregion

    #region Player Jump

    public void Player_Jump()
    {
        if (iCanJump)
        {
            // Si presionamos la tecla "Espacio" se activará la animación de saltar y impulsaremos el rigidbody 
            // hacia arriba en y
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //anim.SetBool("Jump", true);
                rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
            }
            
            // Desactivamos la animación de saltar
            else
            {
                //anim.SetBool("Jump", false);
            }
        }   
    }

    #endregion
}
