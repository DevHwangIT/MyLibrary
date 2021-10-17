using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyLibrary.Utility
{
    [CreateAssetMenu]
    public class CameraEffectsData : ScriptableObject
    {
        public CameraZoomIn ZoomIn = new CameraZoomIn("Zoom-In");
        public CameraShake Shake = new CameraShake("Shake");

        public List<CameraEffect> GetArrayEffect()
        {
            List<CameraEffect> effects = new List<CameraEffect>();
            effects.Add(ZoomIn);
            effects.Add(Shake);
            
            return effects;
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////
        
        public CameraEffect GetCameraEffect<T>() where T : CameraEffect
        {
            foreach (var effect in GetArrayEffect())
            {
                if (effect.GetType() == typeof(T))
                    return effect;
            }
            return null;
        }

        public CameraEffect GetCameraEffect(CameraEffect type)
        {
            foreach (var effect in GetArrayEffect())
            {
                if (effect.GetType() == type.GetType())
                    return effect;
            }
            return null;
        }
    }
}