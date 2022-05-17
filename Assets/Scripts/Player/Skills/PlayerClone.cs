using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject playerClone;
    [SerializeField] private GameObject spawnerClone;
    
    private IEnumerator destruirClone;
    [Header("CONTROL")]
    [SerializeField] private string control;
    // Setear la tecla
    
    public void setControl(string tecla)
    {
        control = tecla;
    }

    private void Update()
    {
        SpawnearClone();
        
        // Añadimos a la variable la funcion que queremos que haga
        destruirClone = TimeToDestroyClone();
    }

    private void SpawnearClone()
    {
        if (Input.GetKeyDown(control) && GameObject.FindWithTag("PlayerClone") == null)
        {
            // Spawneamos el prefab en la posicion del player y donde estemos mirando
            Instantiate(playerClone, spawnerClone.transform.position, transform.rotation);
            
            // Empezamos la corrutina DestruirClone
            StartCoroutine(destruirClone);
            
            // Si existe un objeto con el nombre "PlayerClone" lo destruimos (esto nos sirve para solo tener un clon spawneado)
            /*if (GameObject.FindWithTag("PlayerClone") != null)
            {
                // Destruimos el objeto con el tag "PlayerClone"
                Destroy(GameObject.FindWithTag("PlayerClone"));
                // Paramos la corrutina DestruirClone
                StopCoroutine(destruirClone);
            }*/
        }
        
        // Si existe un objeto con el tag "PlayerClone" se moverá hacia delante
        if (GameObject.FindWithTag("PlayerClone") != null)
        {
            GameObject.FindWithTag("PlayerClone").transform.Translate(Vector3.forward * 3 * Time.deltaTime);
        }
    }

    private IEnumerator TimeToDestroyClone()
    {
        // Después de 6 segundos destruimos el objeto con el tag "PlayerClone"
        yield return new WaitForSeconds(6);
        Destroy(GameObject.FindWithTag("PlayerClone"));
    }
}
