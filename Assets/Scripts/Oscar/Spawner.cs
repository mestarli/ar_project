using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRadius;
    public GameObject enemy;
    PlayerMovementWOChC player;
    public float refreshSeconds;
    float seconds;
    bool spawned;
    private void Awake()
    {
        seconds = refreshSeconds;
        player = FindObjectOfType<PlayerMovementWOChC>();
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
    private void Update()
    {
        seconds -= Time.deltaTime;
        if(seconds <= 0)
        {
            seconds = refreshSeconds;
            var dist = transform.position - player.transform.position;
            if (dist.magnitude <= spawnRadius && !spawned)
            {
                spawned = true;
                foreach (Transform waypoint in transform)
                {
                    Instantiate(enemy, waypoint.position, waypoint.rotation);
                }
            }
        }
    }
}
