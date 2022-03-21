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
    public float speed = 5.0f;
    public float rotationSpeed = 200.0f;

    //public Animator anim;
    private float x, y;
    
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Player_Movement();
    }

    #region Player Movement

    public void Player_Movement()
    {
        // Funciones
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * speed * Time.deltaTime * y);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * x);


        /*
        anim.SetFloat("Speed_X", x);
        anim.SetFloat("Speed_Y", y);
        
        // Movimiento player
        transform.Translate(0, 0, y * Time.deltaTime * speed);

        // Rotación player
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        */
    }

    #endregion
}
