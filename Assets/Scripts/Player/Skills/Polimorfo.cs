using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Polimorfo : MonoBehaviour
{
    // Variables
    [Header("PROPS")]
    [SerializeField] public GameObject[] allProps;
    [SerializeField] public GameObject Prop;
    
    [Space(10)]
    [Header("CÁMARA")]
    [SerializeField] public Camera _camera;
    [SerializeField] private float range;
    
    [Space(10)]
    [Header("ENEMIGO")]
    [SerializeField] public Enemy ClosestEnemy;
    [SerializeField] private float distanceToClosestEnemy;
    [SerializeField] private float distanceToEnemy;

    // Start is called before the first frame update
    void Start()
    {
        // Metemos en la lista de objetos todos los props del mapa
        allProps = GameObject.FindGameObjectsWithTag("Prop");
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        FindLookingEnemy();
        ConvertirPolimorfo();
    }

    private void FindClosestEnemy()
    {
        //
        // Encontrar enemigos por distancia de player
        // 
        distanceToClosestEnemy = Mathf.Infinity;
        ClosestEnemy = null;
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        
        foreach (Enemy currentEnemy in allEnemies)
        {
            // Buscamos entre todos los enemigos si están visibles en nuestra pantalla
            if (currentEnemy.IsVisible(_camera, currentEnemy))
            {
                // La distancia del enemigo será la resta de su posición y la nuestra
                distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                
                // La distancia del enemigo será automaticamente la distancia del enemigo más corto, entonces si encontramos que una distancia de otro enemigo es menor que la más corta esta será la más corta
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    // El enemigo más cercano será el que esté más cerca de nosotros
                    ClosestEnemy = currentEnemy;
                }
            }
        }
        
        if (ClosestEnemy != null)
        {
            // SOLO EN EDITOR // si existe un enemigo cercano se dibujará una linea desde nuestra posición a la suya
            Debug.DrawLine(transform.position, ClosestEnemy.transform.position);
        }
    }

    private void FindLookingEnemy()
    {
        //
        // Encontrar enemigos si estamos apuntandolos
        // 
        RaycastHit hit;
        
        // Si apuntamos a un enemigo que esté como máximo en el "range" este será el enemigo más cercano
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            Enemy Enemigo = hit.transform.GetComponent<Enemy>();
            
            if (Enemigo != null)
            {
                ClosestEnemy = Enemigo;
            }
        }
    }
    
    private void ConvertirPolimorfo()
    {
        // Si le damos a la "F" y hay un enemigo cercano...
        if (Input.GetKeyDown(KeyCode.F) && ClosestEnemy != null)
        {
            // El prop será de forma aleatoria uno de todos los del mapa
            Prop = allProps[Random.Range(0, allProps.Length)];
            
            // Spawnearemos el prop en la posición del enemigo y luego lo destruiremos
            Instantiate(Prop, ClosestEnemy.transform.position, ClosestEnemy.transform.rotation);
            Destroy(ClosestEnemy.gameObject);
        }
    }
}
