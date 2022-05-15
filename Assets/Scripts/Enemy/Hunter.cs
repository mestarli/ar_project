using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    public GameObject firstWaypoint;
    public List<GameObject> route;
    int indexRoute;
    //Los estados los he hecho en forma de int
    //0 = Patrullar
    //1 = Alertado (Buscando)
    //2 = Alertado (Persiguiendo)
    public int state = 0;
    public float timePassed = 0;
    public float secondsToDismiss;
    Vector3 lastSeenPosition;
    float random = 0;
    public float range = 0;
    float recharge = 0;
    public float rechargeTime = 0;
    public GameObject bullet;

    bool searching;
    NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        route.Add(firstWaypoint);
       foreach(Transform waypoint in firstWaypoint.transform)
        {
            if (waypoint.tag == "Waypoint")
            {
                route.Add(waypoint.gameObject);
            }
        }
        timePassed = secondsToDismiss;
    }
    
    private void Update()
    {
        recharge -= Time.deltaTime;
        //Si el target no es igual a nada, es decir, si tiene target, pasa a modo perseguir, si deja de tener target modo Buscando y si pasa un tiempo sin ver al target vuelve a la patrulla.
        if(target != null)
        {
            state = 2;
            timePassed = 0;
            lastSeenPosition = target.transform.position;
        }
        else
        {
            if (timePassed < secondsToDismiss)
            {
                state = 1;
                timePassed += Time.deltaTime;
            }
            else
            {
                state = 0;
            }
        }
        if (state == 2)
        {
            _navMeshAgent.SetDestination(target.transform.position);
            searching = false;

            var dist = target.transform.position - transform.position;
            if (range > dist.magnitude && recharge <= 0)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                recharge = rechargeTime;
            }
            
        }
        if(state == 1)
        {
            var dist = lastSeenPosition - transform.position;
            if(dist.magnitude >= 1 && !searching)
            {
                _navMeshAgent.SetDestination(lastSeenPosition);
            }
            else
            {
                if (!searching)
                {
                    transform.LookAt(player.transform.position);
                    searching = true;
                }
                else
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 25 * Time.deltaTime, transform.eulerAngles.z);
                }
            }
        }
        if(state == 0)
        {
            var dist = route[indexRoute].transform.position - transform.position;
            _navMeshAgent.SetDestination(route[indexRoute].transform.position);
            if (dist.magnitude <= 1)
            {
                if (random < 0)
                {
                    random = Random.Range(1, 10);
                    indexRoute++;
                    if (indexRoute >= route.Count - 1)
                    {
                        indexRoute = 0;
                    }
                }
                else
                {

                    random -= Time.deltaTime;
                }
            }
        }
    }
}
