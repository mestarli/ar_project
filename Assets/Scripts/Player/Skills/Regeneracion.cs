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
    [SerializeField] private int maxVidaPlayer;
    [SerializeField] private float speedVidaRegeneration;
    [SerializeField] private bool RegenerateVida;
    [SerializeField] private TextMeshProUGUI vidaTXT;
    
    [Space(10)]
    [Header("DAÑO")]
    [SerializeField] private float Damage;

    private void Start()
    {
        maxVidaPlayer = 100;
        currentVidaPlayer = maxVidaPlayer;
        speedVidaRegeneration = 10;
    }

    private void Update()
    {
        vidaTXT.text = "" + (int)currentVidaPlayer;
        
        QuitarVida();
        ActivarRegenerarVida();
    }

    #region - DamagePlayer -

    private void QuitarVida()
    {
        // Si apretamos a la Q y tenemos vida nos restará el valor que haya en damage
        if (Input.GetKeyDown(KeyCode.Q) && currentVidaPlayer > 0)
        {
            currentVidaPlayer = currentVidaPlayer - Damage;
        }

        // Esto nos sirve para que nunca podamos tener menos valor que 0 en la vida
        if (currentVidaPlayer < 0)
        {
            currentVidaPlayer = 0;
        }
    }

    #endregion

    #region - VidaPlayer -
    
    private void ActivarRegenerarVida()
    {
        // Si no tenemos máxima vida, le damos a la G y no estamos ya regenerando vida... se activará el bool regenerar vida y la corrutina "RegenerarVida"
        if (currentVidaPlayer < maxVidaPlayer && Input.GetKeyDown(KeyCode.G) && !RegenerateVida)
        {
            RegenerateVida = true;
            StartCoroutine(RegenerarVida());
        }

        // Si está activado el bool regenerarVida nuestra vida se sumara por el Time.DeltaTime * speedVidaRegeneracion
        if (RegenerateVida)
        {
            currentVidaPlayer += Time.deltaTime * speedVidaRegeneration;
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

    #endregion
}
