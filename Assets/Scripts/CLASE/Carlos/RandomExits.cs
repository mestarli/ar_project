using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomExits : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject[] exits;

    private void Start()
    {
        exits = GameObject.FindGameObjectsWithTag("Exit");
        exits[Random.Range(0, exits.Length)].SetActive(false);
    }
}
