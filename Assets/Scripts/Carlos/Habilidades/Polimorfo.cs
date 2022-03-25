using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polimorfo : MonoBehaviour
{
    // Variables
    [SerializeField] private float distanceToClosestEnemy;
    [SerializeField] private float distanceToEnemy;
    [SerializeField] private Enemy ClosestEnemy;
    [SerializeField] private GameObject Enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
    }

    private void FindClosestEnemy()
    {
        distanceToClosestEnemy = Mathf.Infinity;
        ClosestEnemy = null;
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                ClosestEnemy = currentEnemy;
                ClosestEnemy.GetComponent<Outline>().enabled = true;
            }
            else
            {
                Enemies.GetComponent<Outline>().enabled = false;
            }
        }
        Debug.Log("" + ClosestEnemy);
        Debug.DrawLine(transform.position, ClosestEnemy.transform.position);
    }
}
