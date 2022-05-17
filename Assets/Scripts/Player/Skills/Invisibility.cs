using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public KeyCode control;
    
    // Start is called before the first frame update
    void Start()
    {
        if (LoadSkill.Instance.EnableSkill_01.GetType().ToString() == "Invisibility")
        {
            control = KeyCode.T;
        }else if (LoadSkill.Instance.EnableSkill_02.GetType().ToString() == "Invisibility")
        {
            control = KeyCode.R; 
        }
        else if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() ==  "Invisibility")
        {
            control = KeyCode.E; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
