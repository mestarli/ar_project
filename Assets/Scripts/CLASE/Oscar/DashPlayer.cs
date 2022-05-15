using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayer : MonoBehaviour
{
    public int uses;
    public float cooldown, distance, speed, destinationMultiplier, cameraHeight;

    int maxUses;
    float cooldownTimer;
    bool blinking = false;
    Vector3 destination;
    Transform cam;
    LayerMask layerMask;

    void Awake()
    {
        maxUses = uses;
        cooldownTimer = cooldown;
        cam = FindObjectOfType<Camera>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Blink();
        }
        if(uses < maxUses)
        {
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
            else
            {
                uses += 1;
                cooldownTimer = cooldown;
            }
        }
        if (blinking)
        {
            var dist = Vector3.Distance(transform.position, destination);
            if (dist > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            }
            else { blinking = false; }
        }
    }

    void Blink()
    {
        if (uses > 0)
        {
            uses -= 1;
            RaycastHit hit;
            if(Physics.Raycast(cam.position, cam.forward,out hit, distance, layerMask))
            {
                destination = hit.point * destinationMultiplier;

            }
            else
            {
                destination = (cam.position + cam.forward.normalized * distance) * destinationMultiplier;
            }

            destination.y += cameraHeight;
            blinking = true;
        }
    }
}
