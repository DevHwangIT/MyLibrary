using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UIWindowManager : MonoBehaviour
{
	#region Singleton

	private static UIWindowManager _instance;

	public static UIWindowManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (UIWindowManager) FindObjectOfType(typeof(UIWindowManager));
				if (_instance == null)
				{
					GameObject singletonObject = new GameObject($"{typeof(UIWindowManager)} (Singleton)");
					singletonObject.AddComponent<RectTransform>();
					_instance = singletonObject.AddComponent<UIWindowManager>();
				}
			}
			return _instance;
		}
	}

	#endregion

	[SerializeField] private string m_EscapeInputName = "Cancel";
	public string escapeInputName
	{
		get { return this.m_EscapeInputName; }
	}

	private void Awake()
	{
		SortingWindowUIOrder();
	}

	private void OnValidate()
	{
		SortingWindowUIOrder();
	}

	protected virtual void Update()
	{
		if (Input.GetButtonDown(this.m_EscapeInputName))
		{
			List<UIWindow> windows = UIWindow.GetWindows();

			UIWindow lastWindow = null;
			foreach (UIWindow window in windows)
			{
				if(!window.IsVisible)
					continue;
					
				if (lastWindow == null)
					lastWindow = window;
				else
				{
					if (lastWindow.ID < window.ID)
						lastWindow = window;
					else
					{
						Debug.Log(window.ID.ToString());
					}
				}
			}

			if (lastWindow == null)
			{
				UIWindow.GetWindow(UIWindowID.GameMenu).Show();
			}
			else
			{
				if (lastWindow.IsVisible)
					lastWindow.Hide();
			}
		}
	}

	private void SortingWindowUIOrder()
	{
		List<UIWindow> windows = UIWindow.GetWindows();
		windows = windows.OrderBy(x => x.ID).ToList();
		foreach (var window in windows)
		{
			window.transform.SetAsLastSibling();
		}
	}
}