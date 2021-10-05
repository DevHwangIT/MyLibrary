using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text)), DisallowMultipleComponent]
public class UILocalize : MonoBehaviour
{
    private Text _text;
    
    private void Awake()
    {
        _text = this.GetComponent<Text>();
    }
}
