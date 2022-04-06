using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Controller : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform inside, outside;

    [SerializeField] private float timing;
    private GameObject player;
    
    public bool enter;
    public bool exit;

    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        // Buscamos al player con el tag "Player" y obtenemos su posición
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si el boolean de entrar esta true, la posición y la rotación actual del player se va a cambiar por la
        // posición y la rotación del empty object de entrada y el timing multiplicamos por Time.deltatime
        if (enter)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, inside.position, timing * Time.deltaTime);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, inside.rotation, timing * Time.deltaTime);

            // Si se pulsa la tecla "T", podremos salir del armario
            if (Input.GetKeyDown(KeyCode.T))
            {
                enter = false;
                exit = true;
            }
        }
        
        // Si el boolean de salir esta true, la posición y la rotación actual del player se va a cambiar por la
        // posición y la rotación del empty object de salida y el timing multiplicamos por Time.deltatime
        if (exit)
        {
            playerTransform.position = Vector3.Lerp(playerTransform.position, outside.position, timing * Time.deltaTime);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, outside.rotation, timing * Time.deltaTime);
           
            // Activamos la corutine
            StartCoroutine(HideFinal());
        }
    }
    
    // Corutine para desactivar el boolean de salida
    IEnumerator HideFinal()
    {
        yield return new WaitForSeconds(2);
        exit = false;
    }
}
