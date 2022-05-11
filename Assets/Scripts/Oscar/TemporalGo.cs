using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalGo : MonoBehaviour
{
    public float lifetime;
    private void Start()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
