using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeBomb : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject bombSmoke;
    [SerializeField] private GameObject spawnBombSmoke;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ThrowSmokeBomb();
    }

    #region - EnableSmokeBomb -

    private void ThrowSmokeBomb()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(waitToThrowBombSmoke());
        }
    }

    private IEnumerator waitToThrowBombSmoke()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(bombSmoke, spawnBombSmoke.transform.position, spawnBombSmoke.transform.rotation);
        StartCoroutine(waitToDesapearSmokeBomb());
    }
    
    private IEnumerator waitToDesapearSmokeBomb()
    {
        yield return new WaitForSeconds(10);
        bombSmoke.GetComponentsInChildren<VisualEffect>()[bombSmoke.GetComponentsInChildren<VisualEffect>().Length].pause = true;
    }
    
    #endregion
}
