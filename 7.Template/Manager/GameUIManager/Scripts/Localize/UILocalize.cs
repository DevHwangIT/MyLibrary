﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public enum TranslationCountries
{
    Korean,
    English,
    Japanese,
    Chinese
}

[RequireComponent(typeof(Text)), DisallowMultipleComponent]
public class UILocalize : MonoBehaviour
{
    private Text _text;
    [SerializeField] private string _key = "";

    private static int _keyIndex = 0;
    private static Dictionary<string, int> _keyDictionary = new Dictionary<string, int>();
    private static List<Dictionary<string, string>> _localizeData;
    
    private void Awake()
    {
        _text = this.GetComponent<Text>();
        _text.text = Get(_key);
    }

    private static void LoadData()
    {
        if (_localizeData == null) 
            _localizeData = CSVRead("Assets/MyLibrary/7.Template/Manager/GameUIManager/Resources/Localize.csv");
    }
    
    public static bool SetRegion(string region)
    {
        LoadData();
        if (_keyDictionary.TryGetValue(region, out _keyIndex))
            return true;
        else
            return false;
    }
    public static bool SetCountries(TranslationCountries region)
    {
        LoadData();
        if (_keyDictionary.TryGetValue(region.ToString(), out _keyIndex))
            return true;
        else
            return false;
    }
    
    public static string Get(string key)
    {
        LoadData();
        string value;
        if (_localizeData[_keyIndex].TryGetValue(key, out value))
            return value;
        else
            return "";
    }

    public static List<Dictionary<string, string>> CSVRead(string file)
    {
        var localizeDictionaryList = new List<Dictionary<string, string>>();
        TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(file);

        string firstLine = data.text.Split('\n')[0];
        string[] KeyList = firstLine.Split(',');
        
        _keyDictionary.Clear();
        for (int i = 1; i < KeyList.Length; i++)
        {
            localizeDictionaryList.Add(new Dictionary<string, string>());
            _keyDictionary.Add(KeyList[i], i - 1);
        }

        string[] lineText = data.text.Split('\n');
        for (int index = 1; index < lineText.Length; index++)
        {
            string[] localizeText = lineText[index].Split(',');
            for (int keyIndex = 1; keyIndex < localizeText.Length; keyIndex++)
            {
                localizeDictionaryList[keyIndex - 1].Add(localizeText[0], localizeText[keyIndex]);
            }
        }
        return localizeDictionaryList;
    }
}
