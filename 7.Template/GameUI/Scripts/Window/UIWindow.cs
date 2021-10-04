using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum UIWindowID : int
{
	None = 0,
	Custom = 1,
	Settings = 2,
	GameMenu = 3,
	ModalBox = 4,
	Character = 5,
	Inventory = 6,
	SpellBook = 7,
	Dialog = 8
}

[DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(CanvasGroup))]
public class UIWindow : MonoBehaviour, IEventSystemHandler, ISelectHandler, IPointerDownHandler
{
	protected static UIWindow m_FucusedWindow;
	public static UIWindow FocusedWindow => m_FucusedWindow;

	[SerializeField] private UIWindowID m_WindowId = UIWindowID.None;
	protected bool m_IsFocused = false;
	private CanvasGroup m_CanvasGroup;

	public UIWindowID ID
	{
		get { return this.m_WindowId; }
		set { this.m_WindowId = value; }
	}

	public bool IsVisible
	{
		get { return (this.m_CanvasGroup != null && this.m_CanvasGroup.alpha > 0f) ? true : false; }
	}

	public bool IsOpen
	{
		get { return true; }
	}

	public bool IsFocused
	{
		get { return this.m_IsFocused; }
	}

	protected virtual void Awake()
	{
		this.m_CanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
	}

	protected virtual bool IsActive()
	{
		return (this.enabled && this.gameObject.activeInHierarchy);
	}

	public virtual void OnSelect(BaseEventData eventData)
	{
		this.Focus();
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		this.Focus();
	}

	public virtual void Focus()
	{
		if (this.m_IsFocused)
			return;

		this.m_IsFocused = true;

		UIWindow.OnBeforeFocusWindow(this);
	}

	public virtual void Show()
	{
		this.Show(false);
	}

	public virtual void Show(bool instant)
	{
		if (!this.IsActive())
			return;
		this.Focus();
	}

	public virtual void Hide()
	{
		this.Hide(false);
	}

	public virtual void Hide(bool instant)
	{
		if (!this.IsActive())
			return;
	}

	#region Static Methods

	public static List<UIWindow> GetWindows()
	{
		List<UIWindow> windows = new List<UIWindow>();

		UIWindow[] ws = Resources.FindObjectsOfTypeAll<UIWindow>();

		foreach (UIWindow w in ws)
		{
			if (w.gameObject.activeInHierarchy)
				windows.Add(w);
		}

		return windows;
	}

	public static UIWindow GetWindow(UIWindowID id)
	{
		foreach (UIWindow window in UIWindow.GetWindows())
			if (window.ID == id)
				return window;

		return null;
	}

	public static void FocusWindow(UIWindowID id)
	{
		if (UIWindow.GetWindow(id) != null)
			UIWindow.GetWindow(id).Focus();
	}

	protected static void OnBeforeFocusWindow(UIWindow window)
	{
		if (m_FucusedWindow != null)
			m_FucusedWindow.m_IsFocused = false;

		m_FucusedWindow = window;
	}

	#endregion
}
