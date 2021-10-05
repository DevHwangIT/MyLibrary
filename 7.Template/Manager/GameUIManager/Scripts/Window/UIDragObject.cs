using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DuloGames.UI
{
    [AddComponentMenu("UI/Drag Object", 82)]
    public class UIDragObject : UIBehaviour, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public enum Rounding
        {
            Soft,
            Hard
        }

        [Serializable] public class BeginDragEvent : UnityEvent<BaseEventData> { }
        [Serializable] public class EndDragEvent : UnityEvent<BaseEventData> { }
        [Serializable] public class DragEvent : UnityEvent<BaseEventData> { }

        [SerializeField] private RectTransform m_Target;
        [SerializeField] private bool m_Horizontal = true;
        [SerializeField] private bool m_Vertical = true;
        [SerializeField] private bool m_Inertia = true;
        [SerializeField] private Rounding m_InertiaRounding = Rounding.Hard;
        [SerializeField] private float m_DampeningRate = 9f;
        [SerializeField] private bool m_ConstrainWithinCanvas = false;
        [SerializeField] private bool m_ConstrainDrag = true;
        [SerializeField] private bool m_ConstrainInertia = true;

        private Canvas m_Canvas;
        private RectTransform m_CanvasRectTransform;
        private Vector2 m_PointerStartPosition = Vector2.zero;
        private Vector2 m_TargetStartPosition = Vector2.zero;
        private Vector2 m_Velocity;
        private bool m_Dragging;
        private Vector2 m_LastPosition = Vector2.zero;

        public BeginDragEvent onBeginDrag = new BeginDragEvent();

        public EndDragEvent onEndDrag = new EndDragEvent();

        public DragEvent onDrag = new DragEvent();

        public RectTransform target
        {
            get { return this.m_Target; }
            set { this.m_Target = value; }
        }

        public bool horizontal
        {
            get { return this.m_Horizontal; }
            set { this.m_Horizontal = value; }
        }

        public bool vertical
        {
            get { return this.m_Vertical; }
            set { this.m_Vertical = value; }
        }

        public bool inertia
        {
            get { return this.m_Inertia; }
            set { this.m_Inertia = value; }
        }

        public float dampeningRate
        {
            get { return this.m_DampeningRate; }
            set { this.m_DampeningRate = value; }
        }

        public bool constrainWithinCanvas
        {
            get { return this.m_ConstrainWithinCanvas; }
            set { this.m_ConstrainWithinCanvas = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            this.m_Canvas = FindInParents<Canvas>((this.m_Target != null) ? this.m_Target.gameObject : this.gameObject);
            if (this.m_Canvas != null) this.m_CanvasRectTransform = this.m_Canvas.transform as RectTransform;
        }

        public static T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null)
                return null;
			
            var comp = go.GetComponent<T>();
			
            if (comp != null)
                return comp;
			
            Transform t = go.transform.parent;
			
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
			
            return comp;
        }
        
        protected override void OnTransformParentChanged()
        {
            base.OnTransformParentChanged();
            this.m_Canvas = FindInParents<Canvas>((this.m_Target != null) ? this.m_Target.gameObject : this.gameObject);
            if (this.m_Canvas != null) this.m_CanvasRectTransform = this.m_Canvas.transform as RectTransform;
        }

        public override bool IsActive()
        {
            return base.IsActive() && this.m_Target != null;
        }

        public void StopMovement()
        {
            this.m_Velocity = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData data)
        {
            if (!this.IsActive())
                return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_CanvasRectTransform, data.position, data.pressEventCamera, out this.m_PointerStartPosition);
            this.m_TargetStartPosition = this.m_Target.anchoredPosition;
            this.m_Velocity = Vector2.zero;
            this.m_Dragging = true;

            if (this.onBeginDrag != null)
                this.onBeginDrag.Invoke(data as BaseEventData);
        }

        public void OnEndDrag(PointerEventData data)
        {
            this.m_Dragging = false;

            if (!this.IsActive())
                return;

            // Invoke the event
            if (this.onEndDrag != null)
                this.onEndDrag.Invoke(data as BaseEventData);
        }

        public void OnDrag(PointerEventData data)
        {
            if (!this.IsActive() || this.m_Canvas == null)
                return;

            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_CanvasRectTransform, data.position, data.pressEventCamera, out mousePos);

            if (this.m_ConstrainWithinCanvas && this.m_ConstrainDrag)
            {
                mousePos = this.ClampToCanvas(mousePos);
            }

            Vector2 newPosition = this.m_TargetStartPosition + (mousePos - this.m_PointerStartPosition);

            // Restrict movement on the axis
            if (!this.m_Horizontal)
            {
                newPosition.x = this.m_Target.anchoredPosition.x;
            }
            if (!this.m_Vertical)
            {
                newPosition.y = this.m_Target.anchoredPosition.y;
            }

            this.m_Target.anchoredPosition = newPosition;

            if (this.onDrag != null)
                this.onDrag.Invoke(data as BaseEventData);
        }
        
        protected virtual void LateUpdate()
        {
            if (!this.m_Target)
                return;

            if (this.m_Dragging && this.m_Inertia)
            {
                Vector3 to = (this.m_Target.anchoredPosition - this.m_LastPosition) / Time.unscaledDeltaTime;
                this.m_Velocity = Vector3.Lerp(this.m_Velocity, to, Time.unscaledDeltaTime * 10f);
            }

            this.m_LastPosition = this.m_Target.anchoredPosition;

            if (!this.m_Dragging && this.m_Velocity != Vector2.zero)
            {
                Vector2 anchoredPosition = this.m_Target.anchoredPosition;

                this.Dampen(ref this.m_Velocity, this.m_DampeningRate, Time.unscaledDeltaTime);

                for (int i = 0; i < 2; i++)
                {
                    if (this.m_Inertia)
                    {
                        anchoredPosition[i] += this.m_Velocity[i] * Time.unscaledDeltaTime;
                    }
                    else
                    {
                        this.m_Velocity[i] = 0f;
                    }
                }

                if (this.m_Velocity != Vector2.zero)
                {
                    if (!this.m_Horizontal)
                    {
                        anchoredPosition.x = this.m_Target.anchoredPosition.x;
                    }
                    if (!this.m_Vertical)
                    {
                        anchoredPosition.y = this.m_Target.anchoredPosition.y;
                    }

                    if (this.m_ConstrainWithinCanvas && this.m_ConstrainInertia && this.m_CanvasRectTransform != null)
                    {
                        Vector3[] canvasCorners = new Vector3[4];
                        this.m_CanvasRectTransform.GetWorldCorners(canvasCorners);

                        Vector3[] targetCorners = new Vector3[4];
                        this.m_Target.GetWorldCorners(targetCorners);

                        if (targetCorners[0].x < canvasCorners[0].x || targetCorners[2].x > canvasCorners[2].x)
                        {
                            anchoredPosition.x = this.m_Target.anchoredPosition.x;
                        }

                        if (targetCorners[3].y < canvasCorners[3].y || targetCorners[1].y > canvasCorners[1].y)
                        {
                            anchoredPosition.y = this.m_Target.anchoredPosition.y;
                        }
                    }

                    if (anchoredPosition != this.m_Target.anchoredPosition)
                    {
                        switch (this.m_InertiaRounding)
                        {
                            case Rounding.Hard:
                                this.m_Target.anchoredPosition = new Vector2(Mathf.Round(anchoredPosition.x / 2f) * 2f, Mathf.Round(anchoredPosition.y / 2f) * 2f);
                                break;
                            case Rounding.Soft:
                            default:
                                this.m_Target.anchoredPosition = new Vector2(Mathf.Round(anchoredPosition.x), Mathf.Round(anchoredPosition.y));
                                break;
                        }
                    }
                }
            }
        }

        protected Vector3 Dampen(ref Vector2 velocity, float strength, float delta)
        {
            if (delta > 1f)
            {
                delta = 1f;
            }

            float dampeningFactor = 1f - strength * 0.001f;
            int ms = Mathf.RoundToInt(delta * 1000f);
            float totalDampening = Mathf.Pow(dampeningFactor, ms);
            Vector2 vTotal = velocity * ((totalDampening - 1f) / Mathf.Log(dampeningFactor));

            velocity = velocity * totalDampening;

            return vTotal * 0.06f;
        }

        protected Vector2 ClampToScreen(Vector2 position)
        {
            if (this.m_Canvas != null)
            {
                if (this.m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay || this.m_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
                {
                    float clampedX = Mathf.Clamp(position.x, 0f, Screen.width);
                    float clampedY = Mathf.Clamp(position.y, 0f, Screen.height);

                    return new Vector2(clampedX, clampedY);
                }
            }
            return position;
        }

        protected Vector2 ClampToCanvas(Vector2 position)
        {
            if (this.m_CanvasRectTransform != null)
            {
                Vector3[] corners = new Vector3[4];
                this.m_CanvasRectTransform.GetLocalCorners(corners);

                float clampedX = Mathf.Clamp(position.x, corners[0].x, corners[2].x);
                float clampedY = Mathf.Clamp(position.y, corners[3].y, corners[1].y);

                return new Vector2(clampedX, clampedY);
            }

            return position;
        }
    }
}