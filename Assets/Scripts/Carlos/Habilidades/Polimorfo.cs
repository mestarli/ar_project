using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Polimorfo : MonoBehaviour
{
    // Variables
    [SerializeField] private float distanceToClosestEnemy;
    [SerializeField] private float distanceToEnemy;
    [SerializeField] public Enemy ClosestEnemy;
    [SerializeField] public GameObject[] allProps;
    [SerializeField] public GameObject Prop;
    [SerializeField] public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        allProps = GameObject.FindGameObjectsWithTag("Prop");
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        ConvertirPolimorfo();
    }

    private void ConvertirPolimorfo()
    {
        if (Input.GetKeyDown(KeyCode.F) && ClosestEnemy != null)
        {
            Prop = allProps[Random.Range(0, allProps.Length)];
            
            Instantiate(Prop, ClosestEnemy.transform.position, ClosestEnemy.transform.rotation);
            Destroy(ClosestEnemy.gameObject);
        }
    }
    
    private void FindClosestEnemy()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("" + hit.transform.gameObject.name);
        }
        
        distanceToClosestEnemy = Mathf.Infinity;
        ClosestEnemy = null;
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            if (currentEnemy.IsVisible(currentEnemy.camera, currentEnemy))
            {
                distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;

                if (hit.transform.gameObject.tag == "Enemy")
                {
                    ClosestEnemy = hit.transform.gameObject.GetComponent<Enemy>();
                }
                else if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    ClosestEnemy = currentEnemy;
                }
            }
        }
        
        //Debug.Log("" + ClosestEnemy);
        
        if (ClosestEnemy != null)
        {
            Debug.DrawLine(transform.position, ClosestEnemy.transform.position);
        }
    }
}
