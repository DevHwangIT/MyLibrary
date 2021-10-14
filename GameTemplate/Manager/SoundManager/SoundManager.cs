using System;
using System.Collections;
using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioClipData))]
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

    private const float MinimumVolum = -40f;
    private const float MaximumVolum = 0f;

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

    public float SFXVolum
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

    private AudioClipData _clipData;
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

    public void PlayBGM(string clipName)
    {
        AudioClip bgmClip = _clipData.GetBGMClip(clipName);
        if (bgmClip != null)
        {
            _bgmAudioSource.Stop();
            _bgmAudioSource.clip = bgmClip;
            _bgmAudioSource.Play();
        }
    }

    public void PlayVFX(string clipName)
    {
        AudioClip sfxClip = _clipData.GetSfxClip(clipName);
        if (sfxClip != null)
        {
            //ObjectPool
        }
    }
    
    // TODO :생각해보자 어차피 파티클 자체에도 소리가 프리팹안에 붙어있을텐데 이게 필요한가?.. 그리고 사운드에 2d인지 3d인지 옵션도..?
    
    public void PlayVFX(string clipName, Vector3 Position)
    {
        AudioClip sfxClip = _clipData.GetSfxClip(clipName);
        if (sfxClip != null)
        {
            //ObjectPool
        }
    }
    
    public void PlayEnvironment(string clipName)
    {
        AudioClip environmentClip = _clipData.GetEnvironmentClip(clipName);
        if (environmentClip != null)
        {
            //ObjectPool
        }
    }
    
    public void PlayEnvironment(string clipName, Vector3 Position)
    {
        AudioClip sfxClip = _clipData.GetSfxClip(clipName);
        if (sfxClip != null)
        {
            //ObjectPool
        }
    }

}