using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;
    [SerializeField] private TMP_Text textCooldown;

    [Header("VARIABLES")] 
    private bool isCooldown = false;
    public float cooldownTime = 10f;
    private float cooldownTimer = 0f;

    [Header("SCROLL SKILLS")]
    public float SkillSelected;

    // Start is called before the first frame update
    void Start()
    {
        SkillSelected = 1;
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isCooldown)
        {
            ApplyCooldown();
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            SkillSelected += (Input.GetAxis("Mouse ScrollWheel")) * 10;
            if (SkillSelected < 1)
            {
                SkillSelected = 1;
            }
            if (SkillSelected > 3)
            {
                SkillSelected = 3;
            }
            //Mathf.Clamp(SkillSelected, 1f, 3f);
        }
        
        // Dependiendo de la velocidad puedes obtener valores desde 1 hasta 3 (positivos y negativos).
    }

    private void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0f;
            PlayerMovement.instance.SkillActive = false;
            PlayerMovement.instance._animator.ResetTrigger("Clone");
            PlayerMovement.instance._animator.SetTrigger("Default");
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseSpell()
    {
        if (isCooldown)
        {
            //Click del spell mientras está en cooldown (poner algun sonido)
        }
        else
        {
            isCooldown = true;
            textCooldown.gameObject.SetActive(true);
            cooldownTimer = cooldownTime;
            PlayerMovement.instance.SkillActive = true;
        }
    }
}
