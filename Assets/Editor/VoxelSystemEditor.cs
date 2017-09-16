using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VoxelSystem))]
public class VoxelSystemEditor : Editor {

    public override void OnInspectorGUI()
    {
        VoxelSystem t = (VoxelSystem)target;

        if(base.DrawDefaultInspector() && t.m_autoUpdate && Application.isPlaying)
        {
            t.ThreadDrawAll();
        }

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Generate"))
            {
                t.Generate();
            }

            if (GUILayout.Button("Update"))
            {
                t.ThreadDrawAll();
            }
        }
    }
	
}
