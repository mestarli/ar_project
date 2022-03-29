using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cazador : MonoBehaviour
{
    public GameObject target;

    public int state = 0;
    public float timePassed = 0;
    public float secondsToDismiss;

    private void Update()
    {
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
                timePassed *= Time.deltaTime;
            }
            else
            {
                state = 0;
            }
        }
    }
}
