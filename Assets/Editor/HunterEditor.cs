using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hunter))]
public class HunterEditor : Editor
{
    private void OnSceneGUI()
    {
        Hunter fow = (Hunter)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.range);
    }
}
