using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BoxCollider))]

/*[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]*/

public class Player_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce;
    
    public bool iCanJump;

    //public Animator anim;
    //private float x, y;
    
    void Start()
    {
        // Negamos que podamos saltar
        iCanJump = false;
        
        // Recuperamos el componente Animator 
        //anim = GetComponent<Animator>();
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * speed * Time.deltaTime);

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
                //rb.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
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
