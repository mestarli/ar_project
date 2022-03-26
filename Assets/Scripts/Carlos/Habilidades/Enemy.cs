using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    [SerializeField] private Polimorfo _polimorfo;
    [SerializeField] public Camera camera;

    
    // Start is called before the first frame update
    void Start()
    {
        _polimorfo = FindObjectOfType<Polimorfo>();
        camera = _polimorfo.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this == _polimorfo.ClosestEnemy && IsVisible(camera, _polimorfo.ClosestEnemy))
        {
            GetComponent<Outline>().enabled = true;
        }
        else
        {
            GetComponent<Outline>().enabled = false;
        }
    }
    
    public bool IsVisible(Camera c, Enemy currentEnemy)
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
}
