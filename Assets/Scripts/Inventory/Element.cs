using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

namespace Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Element : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Enums
        public enum ModuleType
        {
            LookCapsule,
            CommentCapsule,
            LikeCapsule,
            Base,
            ResearchCenter
        }
        #endregion

        #region Delegates
        #endregion

        #region Structs
        #endregion

        #region Classes
        [Serializable]
        public class StartDragEvent : UnityEvent<Element> { }

        [Serializable]
        public class EndDragEvent : UnityEvent<Element> { }
        #endregion

        #region Fiedls
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _text;

        private bool _isDrag;
        private Canvas _canvas;

        private CanvasGroup _canvasGroup;

        private Tween _tween;
        #endregion

        #region Events
        public StartDragEvent Draging;
        public EndDragEvent Draged;
        #endregion

        #region Properties
        public Sprite Icon
        {
            set { _icon.sprite = value; }
        }

        public string Name
        {
            get { return _text.text; }
            set { _text.text = value.Substring(0, 8); }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public abstract ModuleType GetModuleType();

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDrag = true;
            _canvas = GetComponentInParent<Canvas>();

            InputManager.Instance.MousePositionChanged.AddListener(InputManager_MousePositionChanged);

            _tween.Kill();
            _tween = DOTween.To(() => { return _canvasGroup.alpha; }, (x) => { _canvasGroup.alpha = x; }, 0.5f, 0.250f).Play();
            
            Draging.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDrag = false;

            InputManager.Instance.MousePositionChanged.RemoveListener(InputManager_MousePositionChanged);

            _tween.Kill();
            _tween = DOTween.To(() => { return _canvasGroup.alpha; }, (x) => { _canvasGroup.alpha = x; }, 1f, 0.250f).Play();

            Draged.Invoke(this);
        }

        private void ProcessDrag(InputManager.MouseInformation mouseInfo)
        {
            RectTransform rectTransform = (RectTransform)transform;

            Vector2 scaledMouseDelta = Vector2.Scale(mouseInfo.MouseDeltaPosition, new Vector2(1f / _canvas.transform.lossyScale.x, 1f / _canvas.transform.lossyScale.y));
            rectTransform.anchoredPosition += scaledMouseDelta;
        }
        #endregion

        #region Event handlers
        private void InputManager_MousePositionChanged(InputManager.MouseInformation mouseInfo)
        {
            ProcessDrag(mouseInfo);
        }
        #endregion
    }
}