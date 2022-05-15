using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    [Header( "SCRIPT PLAYER_POLIMORFO")]
    [SerializeField] private Polimorfo _polimorfo;


    // Start is called before the first frame update
    void Start()
    {
        _polimorfo = FindObjectOfType<Polimorfo>();
    }

    // Update is called once per frame
    void Update()
    {
        MarcarEnemigo();   
    }

    private void MarcarEnemigo()
    {
        // Si el enemigo que tenga este escript es el enemigo más cercano al jugador y es visible en la cámara de nuestro jugador, estará marcado
        if (this == _polimorfo.ClosestEnemy && IsVisible(_polimorfo._camera, _polimorfo.ClosestEnemy))
        {
            GetComponent<Outline>().enabled = true;
        }
        // Si no se cumple no estará marcado
        else
        {
            GetComponent<Outline>().enabled = false;
        }
    }
    
    // Función para saber que hay en el fov de la cámara
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
