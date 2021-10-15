using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MyLibrary.Utility;
using UnityEngine;

namespace MyLibrary.Utility
{
    [System.Serializable]
    public enum CameraWorkType
    {
        Unity_Camera,
        Cinemachine_Camera
    }

    [DisallowMultipleComponent, ExecuteInEditMode]
    public class CameraWorker : MonoBehaviour
    {
        public CameraWorkType cameraType;
        
        [Space(1)]
        public CameraEffect[] CameraEffects =
        {
            new CameraZoomIn("Zoom-In"),
            new CameraShake("Shake")
        };
        
        private void Awake()
        {
            if (Camera.main == null || this.gameObject != Camera.main.gameObject) 
                Destroy(this);
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            Camera mainCam = Camera.main;
            if (this.gameObject != mainCam.gameObject)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (this != null)
                    {
                        Debug.Log("Only MainCamera can have only one of these scripts.");
                        if (mainCam.transform.GetComponent<CameraWorker>() == null)
                            mainCam.gameObject.AddComponent<CameraWorker>();

                        if (GetComponent<CameraWorker>())
                            DestroyImmediate(this);
                    }
                };
            }
#endif
        }
        
        private T GetCameraEffect<T>() where T : CameraEffect
        {
            foreach (var effect in CameraEffects)
            {
                if (effect.GetType() == typeof(T))
                    return (T) effect;
            }
            return null;
        }

        public void Active<T>() where T : CameraEffect
        {
            //임시
            if (GetCameraEffect<T>() != null)
                GetCameraEffect<T>().Action(null);
        }
    }
}
