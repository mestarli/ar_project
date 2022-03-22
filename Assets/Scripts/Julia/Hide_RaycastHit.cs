using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_RaycastHit : MonoBehaviour
{
    // Variables
    private int range = 2;
    
    void Update()
    {
        // Creamos el raycast
        RaycastHit hit;
     
        // Creamos "Rayo"
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Hide_Controller>() == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Hide_Controller>().enter = true;
                }
            }
        }
    }

    // Método para que se vea el "rayo" en la pantalla
    private void OnDrawGizmos()
    {
        // Cambiamos el color del gizmos
        Gizmos.color = Color.cyan;
        
        // Pintamos la raya en el gizmos
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * range);
    }
}
