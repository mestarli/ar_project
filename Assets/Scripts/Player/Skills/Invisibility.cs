using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    [Header("CONTROL")]
    [SerializeField] private string control;
    // Setear la tecla

    public void setControl(string tecla)
    {
        control = tecla;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
