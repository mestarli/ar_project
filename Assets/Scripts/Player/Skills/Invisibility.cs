using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public KeyCode control;
    public Material handsMat;
    public Material invisibilityMat;
    public GameObject HandsGameObject;
    public int invisibilityTime = 12;
    private bool isInvis = false;


    // FALTA APLICAR LOGICA DE COOLDOWN
    // Start is called before the first frame update
    void Start()
    {
        if (LoadSkill.Instance.EnableSkill_01.GetType().ToString() == "Invisibility")
        {
            control = KeyCode.E;
        }else if (LoadSkill.Instance.EnableSkill_02.GetType().ToString() == "Invisibility")
        {
            control = KeyCode.R;
        }
        else if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() ==  "Invisibility")
        {
            control = KeyCode.T;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(control) && !PlayerMovement.instance.SkillActive)
        {
            if (!isInvis)
            {
                StartCoroutine(CorroutineInvisibility());
            }
        }
    }

    IEnumerator CorroutineInvisibility()
    {
        isInvis = true;
        HandsGameObject.GetComponent<SkinnedMeshRenderer>().material = invisibilityMat;
        gameObject.layer= 0;
        // APLICAR LOGICA PARA QUE DEJEN DE DETECTARTE LOS ENEMIES
        yield return new WaitForSeconds(invisibilityTime);
        HandsGameObject.GetComponent<SkinnedMeshRenderer>().material = handsMat;
        gameObject.layer= 6;
        // DESAPLICAR LOGICA PARA QUE DEJEN DE DETECTARTE LOS ENEMIES
        isInvis = false;
        
    }
}
