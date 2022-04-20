using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement_Edited_Pablo : MonoBehaviour
{
    // Wall stuff
    public KeyCode castKeyBind, directionKeyBind;
    public float range;
    public GameObject iceWallPreview, iceWallOBJ;
    public LayerMask LayerMask;
    private bool direction, casting;
    
    //Variables
    public static PlayerMovement_Edited_Pablo instance;
    
    [Header("PLAYER")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject bullet;

    [Space(10)] [Header("VALORES PLAYER")] 
    [SerializeField] public float axisX;
    [SerializeField] public float axisZ;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    [Space(10)]
    [Header("VALORES SUELO")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundmask;

    [Space(10)]
    [Header("INFO PLAYER")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool isGrounded;

    private void Start()
    {
        instance = this;
        cam = Camera.main.transform;
    }

    void Update()
    {
        CharacterMovement();

        IceBullet();
    }

    private void IceBullet()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet, cam.position + cam.forward, cam.rotation);
        }*/

        if (Input.GetKeyDown(castKeyBind))
        {
            casting = !casting;
            if (!casting)
            {
                iceWallPreview.SetActive(false);
            }
        }
        
        if (casting)
        {
            CastingIceWall();
        }
    }

    private void CastingIceWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.forward, out hit, range, LayerMask))
        {
            if (iceWallPreview.activeSelf)
            {
                iceWallPreview.SetActive(true);
            }
            Quaternion rotation = Quaternion.Euler(0,0,0);
            if (direction)
            {
                rotation.y = 1;
            }
            else
            {
                rotation.y = 0;
            }

            iceWallPreview.transform.localRotation = rotation;
            iceWallPreview.transform.position = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(iceWallOBJ, hit.point, iceWallPreview.transform.rotation);
                casting = false;
                iceWallPreview.SetActive(false);
            }
        }
        else
        {
            iceWallPreview.SetActive(false);
        }
        
        if (Input.GetKeyDown(directionKeyBind))
        {
            direction = !direction;
        }
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
        axisX = Input.GetAxis("Horizontal");
        axisZ = Input.GetAxis("Vertical");

        // El vector3 Move = la posicion horizontal dependiendo del valor en X (positivo o negativo) y
        // la posicion Vertical dependiendo del valor en z (positivo o negativo).
        Vector3 move = transform.right * axisX + transform.forward * axisZ;
        
        // Si la magnitud de movimiento supera el valor 1, se dividirá por su propia magnitud (Esto hace que no aumente la velocidad en diagonal)
        if (move.magnitude > 1)
        {
            move /= move.magnitude;
        }

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