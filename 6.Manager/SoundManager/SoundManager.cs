using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace MyLibrary.Manager
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (SoundManager) FindObjectOfType(typeof(SoundManager));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(SoundManager)} (Singleton)");
                        _instance = singletonObject.AddComponent<SoundManager>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
        #endregion
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
        private AudioMixer audioMixer;
        private AudioMixer GetMixer
        {
            get
            {
                if (audioMixer == null)
                {
                    audioMixer = (AudioMixer) AssetDatabase.LoadAssetAtPath("Assets/MyLibrary/6.Manager/SoundManager/GameSound.mixer", typeof(AudioMixer));
                    if (audioMixer == null)
                    {
                        Debug.LogError("Null Exception!! - Please Check the AudioMixer Path");
                        return null;
                    }
                }
                return audioMixer;
            }
        }

        public const float MinimumVolum = -40f;
        public const float MaximumVolum = 0f;
        
        public float MasterVolum
        {
            set
            {
                float volum = value;
                volum -= MinimumVolum;
                volum = Mathf.Clamp(volum, MinimumVolum, MaximumVolum);
                GetMixer.SetFloat("Master", volum);
            }
            get
            {
                float volum = 0f;
                GetMixer.GetFloat("Master", out volum);
                volum += MinimumVolum;
                volum = Mathf.Clamp(volum, 0, 100f);
                return volum;
            }
        }

        public float BGMVolum
        {
            set
            {
                float volum = value;
                volum -= MinimumVolum;
                volum = Mathf.Clamp(volum, MinimumVolum, MaximumVolum);
                GetMixer.SetFloat("BackGround", volum);
            }
            get
            {
                float volum = 0f;
                GetMixer.GetFloat("BackGround", out volum);
                volum += MinimumVolum;
                volum = Mathf.Clamp(volum, 0, 100f);
                return volum;
            }
        }
        
        public float EffectVolum
        {
            set
            {
                float volum = value;
                volum -= MinimumVolum;
                volum = Mathf.Clamp(volum, MinimumVolum, MaximumVolum);
                GetMixer.SetFloat("Effect", volum);
            }
            get
            {
                float volum = 0f;
                GetMixer.GetFloat("Effect", out volum);
                volum += MinimumVolum;
                volum = Mathf.Clamp(volum, 0, 100f);
                return volum;
            }
        }
        
        public float EnvironmentVolum
        {
            set
            {
                float volum = value;
                volum -= MinimumVolum;
                volum = Mathf.Clamp(volum, MinimumVolum, MaximumVolum);
                GetMixer.SetFloat("Environment", volum);
            }
            get
            {
                float volum = 0f;
                GetMixer.GetFloat("Environment", out volum);
                volum += MinimumVolum;
                volum = Mathf.Clamp(volum, 0, 100f);
                return volum;
            }
        }
    }
}
