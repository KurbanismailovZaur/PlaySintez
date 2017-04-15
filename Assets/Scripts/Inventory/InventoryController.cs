using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Modules;
using System.Linq;
using System;
using UnityEngine.Events;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        #region Enums
        #endregion

        #region Delegates
        #endregion

        #region Structs
        #endregion

        #region Classes
        [Serializable]
        public class ElementDropedEvent : UnityEvent<Element, Vector2> { }
        #endregion

        #region Fiedls
        private bool _isBusy;
        private bool _isOpen;

        [SerializeField]
        private RectTransform _inventoryContainer;

        [SerializeField]
        private RectTransform _dragContainer;

        [SerializeField]
        private Image _arrowImage;

        [SerializeField]
        private float _animationDuration = 1f;

        private List<Element> _elements = new List<Element>();
        private byte _capacity = 16;

        private int _dragElementIndex;
        private bool _isDrag;
        #endregion

        #region Events
        public ElementDropedEvent ElementDroped; 
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void Start()
        {
            Initialize();
        }

        public void Open()
        {
            if (_isBusy || _isOpen)
            {
                return;
            }

            _isBusy = true;

            Sequence seq = DOTween.Sequence();

            seq.Insert(0f, ((RectTransform)transform).DOAnchorPosX(0f, _animationDuration));
            seq.Insert(0f, _arrowImage.rectTransform.DORotate(new Vector3(0f, 0f, 180f), _animationDuration));

            seq.AppendCallback(() =>
            {
                _isBusy = false;
                _isOpen = true;
            });

            seq.Play();
        }

        public void Close()
        {
            if (_isBusy || !_isOpen)
            {
                return;
            }

            _isBusy = true;

            Sequence seq = DOTween.Sequence();

            seq.Insert(0f, ((RectTransform)transform).DOAnchorPosX(-_inventoryContainer.sizeDelta.x, _animationDuration));
            seq.Insert(0f, _arrowImage.rectTransform.DORotate(Vector3.zero, _animationDuration));

            seq.AppendCallback(() =>
            {
                _isBusy = false;
                _isOpen = false;
            });

            seq.Play();
        }

        private void Initialize()
        {
            for (int i = 0; i < _capacity; i++)
            {
                Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/EmptyElement"), _inventoryContainer, false);
            }
        }

        public void SwitchState()
        {
            if (_isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public void AddElement(Element element)
        {
            if (_elements.Contains(element))
            {
                Debug.LogError("Element currenty in inventory");
                return;
            }

            if (_elements.Count == 16)
            {
                Debug.LogError("Inventory is full");
                return;
            }

            _elements.Add(element);

            DestroyImmediate(_inventoryContainer.GetChild(_inventoryContainer.childCount - 1).gameObject);

            element.transform.SetParent(_inventoryContainer, false);
            element.transform.SetSiblingIndex(_elements.Count - 1);

            element.Draging.AddListener(Element_Draging);
            element.Droped.AddListener(Element_Draged);
        }

        public void RemoveElement(Element element)
        {
            if (!_elements.Contains(element))
            {
                Debug.LogError("Element not exist in inventorys");
                return;
            }

            int index = _elements.IndexOf(element);

            _elements.Remove(element);
            _inventoryContainer.DetachChild(index);
            Destroy(element.gameObject);

            Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/EmptyElement"), _inventoryContainer, false);
        }
        #endregion

        #region Event handlers
        private void Element_Draging(Element element)
        {
            if (_isDrag)
            {
                return;
            }

            _dragElementIndex = _elements.IndexOf(element);

            RectTransform tempElement = Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/EmptyElement"), _inventoryContainer, false);
            tempElement.SetSiblingIndex(_dragElementIndex);
            element.transform.SetParent(_dragContainer);

            _isDrag = true;
        }

        private void Element_Draged(Element element)
        {
            if (!_isDrag)
            {
                return;
            }

            _isDrag = false;

            Destroy(_inventoryContainer.GetChild(_dragElementIndex).gameObject);
            _inventoryContainer.DetachChild(_dragElementIndex);

            element.transform.SetParent(_inventoryContainer);
            element.transform.SetSiblingIndex(_dragElementIndex);

            ElementDroped.Invoke(element, InputManager.Instance.MouseInfo.Position);
        }
        #endregion
    }
}