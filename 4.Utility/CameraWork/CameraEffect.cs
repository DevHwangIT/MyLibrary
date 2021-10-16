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
        
        protected int duration;
        protected Vector3 direction;

        protected CameraEffect(string name) { _name = name;}

        public abstract void DrawInspectorGUI();
        public abstract IEnumerator Action(Transform cam);
    }
}
