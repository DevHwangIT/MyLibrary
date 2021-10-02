using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[DisallowMultipleComponent, ExecuteInEditMode, AddComponentMenu("UI/Window", 58), RequireComponent(typeof(CanvasGroup))]
	public class UIWindow : MonoBehaviour, IEventSystemHandler, ISelectHandler, IPointerDownHandler {
		
		public enum Transition
		{
			Instant,
			Fade
		}
		
		public enum VisualState
		{
			Shown,
			Hidden
		}
		
		public enum EscapeKeyAction
		{
			None,
			Hide,
			HideIfFocused,
			Toggle
		}
		
		[Serializable] public class TransitionBeginEvent : UnityEvent<UIWindow, VisualState, bool> {}
		[Serializable] public class TransitionCompleteEvent : UnityEvent<UIWindow, VisualState> {}
		
		protected static UIWindow m_FucusedWindow;
		public static UIWindow FocusedWindow { get { return m_FucusedWindow; } private set { m_FucusedWindow = value; } }
		
		[SerializeField] private UIWindowID m_WindowId = UIWindowID.None;
		[SerializeField] private int m_CustomWindowId = 0;
		[SerializeField] private VisualState m_StartingState = VisualState.Hidden;
		[SerializeField] private EscapeKeyAction m_EscapeKeyAction = EscapeKeyAction.Hide;
		[SerializeField] private bool m_UseBlackOverlay = false;

        [SerializeField] private bool m_FocusAllowReparent = true;

		[SerializeField] private Transition m_Transition = Transition.Instant;
		[SerializeField] private TweenEasing m_TransitionEasing = TweenEasing.InOutQuint;
		[SerializeField] private float m_TransitionDuration = 0.1f;
		
		protected bool m_IsFocused = false;
		private VisualState m_CurrentVisualState = VisualState.Hidden;
		private CanvasGroup m_CanvasGroup;
		
		public UIWindowID ID
		{
			get { return this.m_WindowId; }
			set { this.m_WindowId = value; }
		}
		
		public int CustomID
		{
			get { return this.m_CustomWindowId; }
			set { this.m_CustomWindowId = value; }
		}
		
		public EscapeKeyAction escapeKeyAction
		{
			get { return this.m_EscapeKeyAction; }
			set { this.m_EscapeKeyAction = value; }
		}
		
        public bool useBlackOverlay
        {
            get { return this.m_UseBlackOverlay; }
            set
            {
                this.m_UseBlackOverlay = value;
                
                if (Application.isPlaying && this.m_UseBlackOverlay && this.isActiveAndEnabled)
                {
                    UIBlackOverlay overlay = UIBlackOverlay.GetOverlay(this.gameObject);

                    if (overlay != null)
                    {
                        if (value) this.onTransitionBegin.AddListener(overlay.OnTransitionBegin);
                        else this.onTransitionBegin.RemoveListener(overlay.OnTransitionBegin);
                    }
                }
            }
        }

        public bool focusAllowReparent
        {
            get { return this.m_FocusAllowReparent; }
            set { this.m_FocusAllowReparent = value; }
        }

		public Transition transition
		{
			get { return this.m_Transition; }
			set { this.m_Transition = value; }
		}
		
		public TweenEasing transitionEasing
		{
			get { return this.m_TransitionEasing; }
			set { this.m_TransitionEasing = value; }
		}
		
		public float transitionDuration
		{
			get { return this.m_TransitionDuration; }
			set { this.m_TransitionDuration = value; }
		}
		
		public TransitionBeginEvent onTransitionBegin = new TransitionBeginEvent();
		
		public TransitionCompleteEvent onTransitionComplete = new TransitionCompleteEvent();
		
		public bool IsVisible
		{
			get { return (this.m_CanvasGroup != null && this.m_CanvasGroup.alpha > 0f) ? true : false; }
		}
		
		public bool IsOpen
		{
			get { return (this.m_CurrentVisualState == VisualState.Shown); }
		}
		
		public bool IsFocused
		{
			get { return this.m_IsFocused; }
		}
		
		[NonSerialized] private readonly TweenRunner<FloatTween> m_FloatTweenRunner;
		
		protected UIWindow()
		{
			if (this.m_FloatTweenRunner == null)
				this.m_FloatTweenRunner = new TweenRunner<FloatTween>();
			
			this.m_FloatTweenRunner.Init(this);
		}
		
		protected virtual void Awake()
		{
			// Get the canvas group
			this.m_CanvasGroup = this.gameObject.GetComponent<CanvasGroup>();

            // Transition to the starting state
            if (Application.isPlaying)
                this.ApplyVisualState(this.m_StartingState);
        }
		
		protected virtual void Start()
		{
			if (this.CustomID == 0)
				this.CustomID = UIWindow.NextUnusedCustomID;
            
            if (this.m_EscapeKeyAction != EscapeKeyAction.None)
            {
                UIWindowManager manager = Component.FindObjectOfType<UIWindowManager>();

                if (manager == null)
                {
                    GameObject newObj = new GameObject("Window Manager");
                    newObj.AddComponent<UIWindowManager>();
                    newObj.transform.SetAsFirstSibling();
                }
            }
        }
		
        protected virtual void OnEnable()
        {
            if (Application.isPlaying && this.m_UseBlackOverlay)
            {
                UIBlackOverlay overlay = UIBlackOverlay.GetOverlay(this.gameObject);

                if (overlay != null)
                    this.onTransitionBegin.AddListener(overlay.OnTransitionBegin);
            }
        }

        protected virtual void OnDisable()
        {
            if (Application.isPlaying && this.m_UseBlackOverlay)
            {
                UIBlackOverlay overlay = UIBlackOverlay.GetOverlay(this.gameObject);

                if (overlay != null)
                    this.onTransitionBegin.RemoveListener(overlay.OnTransitionBegin);
            }
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
		{
			this.m_TransitionDuration = Mathf.Max(this.m_TransitionDuration, 0f);
		}
#endif
		
		protected virtual bool IsActive()
		{
			return (this.enabled && this.gameObject.activeInHierarchy);
		}
		
		public virtual void OnSelect(BaseEventData eventData)
		{
			// Focus the window
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

            this.BringToFront();
		}

        public void BringToFront()
        {
            UIUtility.BringToFront(this.gameObject, this.m_FocusAllowReparent);
        }

        public virtual void Toggle()
		{
			if (this.m_CurrentVisualState == VisualState.Shown)
				this.Hide();
			else
				this.Show();
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
			
			if (this.m_CurrentVisualState == VisualState.Shown)
				return;
            
            this.EvaluateAndTransitionToVisualState(VisualState.Shown, instant);
		}

		public virtual void Hide()
		{
			this.Hide(false);
		}
		
		public virtual void Hide(bool instant)
		{
			if (!this.IsActive())
				return;
			
			if (this.m_CurrentVisualState == VisualState.Hidden)
				return;
			
			this.EvaluateAndTransitionToVisualState(VisualState.Hidden, instant);
		}
		
		protected virtual void EvaluateAndTransitionToVisualState(VisualState state, bool instant)
		{
			float targetAlpha = (state == VisualState.Shown) ? 1f : 0f;
			
			if (this.onTransitionBegin != null)
				this.onTransitionBegin.Invoke(this, state, (instant || this.m_Transition == Transition.Instant));
			
			if (this.m_Transition == Transition.Fade)
			{
				float duration = (instant) ? 0f : this.m_TransitionDuration;
                
                this.StartAlphaTween(targetAlpha, duration, true);
			}
			else
			{
				this.SetCanvasAlpha(targetAlpha);
				
				if (this.onTransitionComplete != null)
					this.onTransitionComplete.Invoke(this, state);
			}
			
			this.m_CurrentVisualState = state;
			
			if (state == VisualState.Shown)
			{
				this.m_CanvasGroup.blocksRaycasts = true;
			}
		}

        public virtual void ApplyVisualState(VisualState state)
        {
            float targetAlpha = (state == VisualState.Shown) ? 1f : 0f;

            this.SetCanvasAlpha(targetAlpha);

            this.m_CurrentVisualState = state;

            if (state == VisualState.Shown)
            {
                this.m_CanvasGroup.blocksRaycasts = true;
                //this.m_CanvasGroup.interactable = true;
            }
            else
            {
                this.m_CanvasGroup.blocksRaycasts = false;
                //this.m_CanvasGroup.interactable = false;
            }
        }

        public void StartAlphaTween(float targetAlpha, float duration, bool ignoreTimeScale)
		{
			if (this.m_CanvasGroup == null)
				return;
            
            var floatTween = new FloatTween { duration = duration, startFloat = this.m_CanvasGroup.alpha, targetFloat = targetAlpha };
			floatTween.AddOnChangedCallback(SetCanvasAlpha);
			floatTween.AddOnFinishCallback(OnTweenFinished);
			floatTween.ignoreTimeScale = ignoreTimeScale;
			floatTween.easing = this.m_TransitionEasing;
			this.m_FloatTweenRunner.StartTween(floatTween);
		}
		
		public void SetCanvasAlpha(float alpha)
		{
			if (this.m_CanvasGroup == null)
				return;
			
			this.m_CanvasGroup.alpha = alpha;
			
			if (alpha == 0f)
			{
				this.m_CanvasGroup.blocksRaycasts = false;
				//this.m_CanvasGroup.interactable = false;
			}
		}
		
		protected virtual void OnTweenFinished()
		{
			if (this.onTransitionComplete != null)
				this.onTransitionComplete.Invoke(this, this.m_CurrentVisualState);
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
		
		public static int SortByCustomWindowID(UIWindow w1, UIWindow w2)
		{
			return w1.CustomID.CompareTo(w2.CustomID);
		}
		
		public static int NextUnusedCustomID
		{
			get
			{
				List<UIWindow> windows = UIWindow.GetWindows();
				
				if (GetWindows().Count > 0)
				{
					windows.Sort(UIWindow.SortByCustomWindowID);
					
					return windows[windows.Count - 1].CustomID + 1;
				}
				
				return 0;
			}
		}
		
		public static UIWindow GetWindow(UIWindowID id)
		{
			foreach (UIWindow window in UIWindow.GetWindows())
				if (window.ID == id)
					return window;
			
			return null;
		}
		
		public static UIWindow GetWindowByCustomID(int customId)
		{
			foreach (UIWindow window in UIWindow.GetWindows())
				if (window.CustomID == customId)
					return window;
			
			return null;
		}
		
		public static void FocusWindow(UIWindowID id)
		{
			// Focus the window
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
}
