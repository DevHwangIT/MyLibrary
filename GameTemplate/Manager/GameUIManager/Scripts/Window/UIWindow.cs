using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent, ExecuteInEditMode, RequireComponent(typeof(CanvasGroup))]
public abstract class UIWindow : MonoBehaviour, IEventSystemHandler, ISelectHandler, IPointerDownHandler
{
	protected static UIWindow _FucusedWindow;
	public static UIWindow FocusedWindow => _FucusedWindow;
	protected bool _IsFocused = false;
	private CanvasGroup _CanvasGroup;
	public CanvasGroup CanvasGroup 
	{
		get
		{
			if (_CanvasGroup == null)
				_CanvasGroup = this.GetComponent<CanvasGroup>();
			return _CanvasGroup;
		}
	}

	[SerializeField] private bool isNotHideWithCancelInput;
	public bool NotHideWithCancelInput => isNotHideWithCancelInput;

	[SerializeField] protected int _priority;
	
	public string Name
	{
		get { return this.GetType().ToString(); }
	}

	public int Priority
	{
		get { return _priority;}
		set { _priority = value; }
	}

	public bool IsVisible
	{
		get { return (this._CanvasGroup != null && this._CanvasGroup.alpha > 0f) ? true : false; }
	}

	public bool IsFocused
	{
		get { return this._IsFocused; }
	}

	protected virtual void Awake()
	{
		this._CanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
		this.transform.SetParent(UIWindowManager.Instance.transform);
		UIWindowManager.Instance.SortingWindowUIOrder();
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
		if (this._IsFocused)
			return;

		this._IsFocused = true;
		UIWindow.OnBeforeFocusWindow(this);
	}

	public virtual void Show()
	{
		CanvasGroup.alpha = 1;
		CanvasGroup.blocksRaycasts = true;
		this.Focus();
		this.gameObject.SetActive(true);
	}

	public virtual void Hide()
	{
		CanvasGroup.alpha = 0;
		CanvasGroup.blocksRaycasts = false;
		this.gameObject.SetActive(false);
	}

	#region Static Methods

	public static List<UIWindow> GetWindows()
	{
		List<UIWindow> windows = new List<UIWindow>();
		UIWindow[] ws = FindObjectsOfType<UIWindow>(true);

		foreach (UIWindow w in ws)
		{
			if (w.gameObject.activeInHierarchy)
				windows.Add(w);
		}

		return windows;
	}

	public static UIWindow GetWindow<T>() where T : UIWindow
	{
		foreach (UIWindow window in UIWindow.GetWindows())
			if (window.GetType() == typeof(T))
				return (T)window;
		return null;
	}

	public static void FocusWindow<T>() where T : UIWindow
	{
		if (UIWindow.GetWindow<T>() != null)
			UIWindow.GetWindow<T>().Focus();
	}
	
	protected static void OnBeforeFocusWindow(UIWindow window)
	{
		if (_FucusedWindow != null)
			_FucusedWindow._IsFocused = false;

		_FucusedWindow = window;
	}

	#endregion
}