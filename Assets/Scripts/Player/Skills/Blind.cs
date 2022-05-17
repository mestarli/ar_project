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
    [Space(10)]
    [Header("CONTROL")]
    [SerializeField] private string control;
    // Setear la tecla
    public void setControl(string tecla)
    {
        control = tecla;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(control))
        {
            ShootProjectile();
        }
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
