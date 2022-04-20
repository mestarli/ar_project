using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float awarenedViewRadius;
    [Range(0,360)]
    public float viewAngle;
    [Range(0,360)]
    public float awarenedViewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    Hunter cazador;

    private void Start()
    {
        cazador = FindObjectOfType<Hunter>();
    }
    private void Update()
    {
        FindVisibleTarget();
    }
    void FindVisibleTarget()
    {
        //Resetea el target para que no haya en caso de que haya dejado de verlo en este frame
        cazador.target = null;
        Collider[] targetsInViewRadius;
        //Hace un array para todos los targets dentro del rango, está hecho por si hay mas de un player pero como solo hay uno yo lo guardo en una variable normal y no una lista.
        if (cazador.state == 0)
        {
            targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        }
        else
        {
            targetsInViewRadius = Physics.OverlapSphere(transform.position, awarenedViewRadius, targetMask);
        }

        int i=0;
        //Por cada objetivo hace un calculo matemático para ver si están dentro del angulo que hemos seteado y si lo está lo guarda como target en cazador
        while(i < targetsInViewRadius.Length)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward,dirToTarget) < awarenedViewAngle / 2 && cazador.state > 0 || Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    cazador.target = target.gameObject;
                   
                }
            }
            i++;
        }
    }
    //Esto es una cosa que usa el FieldOfViewEditor
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
