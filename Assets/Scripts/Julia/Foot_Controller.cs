using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot_Controller : MonoBehaviour
{
    // Variables
    public Player_Controller _PlayerController;

    // Si estamos tocando el suelo, la variable booleana del script del player iCanJump será verdadera
    private void OnTriggerStay(Collider other)
    {
        _PlayerController.iCanJump = true;
    }
    
    // Si no estamos tocando el suelo, la variable booleana del script del player iCanJump será falsa
    private void OnTriggerExit(Collider other)
    {
        _PlayerController.iCanJump = false;
    }
}
