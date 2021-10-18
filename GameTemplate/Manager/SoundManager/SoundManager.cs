using System;
using System.Collections;
using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

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

    private AudioMixer audioMixer;

    private AudioMixer GetMixer
    {
        get
        {
            if (audioMixer == null)
            {
                audioMixer =
                    (AudioMixer) AssetDatabase.LoadAssetAtPath(
                        "Assets/MyLibrary/GameTemplate/Manager/SoundManager/GameSound.mixer", typeof(AudioMixer));
                if (audioMixer == null)
                {
                    Debug.LogError("Null Exception!! - Please Check the AudioMixer Path");
                    return null;
                }
            }

            return audioMixer;
        }
    }

    private const float MuteVolume = -80;
    private const float MinimumVolum = -40;
    private const float MaximumVolum = 0;

    public float Volum
    {
        set
        {
            float volum = Mathf.Clamp(value, 0f, 1f);
            AudioListener.volume = volum;
        }
        get
        {
            return AudioListener.volume;
        }
    }
    
    public float MasterVolum
    {
        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("Master", MuteVolume);
            else
                GetMixer.SetFloat("Master", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("Master", out volum);
            return volum;
        }
    }

    public float BGMVolum
    {
        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("BGM", MuteVolume);
            else
                GetMixer.SetFloat("BGM", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("BGM", out volum);
            return volum;
        }
    }

    public float SFXVolum
    {
        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("SFX", MuteVolume);
            else
                GetMixer.SetFloat("SFX", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("SFX", out volum);
            return volum;
        }
    }

    public float UIVolum
    {

        set
        {
            float volum = value;
            volum = Mathf.Clamp(volum, 0f, 1f);
            volum = Mathf.Lerp(MinimumVolum, MaximumVolum, volum);
            if ((int) volum == MinimumVolum)
                GetMixer.SetFloat("UI", MuteVolume);
            else
                GetMixer.SetFloat("UI", volum);
        }
        get
        {
            float volum = 0f;
            GetMixer.GetFloat("UI", out volum);
            return volum;
        }
    }

    [SerializeField] private AudioClipData _bgmClipData;
    [SerializeField] private AudioClipData _sfxClipData;
    private AudioSource _bgmAudioSource;

    private void Awake()
    {
        GameObject audioClipDataParent = new GameObject();
        audioClipDataParent.transform.parent = this.transform;
        audioClipDataParent.gameObject.name = "Sound Controller";

        GameObject BGMAudioSource = new GameObject();
        BGMAudioSource.transform.parent = audioClipDataParent.transform;
        BGMAudioSource.gameObject.name = "BGM";
        _bgmAudioSource = BGMAudioSource.AddComponent<AudioSource>();
        _bgmAudioSource.outputAudioMixerGroup = GetMixer.FindMatchingGroups("Master")[1];

        _bgmAudioSource.playOnAwake = true;
    }

    public void PlayBGMSound(string clipName)
    {
        AudioClip bgmClip = _bgmClipData.GetClip(clipName);
        if (bgmClip != null)
        {
            _bgmAudioSource.Stop();
            _bgmAudioSource.clip = bgmClip;
            _bgmAudioSource.Play();
        }
    }

    public void PlayVFXSound(string clipName)
    {
        AudioClip sfxObj = _sfxClipData.GetClip(clipName);
        if (sfxObj != null)
        {
            //ObjectPool
        }
    }
    
    public void PlayVFXSound(string clipName, Vector3 Position)
    {
        AudioClip sfxObj = _sfxClipData.GetClip(clipName);
        if (sfxObj != null)
        {
            //ObjectPool
        }
    }
}