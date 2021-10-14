using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;

    public string GetName => _name;
    public AudioClip GetClip => _clip;
}

[DisallowMultipleComponent]
public class AudioClipData : MonoBehaviour
{
    [Header("BGM")]
    [SetElementTitle("_name")]
    [SerializeField] private List<Sound> bgmSounds = new List<Sound>();
    
    [Header("VFX[Visual Effects]")]
    [SetElementTitle("_name")]
    [SerializeField] private List<Sound> sfxSounds = new List<Sound>();

    [Header("Environment")]
    [SetElementTitle("_name")]
    [SerializeField] private List<Sound> environmentSounds = new List<Sound>();
    
    public AudioClip GetBGMClip(string clipName)
    {
        foreach (var bgmSound in bgmSounds)
        {
            if (bgmSound.GetName.Equals(clipName))
                return bgmSound.GetClip;
        }
        return null;
    }
    
    public AudioClip GetSfxClip(string clipName)
    {
        foreach (var sfxSound in sfxSounds)
        {
            if (sfxSound.GetName.Equals(clipName))
                return sfxSound.GetClip;
        }
        return null;
    }
        
    public AudioClip GetEnvironmentClip(string clipName)
    {
        foreach (var environmentSound in environmentSounds)
        {
            if (environmentSound.GetName.Equals(clipName))
                return environmentSound.GetClip;
        }
        return null;
    }
}
