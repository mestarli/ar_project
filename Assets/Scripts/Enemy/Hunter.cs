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
    bool cegado;
    Transform shootPoint;
    bool searching;
    NavMeshAgent _navMeshAgent;
   public  FieldOfView fov;

    Animator animator;
    
    // Variables
    [Header( "SCRIPT PLAYER_POLIMORFO")]
    [SerializeField] private Polimorfo _polimorfo;
    
    private void Awake()
    {
        shootPoint = transform.GetChild(3);
        animator = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
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

    private void Start()
    {
        _polimorfo = player.GetComponent<Polimorfo>();
    }
    
    private void Update()
    {
        MarcarEnemigo();   
        
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

            var dist = target.transform.position - transform.position;
            if (range > dist.magnitude && recharge <= 0)
            {
                animator.SetBool("Disparar", true);
                recharge = rechargeTime;
            }
            if (dist.magnitude > 10)
            {

                _navMeshAgent.SetDestination(target.transform.position);
            }
            else
            {
                _navMeshAgent.SetDestination(transform.position);
                transform.LookAt(target.transform.position);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }

            searching = false;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Smoke")
        {
            Debug.Log("ey");
            target = null;
            state = 1;
            StartCoroutine(Cegar());
        }
        
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("ey bullet");
            target = null;
            state = 1;
            StartCoroutine(Cegar());
        }
        else
        {

        }
    }

    IEnumerator Cegar()
    {
        cegado = true;
        float aux = GetComponent<NavMeshAgent>().speed;
        GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(6);
        GetComponent<NavMeshAgent>().speed = aux;
        cegado = false;
    }
    private void MarcarEnemigo()
    {
        // Si el enemigo que tenga este escript es el enemigo m??s cercano al jugador y es visible en la c??mara de nuestro jugador, estar?? marcado
        if (this == _polimorfo.ClosestEnemy && IsVisible(_polimorfo._camera, _polimorfo.ClosestEnemy))
        {
            GetComponent<Outline>().enabled = true;
        }
        // Si no se cumple no estar?? marcado
        else
        {
            GetComponent<Outline>().enabled = false;
        }
    }
    
    // Funci??n para saber que hay en el fov de la c??mara
    public bool IsVisible(Camera c, Hunter currentEnemy)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = currentEnemy.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }
    public void Disparar()
    {

        animator.SetBool("Disparar", false);
        Instantiate(bullet, shootPoint.position, transform.rotation);
    }
}
