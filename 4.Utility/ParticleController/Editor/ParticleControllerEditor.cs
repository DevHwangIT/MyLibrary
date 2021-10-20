using System;
using System.Collections;
using System.Collections.Generic;
using MyLibrary.Utility;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Utility
{
    [CustomEditor(typeof(ParticleController))]
    public class ParticleControllerEditor : Editor
    {
        private ParticleController _controller;

        private void OnEnable()
        {
            _controller = (ParticleController) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Particle Property");
            
            _controller.HideType = (HideType) EditorGUILayout.EnumPopup("Hide Type : ", _controller.HideType);
            if (_controller.HideType == HideType.ObjectPooling)
            {
                EditorGUILayout.Space(5);
                SerializedProperty poolingCallEvent = serializedObject.FindProperty("onObjectPoolCalling");
                EditorGUIUtility.LookLikeControls();
                EditorGUILayout.PropertyField(poolingCallEvent);
                serializedObject.ApplyModifiedProperties();
            }
            
            _controller.destroyType = (DestroyCondition) EditorGUILayout.EnumPopup("Destory Type : ", _controller.destroyType);
            EditorGUILayout.Space(5);
            switch (_controller.destroyType)
            {
                case DestroyCondition.Distance:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Distance Compare Target : ");
                    _controller.distanceCompareTarget = (Transform) EditorGUILayout.ObjectField(_controller.distanceCompareTarget, typeof(Transform));
                    EditorGUILayout.EndHorizontal();
                    _controller.distanceValue = EditorGUILayout.FloatField("Destory Condition Distance : ", _controller.distanceValue);
                    break;

                case DestroyCondition.Time:
                    _controller.durationTime = EditorGUILayout.FloatField("Destory Condition Destruction Time : ", _controller.durationTime);
                    break;
            }
            EditorGUILayout.EndVertical();
        }
    }
}
