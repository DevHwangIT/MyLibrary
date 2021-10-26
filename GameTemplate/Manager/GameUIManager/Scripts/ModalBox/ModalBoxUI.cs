using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class ModalBoxUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    protected CanvasGroup GetCanvasGroup
    {
        get
        {
            if (canvasGroup == null)
                canvasGroup = this.GetComponent<CanvasGroup>();
            return canvasGroup;
        }
    }
    
    protected void SetParent()
    {
        transform.SetParent(UIModalBoxManager.Instance.transform);
    }

    public bool IsActive { get { return (this.enabled && this.gameObject.activeInHierarchy); } }
    
    protected void Awake()
    {
        SetParent();
    }

    public virtual void Initialize()
    {
        SetParent();
    }
    
    public void Show()
    {
        GetCanvasGroup.alpha = 1;
        GetCanvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        GetCanvasGroup.alpha = 0;
        GetCanvasGroup.blocksRaycasts = false;
    }

    private static List<ModalBoxUI> GetActiveModalBoxes()
    {
        List<ModalBoxUI> modalBoxes = new List<ModalBoxUI>();
        modalBoxes.AddRange(FindObjectsOfType<ModalBoxUI>());
        return modalBoxes;
    }

    public static T GetNotActiveModalBoxInHierarchyView<T>() where T : ModalBoxUI
    {
        foreach (ModalBoxUI mBox in ModalBoxUI.GetActiveModalBoxes())
            if (mBox.IsActive == false && mBox.GetType() == typeof(T))
                return (T) mBox;
        return null;
    }
    
    public static ModalBoxUI GetNotActiveModalBoxInHierarchyView(ModalBoxUI type)
    {
        foreach (ModalBoxUI mBox in ModalBoxUI.GetActiveModalBoxes())
            if (mBox.IsActive == false && mBox.GetType() == type.GetType())
                return mBox;
        return null;
    }
}
