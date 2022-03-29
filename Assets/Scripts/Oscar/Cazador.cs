using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cazador : MonoBehaviour
{
    public GameObject target;
    //Los estados los he hecho en forma de int
    //0 = Patrullar
    //1 = Alertado (Buscando)
    //2 = Alertado (Persiguiendo)
    public int state = 0;
    public float timePassed = 0;
    public float secondsToDismiss;

    private void Update()
    {
        //Si el target no es igual a nada, es decir, si tiene target, pasa a modo perseguir, si deja de tener target modo Buscando y si pasa un tiempo sin ver al target vuelve a la patrulla.
        if(target != null)
        {
            state = 2;
            timePassed = 0;
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
    }
}
