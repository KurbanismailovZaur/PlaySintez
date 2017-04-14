using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        #endregion

        #region Fiedls
        private bool _isBusy;
        private bool _isOpen;

        [SerializeField]
        private RectTransform _inventoryContainer;

        [SerializeField]
        private Image _arrowImage;

        [SerializeField]
        private float _animationDuration = 1f;

        private List<Element.ModuleType> _modules;
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Methods
        private void Awake()
        {
            _modules = new List<Element.ModuleType>();
        }

        private void Start()
        {
            UpdateElements();
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

        private void UpdateElements()
        {
            _inventoryContainer.RemoveAllChilds();

            foreach (Element.ModuleType module in _modules)
            {
                switch (module)
                {
                    case Element.ModuleType.LookCapsule:
                        Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/LookCapsule"), _inventoryContainer, false);
                        break;
                    case Element.ModuleType.CommentCapsule:
                        Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/CommentCapsule"), _inventoryContainer, false);
                        break;
                    case Element.ModuleType.LikeCapsule:
                        Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/LikeCapsule"), _inventoryContainer, false);
                        break;
                    case Element.ModuleType.Base:
                        //Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/EmptyElement"), _inventoryContainer, false);
                        break;
                    case Element.ModuleType.ResearchCenter:
                        //Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/EmptyElement"), _inventoryContainer, false);
                        break;
                }
            }

            for (int i = _modules.Count; i < 16; i++)
            {
                Instantiate(Resources.Load<RectTransform>("Inventory/InventoryModules/EmptyElement"), _inventoryContainer, false);
            }
        }

        public void AddElement(Element.ModuleType moduleType)
        {
            _modules.Add(moduleType);

            UpdateElements();
        }
        #endregion

        #region Event handlers
        #endregion
    }
}