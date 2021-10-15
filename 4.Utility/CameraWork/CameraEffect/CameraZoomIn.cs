using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    public class CameraZoomIn : CameraEffect
    {
        private AnimationCurve _curve = new AnimationCurve();
        [SerializeField] private Transform Target;

        public CameraZoomIn(string name) : base(name)
        {
            _name = name;
            _curve = AnimationCurve.Linear(0, 0, 1, 1);
        }
        
        public override void DrawInspectorGUI()
        {
            base.DrawInspectorGUI();
            _curve = EditorGUILayout.CurveField("Zoom Curve : ",_curve);
        }

        public override void Action(Transform cam)
        {
            Debug.Log("Test Zoomin");
        }
    }
}
