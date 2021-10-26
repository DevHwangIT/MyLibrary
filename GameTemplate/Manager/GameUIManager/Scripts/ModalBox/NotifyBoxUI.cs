using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyBoxUI : ModalBoxUI
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    private float showDuration = 1f;
    
    new void Awake()
    {
        base.Awake();
    }
    
    public new void Initialize(string title, string info, float duration)
    {
        titleText.text = title;
        infoText.text = info;
        showDuration = duration;
    }
    
    public new void Show()
    {
        base.Show();
        Invoke(nameof(Hide), showDuration);
    }
}
