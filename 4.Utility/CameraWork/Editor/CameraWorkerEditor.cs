using System.Collections;
using System.Collections.Generic;
using MyLibrary.Utility;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraWorker))]
public class CameraWorkerEditor : Editor
{
    private CameraWorker camWorker;
    
    void OnEnable()
    {
        camWorker = (CameraWorker) target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Camera Type : ");
        camWorker.cameraType = (CameraWorkType) EditorGUILayout.EnumPopup(camWorker.cameraType);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(1);
        
        for (int index = 0; index < camWorker.CameraEffects.Length; index++)
        {
            EditorGUILayout.LabelField(camWorker.CameraEffects[index].ClassName);
            EditorGUILayout.BeginVertical("Box");
            camWorker.CameraEffects[index].DrawInspectorGUI();
            EditorGUILayout.EndVertical();
        }
    }
}
