using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OkBoxUI : ModalBoxUI
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Button okButton;
    
    new void Awake()
    {
        base.Awake();
    } 
    
    public new void Initialize(string title, string info, Action onOkButtonClick = null)
    {
        titleText.text = title;
        infoText.text = info;
        
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(new UnityAction(Hide));
        if (onOkButtonClick != null)
        {
            okButton.onClick.AddListener(new UnityAction(onOkButtonClick));
        }
    }
}
