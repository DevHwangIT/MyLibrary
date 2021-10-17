using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MyLibrary.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary.Utility
{
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
            EditorGUILayout.Space(1f);
            EditorGUILayout.LabelField("Camera Type : ");
            camWorker.cameraType = (CameraWorkType) EditorGUILayout.EnumPopup(camWorker.cameraType);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5f);

            for (int index = 0; index < camWorker.CameraEffectLength; index++)
            {
                CameraEffect camEffect;
                if (camWorker.CameraEffectGetIndex(index, out camEffect))
                {
                    EditorGUILayout.Space(2.5f);
                    EditorGUILayout.LabelField(camEffect.ClassName);
                    EditorGUILayout.BeginVertical("Box");
                    camEffect.DrawInspectorGUI(serializedObject);

                    if (EditorApplication.isPlaying == true)
                    {
                        if (camEffect.isPlaying == false)
                        {
                            if (GUILayout.Button("Play"))
                            {
                                camWorker.Action(camEffect);
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("Stop"))
                            {
                                camWorker.Stop(camEffect);
                            }
                        }
                    }

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space(2.5f);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
