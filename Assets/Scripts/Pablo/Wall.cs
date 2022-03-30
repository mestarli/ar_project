using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Wall : MonoBehaviour
{

    public Camera cam;
    public float detectRange = 20;
    public bool inRange;
    private Vector3 destination;
    private Quaternion rotation;
    

    [Space] [Header("PARAMETROS DE CONSTRUCCION DEL MURO")]

    public GameObject PosicionadorDeMuro;
    private bool PosicionadorDeMuroActivo;
    private bool wallBuilding;
    public Object CuboIndividual;
    private int wallCubeAmount;
    private int wallCubeDistance;
    public float wallDuration;
    public float updateRate;
    

    void Start()
    {
        // Al empezar el juego el posicionador desactivado.
        PosicionadorDeMuro.SetActive(false);
        Debug.Log("Presionar C para activar el posicionador de muro");
        Debug.Log("Presionar E para desactivar el posicionador de muro");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PosicionadorDeMuroActivo = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PosicionadorDeMuroActivo = false;
        }

        if (PosicionadorDeMuroActivo)
        {
            ActualizarPosicionador();
        }
        else
        {
            // Desactivamos el objeto si no está activo el bool del posicionador.
            PosicionadorDeMuro.SetActive(false);
        }

        // Sí el marker está activo, presionamos click izquierdo, estamos en el rango designado y NO se está ya construyendo el muro, lo creamos.
        if (Input.GetButtonDown("Fire1") && inRange && !wallBuilding)
        {
            BuildWall();
        }
    }
    
    private void ActualizarPosicionador()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectRange))
        {
            inRange = true;
            destination = hit.point;
            rotation = Quaternion.LookRotation(ray.direction);
        }
        else
        {
            inRange = false;
        }

        if (inRange)
        {
            PosicionadorDeMuro.transform.position = destination;
            rotation = new Quaternion(PosicionadorDeMuro.transform.rotation.x, rotation.y,
                PosicionadorDeMuro.transform.rotation.z, PosicionadorDeMuro.transform.rotation.w);
            PosicionadorDeMuro.transform.rotation = rotation;
            PosicionadorDeMuro.SetActive(true);
        }
        else
        {
            PosicionadorDeMuro.SetActive(false);
        }
    }
    
    private void BuildWall()
    {
        wallBuilding = true;
        PosicionadorDeMuroActivo = false;
        GameObject muro = new GameObject();
        muro.transform.position = destination;
        muro.name = "Muro";

        List<Material> cubeMaterials = new List<Material>();
        for (int i = 0; i < wallCubeAmount; i++)
        {
            var cube = Instantiate(CuboIndividual, destination + new Vector3(i * wallCubeDistance, 0, 0),
                Quaternion.identity) as GameObject; // Spawneamos 1 Cubo.
            cube.transform.SetParent(muro.transform); // Seteamos los cubos como hijos del muro.
            cubeMaterials.Add(cube.transform.GetChild(0).GetComponent<MeshRenderer>().material); // TO DO: Gestionamos los materiales para el crack 
        }

        muro.transform.rotation = rotation;
        muro.transform.Translate(new Vector3(-(int)(wallCubeAmount/2)*wallCubeDistance, 0,0), Space.Self);

        wallBuilding = false;

        StartCoroutine(DestroyWall(muro, cubeMaterials));
    }

    IEnumerator DestroyWall(GameObject wallToDestroy, List<Material> materials)
    {
        float duration = wallDuration;
        float cracksAmount = 0;

        while (duration > 0)
        {
            duration -= updateRate;

            if (cracksAmount < 1)
            {
                cracksAmount += 1 / ((wallDuration - 0.2f) / updateRate);
                for (int i = 0; i < materials.Count; i++)
                {
                    materials[i].SetFloat("CracksAmount_", cracksAmount);
                }
            }

            yield return new WaitForSeconds(updateRate);

            if (duration <= 0)
            {
                Destroy(wallToDestroy);
            }
        }
    }
}
