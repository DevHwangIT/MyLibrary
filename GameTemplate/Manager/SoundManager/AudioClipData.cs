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

[CreateAssetMenu(fileName = "AudioClipData", menuName = "ScriptableObjects/AudioClipData", order = 11)]
public class AudioClipData : ScriptableObject
{
    [SetElementTitle("_name")]
    [SerializeField] private List<Sound> Sounds = new List<Sound>();
    
    public AudioClip GetClip(string clipName)
    {
        foreach (var Sound in Sounds)
        {
            if (Sound.GetName.Equals(clipName))
                return Sound.GetClip;
        }
        return null;
    }
}
