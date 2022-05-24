using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Regeneracion : MonoBehaviour
{
    // Variables
    [Header("VALORES VIDA")]
    [SerializeField] private float currentVidaPlayer;
    [SerializeField] private float maxVidaPlayer;
    [SerializeField] private float speedVidaRegeneration;
    [SerializeField] private bool RegenerateVida;
    
    public KeyCode control;
    // Setear la tecla
    

    private void Start()
    {
        maxVidaPlayer = Player.Instance.Maxlife;
        speedVidaRegeneration = 10;
        if (LoadSkill.Instance.EnableSkill_01.GetType().ToString() == "Regeneracion")
        {
            control = KeyCode.E;
        }else if (LoadSkill.Instance.EnableSkill_02.GetType().ToString() == "Regeneracion")
        {
            control = KeyCode.R; 
        }
        else if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() == "Regeneracion")
        {
            control = KeyCode.T; 
        }
        
    }

    private void Update()
    {
        ActivarRegenerarVida();
        currentVidaPlayer = Player.Instance.life;
    }
    
   
    private void ActivarRegenerarVida()
    {
        
        // Si no tenemos máxima vida, le damos a la G y no estamos ya regenerando vida... se activará el bool regenerar vida y la corrutina "RegenerarVida"
        if (currentVidaPlayer < maxVidaPlayer && Input.GetKeyDown(control) && !RegenerateVida)
        {
            RegenerateVida = true;
            StartCoroutine(RegenerarVida());
        }

        // Si está activado el bool regenerarVida nuestra vida se sumara por el Time.DeltaTime * speedVidaRegeneracion
        if (RegenerateVida)
        {
            currentVidaPlayer += Time.deltaTime * speedVidaRegeneration;
            Player.Instance.SumarVida(Time.deltaTime * speedVidaRegeneration);
        }

        // Si el player se está moviendo la velocidad de regeneración disminuirá si no será 10
        if ( PlayerMovement.instance.axisX != 0 || PlayerMovement.instance.axisZ != 0)
        {
            speedVidaRegeneration = 5;
        }
        else
        {
            speedVidaRegeneration = 10;
        }

        // Si cuando estamos regenerando vida llegamos al máximo pararemos de regenerar
        if (currentVidaPlayer >= maxVidaPlayer)
        {
            RegenerateVida = false;
        }

        // Esto nos sirve para nunca poder tener más vida que la máxima posible
        if (currentVidaPlayer > maxVidaPlayer)
        {
            currentVidaPlayer = maxVidaPlayer;
        }
    }

    // Una vez pasen 5 segundos después de activar el regenerarVida se desactivará
    private IEnumerator RegenerarVida()
    {
        yield return new WaitForSeconds(5);
        RegenerateVida = false;
    }
}
