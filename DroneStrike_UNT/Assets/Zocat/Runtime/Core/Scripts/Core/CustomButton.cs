using System;
using Opsive.Shared.Game;
using UnityEngine;
using UnityEngine.EventSystems;

// using Iso;


namespace Zocat
{
    public class CustomButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
    {
        private bool _animatable;
        private Vector2 initialScale;

        /*--------------------------------------------------------------------------------------*/
        public bool IsEnabled { get; set; }

        private void Start()
        {
            initialScale = transform.localScale;
        }

        /*--------------------------------------------------------------------------------------*/
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsEnabled) return;
            // base.OnPointerClick(eventData);
            _OnClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsEnabled) return;
            // base.OnPointerDown(eventData);
            if (_animatable) transform.localScale = initialScale * 1.2f;
            _OnPointerDown?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsEnabled) return;
            // base.OnPointerEnter(eventData);
            _OnPointerEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsEnabled) return;
            // base.OnPointerExit(eventData);
            _OnPointerExit?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsEnabled) return;
            // base.OnPointerUp(eventData);
            if (_animatable) transform.localScale = initialScale;
            _OnPointerUp?.Invoke(eventData);
        }

        /*--------------------------------------------------------------------------------------*/


        private event Action _OnClick;
        private event Action<PointerEventData> _OnPointerEnter;
        private event Action<PointerEventData> _OnPointerDown;
        private event Action<PointerEventData> _OnPointerUp;
        private event Action<PointerEventData> _OnPointerExit;

        /*--------------------------------------------------------------------------------------*/
        private void Initialize()
        {
            IsEnabled = true;
        }

        protected void Animate()
        {
            // transform.DOKill(true);
            // transform.DOScale(new Vector3(.9f, .9f, .9f), .1f).SetLoops(2, LoopType.Yoyo).SetAutoKill(true).Play();
            transform.localScale = initialScale * 1.2f;
            Scheduler.Schedule(1, () => transform.localScale = initialScale);
        }

        #region Initialize

        public virtual void InitializeClick(Action onClick, bool animatable = false)
        {
            Initialize();
            _animatable = animatable;
            _OnClick = onClick;
        }

        public virtual void InitializeEnter(Action<PointerEventData> onPointerEnter)
        {
            Initialize();
            _OnPointerEnter = onPointerEnter;
        }

        public virtual void InitializeDown(Action<PointerEventData> onPointerDown)
        {
            Initialize();
            _OnPointerDown = onPointerDown;
        }

        public virtual void InitializeUp(Action<PointerEventData> onPointerUp)
        {
            Initialize();
            _OnPointerUp = onPointerUp;
        }

        public virtual void InitializeExit(Action<PointerEventData> onPointerExit)
        {
            Initialize();
            _OnPointerExit = onPointerExit;
        }

        #endregion
    }
}