using UnityEngine;
using System.Collections.Generic;

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
				_instance = (UIWindowManager) FindObjectOfType(typeof(GameManager));
				if (_instance == null)
				{
					GameObject singletonObject = new GameObject($"{typeof(UIWindowManager)} (Singleton)");
					_instance = singletonObject.AddComponent<UIWindowManager>();
					DontDestroyOnLoad(singletonObject);
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
	
	protected virtual void OnDestroy()
	{
		if (_instance.Equals(this))
			_instance = null;
	}

	protected virtual void Update()
	{
		if (Input.GetButtonDown(this.m_EscapeInputName))
		{
			List<UIWindow> windows = UIWindow.GetWindows();

			foreach (UIWindow window in windows)
			{
				
			}
		}
	}
}