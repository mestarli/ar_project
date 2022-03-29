using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Variables
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float xRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        // Recogemos los valores del ratón en X e Y y le añadimos una sensibilidad.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // La rotación en vertical será menos MouseY para invertir la dirección al mirar hacia arriba o abajo.
        xRotation -= mouseY;
        // Bloqueamos poder mirar hacia arriba y abajo mas de 90 grados.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // La rotación del player en horizontal será con el MouseX
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
