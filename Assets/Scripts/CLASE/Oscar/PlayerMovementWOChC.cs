using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementWOChC : MonoBehaviour
{
    // Atributes
    [SerializeField] private float speed;
    [SerializeField] private Transform playerCamera;


    private Vector2 inputMov;
    private Vector2 inputRot;
    public Rigidbody _rigidbody;
    private float rotX;

    bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundmask;


    void Start()
    {
        // Recuperamos el componente Rigidbody del player para poder trabajar con el
        _rigidbody = GetComponent<Rigidbody>();

        rotX = playerCamera.eulerAngles.x; // Recuperamos la rotacion "vertical" de la cámara

        Physics.gravity = new Vector3(0, -72f, 0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            _rigidbody.AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
            isGrounded = false;
        }

        // Leemos los inputs para el desplazamiento del jugador
        inputMov.x = Input.GetAxis("Horizontal");
        inputMov.y = Input.GetAxis("Vertical");


    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            _rigidbody.velocity = transform.forward * speed * inputMov.y  // Movimiento hacia adelante y atrás del PJ
                                  + transform.right * speed * inputMov.x;  // Movimiento hacia izquierda y derecha del PJ

        }
    }


}
