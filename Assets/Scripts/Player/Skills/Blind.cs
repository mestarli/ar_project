using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour
{
    
    public Camera cam;
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 30;

    private Vector3 destination;
    
    public KeyCode control;
    
    private bool isPosible = false;
    void Start()
    {
        if (LoadSkill.Instance.EnableSkill_01.GetType().ToString() == "Blind")
        {
            control = KeyCode.T;
        }else if (LoadSkill.Instance.EnableSkill_02.GetType().ToString() == "Blind")
        {
            control = KeyCode.R; 
        }
        else if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() ==  "Blind")
        {
            control = KeyCode.E; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control))
        {
            if (!isPosible)
            {
                StartCoroutine(CorroutineShootProjectile());
            }
            //
        }
    }
    
    IEnumerator CorroutineShootProjectile()
    {
        isPosible = true;
        ShootProjectile();
        yield return new WaitForSeconds(6);
        isPosible = false;
        
    }

    private void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity =
            (destination - firePoint.position).normalized * projectileSpeed;
    }
}
