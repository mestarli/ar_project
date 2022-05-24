using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    public float life = 100f;
    public float Maxlife;

    void Awake()
    {
        Instance = this;
        Maxlife = life;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Finish")
        {
            SceneManager.LoadScene("YouWin");
        }
        if (other.gameObject.tag =="exit")
        {
            //SceneManager.LoadScene("YouWin");
        }
    }
    
    public void RestarVida(float restar_vida)
    {
        life -= restar_vida;
        AudioManager.instance.PlaySong("Daño");
        UIManager.Instance.UpdateLife(life, Maxlife);
        if (life <= 0)
        {
            
           // _animator.SetTrigger("Damage");
            StartCoroutine(courotineShowGameOver());
        }
        else
        {
            //_animator.SetTrigger("Damage");
        }
    }
    public void SumarVida(float sumar_vida)
    {
        life += sumar_vida;
        UIManager.Instance.UpdateLife(life, Maxlife);
    }
    IEnumerator courotineShowGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameOver");
    }
}
