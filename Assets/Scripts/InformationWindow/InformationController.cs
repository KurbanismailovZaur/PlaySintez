using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InformationWindow
{
    public class InformationController : MonoBehaviour
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
        private RectTransform _informationContainer;

        [SerializeField]
        private Image _arrowImage;

        [SerializeField]
        private float _animationDuration;

        [Header("Information Types")]
        [SerializeField]
        private StarInformation _starInformation;

        [SerializeField]
        private OrbitInformation _orbitInformation;

        [SerializeField]
        private ModuleInformation _moduleInformation;

        private BaseObject _trackedObject;
        private GameObject _trackedInformationWindow;
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void Open()
        {
            if (_isBusy || _isOpen)
            {
                return;
            }

            _isBusy = true;

            Sequence seq = DOTween.Sequence();

            seq.Insert(0f, ((RectTransform)transform).DOAnchorPosX(0f, _animationDuration));
            seq.Insert(0f, _arrowImage.rectTransform.DORotate(Vector3.zero, _animationDuration));

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

            seq.Insert(0f, ((RectTransform)transform).DOAnchorPosX(_informationContainer.sizeDelta.x, _animationDuration));
            seq.Insert(0f, _arrowImage.rectTransform.DORotate(new Vector3(0f, 0f, 180f), _animationDuration));

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

        private void TrackInformationAboutSelected(BaseObject selected)
        {
            if (_trackedObject != null)
            {
                _trackedInformationWindow.SetActive(false);

                switch (_trackedObject.BaseObjectType)
                {
                    case BaseObject.ObjectType.Star:
                        _trackedObject.GetComponent<Star>().StateChanged.RemoveListener(Star_StateChanged);
                        break;
                    case BaseObject.ObjectType.Orbit:
                        _trackedObject.GetComponent<Orbit>().StateChanged.RemoveListener(Orbit_StateChanged);
                        break;
                    case BaseObject.ObjectType.Module:
                        _trackedObject.GetComponent<Module>().StateChanged.RemoveListener(Module_StateChanged);
                        break;
                }
            }

            if (selected == null)
            {
                _trackedObject = null;
                _trackedInformationWindow = null;

                return;
            }

            _trackedObject = selected;

            switch (_trackedObject.BaseObjectType)
            {
                case BaseObject.ObjectType.Star:
                    Star star = _trackedObject.GetComponent<Star>();
                    star.StateChanged.AddListener(Star_StateChanged);

                    _trackedInformationWindow = _starInformation.gameObject;
                    _trackedInformationWindow.SetActive(true);

                    UpdateInformationAboutStar(star.GetState());
                    break;
                case BaseObject.ObjectType.Orbit:
                    Orbit orbit = _trackedObject.GetComponent<Orbit>();
                    orbit.StateChanged.AddListener(Orbit_StateChanged);

                    _trackedInformationWindow = _orbitInformation.gameObject;
                    _trackedInformationWindow.SetActive(true);

                    UpdateInformationAboutOrbit(orbit.GetState());
                    break;
                case BaseObject.ObjectType.Module:
                    Module module = _trackedObject.GetComponent<Module>();
                    module.StateChanged.AddListener(Module_StateChanged);

                    _trackedInformationWindow = _orbitInformation.gameObject;
                    _trackedInformationWindow.SetActive(true);

                    UpdateInformationAboutModule(module.GetState());
                    break;
            }
        }

        private void UpdateInformationAboutStar(Star.State state)
        {
            _starInformation.UpdateInformationAboutStar(state);
        }

        private void UpdateInformationAboutOrbit(Orbit.State state)
        {
            _orbitInformation.UpdateInformationAboutOrbit(state);
        }

        private void UpdateInformationAboutModule(Module.State state)
        {
            _moduleInformation.UpdateInformationAboutModule(state);
        }
        #endregion

        #region Event handlers
        public void Button_Clicked()
        {
            SwitchState();
        }

        public void SelectionManager_SelectedChanged(BaseObject selected)
        {
            TrackInformationAboutSelected(selected);
        }

        private void Star_StateChanged(Star.State state)
        {
            UpdateInformationAboutStar(state);
        }

        private void Orbit_StateChanged(Orbit.State state)
        {
            UpdateInformationAboutOrbit(state);
        }

        private void Module_StateChanged(Module.State state)
        {
            UpdateInformationAboutModule(state);
        }
        #endregion
    }
}