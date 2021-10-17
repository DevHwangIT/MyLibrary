using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    [System.Serializable]
    public class CameraShake : CameraEffect
    {
        public bool isCompleteBackInitPos { get; set; }
        
        #region Editor Variable
        private bool isInfinity = false;
        #endregion
        
        public CameraShake(string name) : base(name) { _name = name;}

        public override void DrawInspectorGUI()
        {
            isCompleteBackInitPos = EditorGUILayout.Toggle("return after work is completed?", isCompleteBackInitPos);

            isInfinity = EditorGUILayout.Toggle("Duration is infinity?", isInfinity);
            if (isInfinity == false)
            {
                duration = 0;
                duration = EditorGUILayout.FloatField("Duration : ", duration);
            }
            else
            {
                duration = Mathf.Infinity;
            }
            
        }

        public override IEnumerator Action(Transform cam)
        {
            yield return new WaitForSeconds(1f);
            CamCoroutine = null;
        }

        public override void Stop(Transform cam)
        {
            
        }
    }
}