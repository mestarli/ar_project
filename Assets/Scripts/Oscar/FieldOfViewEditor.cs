using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]

public class FieldOfViewEditor : Editor
{
    //ESTE SCRIPT SE ENCARGA DE DIBUJAR LOS GIZMOS EN EL EDITOR, TODO ES VISUAL Y NO SE HA DE METER EN EL GAMEOBJECT
    private void OnSceneGUI()
    {
        //Llama al script field of view y lo guarda como variable temporal
        FieldOfView fow = (FieldOfView)target;
        //Esto dibuja el circulo
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.awarenedViewRadius);
        //Esto dibuja las dos lineas que hacen el angulo de vision
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        Vector3 viewAngleC = fow.DirFromAngle(-fow.awarenedViewAngle / 2, false);
        Vector3 viewAngleD = fow.DirFromAngle(fow.awarenedViewAngle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleC * fow.awarenedViewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleD * fow.awarenedViewRadius);
    }
}
