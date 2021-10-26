using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YesOrNoBoxUI : ModalBoxUI
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    
    new void Awake()
    {
        base.Awake();
    }
    
    public new void Initialize(string title, string info, Action onYesButtonClick = null, Action onNoButtonClick = null)
    {
        titleText.text = title;
        infoText.text = info;
        
        yesButton.onClick.RemoveAllListeners();
        if (onYesButtonClick != null)
        {
            yesButton.onClick.AddListener(new UnityAction(onYesButtonClick));
        }

        yesButton.onClick.RemoveAllListeners();
        if (onNoButtonClick != null)
        {
            yesButton.onClick.AddListener(new UnityAction(onNoButtonClick));
        }
    }
}
