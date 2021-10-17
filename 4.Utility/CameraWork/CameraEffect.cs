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
        public Coroutine CamCoroutine;
        public bool isPlaying => CamCoroutine != null ? true : false;
        public float duration { get; set; }
        protected CameraEffect(string name) { _name = name;}

        public abstract void DrawInspectorGUI(SerializedObject serializedObject);
        public abstract IEnumerator Action(Transform cam);
        public abstract void Stop(Transform cam);
    }
}
