using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    public class CameraShake : CameraEffect
    {
        public CameraShake(string name) : base(name) { _name = name;}

        public override void DrawInspectorGUI()
        {
            base.DrawInspectorGUI();
            
        }

        public override void Action(Transform cam)
        {
            Debug.Log("Test Shaking");
        }
    }
}