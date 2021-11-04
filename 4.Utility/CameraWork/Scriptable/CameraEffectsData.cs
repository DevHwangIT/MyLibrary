using System.Collections.Generic;
using UnityEngine;


namespace MyLibrary.Utility
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MyLibrary/Camera ScriptableObject")]
    public class CameraEffectsData : ScriptableObject
    {
        public CameraFocusing Focusing = new CameraFocusing("Focusing");
        public CameraShake Shake = new CameraShake("Shake");
        
        public List<CameraEffect> GetArrayEffect()
        {
            // TODO : If possible in the future, use system.Reflection to convert class member fields to a list of cameraeffect class.
            
            List<CameraEffect> effects = new List<CameraEffect>();
            effects.Add(Focusing);
            effects.Add(Shake);
            
            return effects;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
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