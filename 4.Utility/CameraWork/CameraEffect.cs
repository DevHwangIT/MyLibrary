using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Data.Util;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    [System.Serializable]
    public abstract class CameraEffect
    {
        protected string _name = "Camera Effect";
        public string ClassName => _name;
            
        protected int duration;
        protected Vector3 direction;

        protected CameraEffect(string name) { _name = name;}

        public virtual void DrawInspectorGUI()
        {
            duration = EditorGUILayout.IntField("Duration : ",duration);
            direction = EditorGUILayout.Vector3Field("Direction : ",direction);
        }
        
        public abstract void Action(Transform cam);
    }
}
