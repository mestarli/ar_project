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

    private Vector2 inputMov;
    private Vector2 inputRot;
    private Rigidbody rb;
    private float rotX;

    //public Animator anim;
    //private float x, y;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();

        rotX = playerCamera.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement();
        Player_Run();
        Player_Hide();
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 10f;
            //anim.SetBool("Run", true);
        }
        else
        {
            speed = 5f;
            //anim.SetBool("Run", false);
        }
    }

    #endregion

    #region Player Hide

    private void Player_Hide()
    {
        
    }

    #endregion
}
