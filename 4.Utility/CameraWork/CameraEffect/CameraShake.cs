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
            duration = EditorGUILayout.IntField("Duration : ",duration);
            direction = EditorGUILayout.Vector3Field("Direction : ",direction);
        }

        public override IEnumerator Action(Transform cam)
        {
            Debug.Log("코루틴 실행 중 10초간");
            yield return new WaitForSeconds(1f);
            Debug.Log("뭐야 되는거야 안되는거야");
            yield return new WaitForSeconds(1f);
            Debug.Log("코루틴 종료 됨");
            CamCoroutine = null;
        }
    }
}