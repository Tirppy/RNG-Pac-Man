using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WFCF))]
public class WFCEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WFCF wfc = (WFCF)target;

        if (GUILayout.Button("Generate Matrix"))
        {
            wfc.InitializeMatrix();
        }
    }
}
