using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float life = 100f;
    [SerializeField] private float Maxlife;

    void Awake()
    {
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
}
