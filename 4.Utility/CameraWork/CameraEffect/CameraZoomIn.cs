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
            duration = EditorGUILayout.IntField("Duration : ",duration);
            direction = EditorGUILayout.Vector3Field("Direction : ",direction);
            _curve = EditorGUILayout.CurveField("Zoom Curve : ",_curve);
        }

        public override IEnumerator Action(Transform cam)
        {
            float duration = 0f;
            while (duration < 5f)
            {
                cam.Translate(Vector3.up * 10 * Time.fixedDeltaTime);
                duration += Time.fixedDeltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            duration = 0f;
            while (duration < 5f)
            {
                cam.Translate(Vector3.down * 10 * Time.fixedDeltaTime);
                duration += Time.fixedDeltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            Debug.Log("코루틴 종료 됨");
            CamCoroutine = null;
        }
    }
}
