using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public KeyCode control; 
    
    public int uses;
    public float cooldown, distance,path , speed;

    int maxUses;
    float cooldownTimer;
    bool blinking = false;
    bool blinkable= true;


    // Start is called before the first frame update
    void Start()
    {
        if (LoadSkill.Instance.EnableSkill_01.GetType().ToString() == "Dash")
        {
            control = KeyCode.E;
        }else if (LoadSkill.Instance.EnableSkill_02.GetType().ToString() == "Dash")
        {
            control = KeyCode.R; 
        }
        else if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() ==  "Dash")
        {
            control = KeyCode.T; 
        }
    }


    void Awake()
    {
        maxUses = uses;
        cooldownTimer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(control)&& uses>0 && !blinking&&blinkable)
        {
            StartCoroutine(Blink());
            uses--;
        }
            if (uses < maxUses)
            {
                if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
                else
                {
                    uses += 1;
                    cooldownTimer = cooldown;
                }
            }
        
    }

    IEnumerator Blink()
    {
        var dir = Vector3.forward;
        blinking = true;
        path = 0;
        while (blinking)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            path += speed * Time.deltaTime;
            if (path > distance)
            {
                blinking = false;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstaculo")
        {
            blinkable = false;
            blinking = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Obstaculo")
        {
            blinkable = true;
        }
    }
}
