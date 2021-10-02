using UnityEngine;
using System.Collections.Generic;

public class UIWindowManager : MonoBehaviour
{
	private static UIWindowManager m_Instance;

	public static UIWindowManager Instance
	{
		get { return m_Instance; }
	}

	[SerializeField] private string m_EscapeInputName = "Cancel";
	private bool m_EscapeUsed = false;

	public string escapeInputName
	{
		get { return this.m_EscapeInputName; }
	}

	public bool escapedUsed
	{
		get { return this.m_EscapeUsed; }
	}

	protected virtual void Awake()
	{
		if (m_Instance == null)
		{
			m_Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	protected virtual void OnDestroy()
	{
		if (m_Instance.Equals(this))
			m_Instance = null;
	}

	protected virtual void Update()
	{
		if (this.m_EscapeUsed)
			this.m_EscapeUsed = false;

		if (Input.GetButtonDown(this.m_EscapeInputName))
		{
			UIModalBox[] modalBoxes = FindObjectsOfType<UIModalBox>();

			if (modalBoxes.Length > 0)
			{
				foreach (UIModalBox box in modalBoxes)
				{
					if (box.isActive && box.isActiveAndEnabled && box.gameObject.activeInHierarchy)
						return;
				}
			}

			List<UIWindow> windows = UIWindow.GetWindows();

			foreach (UIWindow window in windows)
			{
				if (window.escapeKeyAction != UIWindow.EscapeKeyAction.None)
				{
					if (window.IsOpen && (window.escapeKeyAction == UIWindow.EscapeKeyAction.Hide ||
					                      window.escapeKeyAction == UIWindow.EscapeKeyAction.Toggle ||
					                      (window.escapeKeyAction == UIWindow.EscapeKeyAction.HideIfFocused &&
					                       window.IsFocused)))
					{
						window.Hide();

						this.m_EscapeUsed = true;
					}
				}
			}

			if (this.m_EscapeUsed)
				return;

			foreach (UIWindow window in windows)
			{
				if (!window.IsOpen && window.escapeKeyAction == UIWindow.EscapeKeyAction.Toggle)
				{
					window.Show();
				}
			}
		}
	}
}