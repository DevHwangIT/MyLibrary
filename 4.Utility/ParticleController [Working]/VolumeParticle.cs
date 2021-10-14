using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.Utility
{
    public enum HideType
    {
        Destroy,
        Scaling,
        Enabled
    }

    public enum DestroyType
    {
        Distance,
        Time
    }
    
    public class VolumeParticle : MonoBehaviour
    {
        Vector3 lscale;
        public Action OnDestoyCall;
        
        void FixedUpdate()
        {
            
        }
    }
}
