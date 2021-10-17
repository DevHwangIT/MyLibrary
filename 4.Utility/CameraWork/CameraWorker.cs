using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using MyLibrary.Utility;
using Unity.Collections;
using UnityEngine;

namespace MyLibrary.Utility
{
    [System.Serializable]
    public enum CameraWorkType
    {
        Unity_Camera,
        Cinemachine_Camera
    }

    [DisallowMultipleComponent]
    public class CameraWorker : MonoBehaviour
    {
        public CameraWorkType cameraType;

        public Transform GetCamera
        {
            get
            {
                switch (cameraType)
                {
                    case CameraWorkType.Cinemachine_Camera:
                        CinemachineBrain camBrain = FindObjectOfType<CinemachineBrain>();
                        if (camBrain.ActiveVirtualCamera as CinemachineVirtualCamera)
                            return (camBrain.ActiveVirtualCamera as CinemachineVirtualCamera).transform;
                        else
                            break;

                    case CameraWorkType.Unity_Camera:
                        if (Camera.main != null)
                            return Camera.main.transform;
                        else
                            break;
                }
#if UNITY_EDITOR
                Debug.LogWarning("Please Check the camera type or camera object");
#endif
                return null;
            }
        }

        public CameraEffect[] cameraEffects = 
        {
            new CameraZoomIn("Zoom-In"),
            new CameraShake("Shake")
        };
        
        public int CameraEffectLength { get { return cameraEffects.Length; } }
        public bool CameraEffectGetIndex(int index, out CameraEffect effect)
        {
            if (cameraEffects.Length > index)
            {
                effect = cameraEffects[index];
                return true;
            }
            effect = null;
            return false;
        }
        
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

                        if (GetComponent<CameraWorker>() && mainCam.transform != this.transform)
                            DestroyImmediate(this);
                    }
                };
            }
#endif
        }
        
        public T GetCameraEffect<T>() where T : CameraEffect
        {
            foreach (var effect in cameraEffects)
            {
                if (effect.GetType() == typeof(T))
                    return (T) effect;
            }
            return null;
        }

        public void Action<T>() where T : CameraEffect
        {
            if (GetCameraEffect<T>() != null)
            {
                T t = GetCameraEffect<T>();
                t.CamCoroutine = StartCoroutine(t.Action(GetCamera));
            }
        }

        public void Action(CameraEffect type)
        {
            foreach (var effect in cameraEffects)
            {
                if (effect.GetType() == type.GetType())
                {
                    effect.CamCoroutine = StartCoroutine(effect.Action(GetCamera));
                    break;
                }
            }
        }
        
        public void Stop<T>() where T : CameraEffect
        {
            if (GetCameraEffect<T>() != null)
            {
                T effect = GetCameraEffect<T>();
                if (effect.isPlaying)
                {
                    StopCoroutine(effect.CamCoroutine);
                    effect.CamCoroutine = null;
                    effect.Stop(GetCamera);
                }
            }
        }
        
        public void Stop(CameraEffect type)
        {
            foreach (var effect in cameraEffects)
            {
                if (effect.GetType() == type.GetType())
                {
                    if (effect.isPlaying)
                    {
                        StopCoroutine(effect.CamCoroutine);
                        effect.CamCoroutine = null;
                        effect.Stop(GetCamera);
                        return;
                    }
                }
            }
        }
    }
}
