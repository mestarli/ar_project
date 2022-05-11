using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHunter : MonoBehaviour
{
    public float dno = 0;
    public float speed = 0;
    public float range = 0;
    float path = 0;
    public Rigidbody _rigidbody;
    public GameObject collisionGO;

    public virtual void Start()
    {
        _rigidbody = FindObjectOfType<Rigidbody>();
        StartCoroutine(Avance());
    }

    // Update is called once per frame
  


    public virtual IEnumerator Avance()
    {
        while (range*10000 >= path)
        {
            _rigidbody.velocity = transform.forward * speed;
            path += speed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);

        }
        Hit();
    }
    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Obstaculo")
        {
            Hit();
        }
        else if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<PlayerMovement>().MetodoRecibirDno;
            Hit();
        }
    }

    void Hit()
    {
        Instantiate(collisionGO, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}

