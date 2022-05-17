using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeBomb : MonoBehaviour
{
    // Variables
    [Header("FUNCIONES BOMBA HUMO")]
    [SerializeField] private GameObject bombSmoke;
    [SerializeField] private GameObject spawnBombSmoke;
    [SerializeField] private bool canSpawnSmoke;
    [SerializeField] private List<GameObject> smokesEffects;
    [SerializeField] private VisualEffect[] smokeEffectsChildren;
    
    public KeyCode control;


    // Start is called before the first frame update
    void Start()
    {
        canSpawnSmoke = true;
        if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() == "SmokeBomb")
        {
            control = KeyCode.T;
        }else if (LoadSkill.Instance.EnableSkill_02.GetType().ToString() == "SmokeBomb")
        {
            control = KeyCode.R; 
        }
        else if (LoadSkill.Instance.EnableSkill_03.GetType().ToString() ==  "SmokeBomb")
        {
            control = KeyCode.E; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        ThrowSmokeBomb();
    }

    #region - EnableSmokeBomb -

    private void ThrowSmokeBomb()
    {
        // Si presionamos la tecla R y podemos spawnear el smoke iniciaremos la corrutina
        if (Input.GetKeyDown(control) && canSpawnSmoke)
        {
            canSpawnSmoke = false;
            StartCoroutine(waitToThrowBombSmoke());
        }
    }

    private IEnumerator waitToThrowBombSmoke()
    {
        // Borramos lo que haya en la lista de smokes y esperamos 0.7s para lanzar la bomba de humo, añadimos ese prefab spawneado a la lista de smokes e
        // iniciamos la corrutina para desvanecer el humo
        smokesEffects.Clear();
        yield return new WaitForSeconds(0.7f);
        var ParticleEffect = Instantiate(bombSmoke, spawnBombSmoke.transform.position, spawnBombSmoke.transform.rotation);
        smokesEffects.Add(ParticleEffect);
        StartCoroutine(waitToDesapearSmokeBomb());
    }
    
    private IEnumerator waitToDesapearSmokeBomb()
    {
        // Añadimos los visualEffects de humo que tienen los hijos del prefab de la lista smokesEffects a la lista smokeEffectsChildren y esperamos
        // 10s para borrar la lista de smokes, después podremos volver a spawnear otra bomba de humo y se desvanecerá la anterior bomba lanzada
        smokeEffectsChildren = smokesEffects[0].GetComponentsInChildren<VisualEffect>();
        yield return new WaitForSeconds(10);
        smokesEffects.Clear();
        canSpawnSmoke = true;   
        foreach (var visualEffect in smokeEffectsChildren)
        {
            visualEffect.Stop();
        }
    }
    
    #endregion
}
