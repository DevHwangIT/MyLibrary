using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MyLibrary.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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

            if (EditorApplication.isPlaying == true)
            {
                if (camWorker.CameraEffects[index].isPlaying == false)
                {
                    if (GUILayout.Button("Play"))
                    {
                        camWorker.Action(camWorker.CameraEffects[index]);
                    }
                }
                else
                {
                    if (GUILayout.Button("Stop"))
                    {
                        camWorker.Stop(camWorker.CameraEffects[index]);
                    }
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
}
