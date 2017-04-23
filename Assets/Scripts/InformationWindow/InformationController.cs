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
        private CapsuleInformation _capsuleInformation;

        [SerializeField]
        private BaseInformation _baseInformation;

        [SerializeField]
        private ResearchCenterInformation _researchCenterInformation;

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
                        Module module = _trackedObject.GetComponent<Module>();

                        switch (module.ModuleType)
                        {
                            case Module.Type.LookCapsule:
                            case Module.Type.CommentCapsule:
                            case Module.Type.LikeCapsule:
                                _trackedObject.GetComponent<Modules.Capsule>().StateChanged.RemoveListener(Capsule_StateChanged);
                                break;
                            case Module.Type.Base:
                                _trackedObject.GetComponent<Modules.Base>().StateChanged.RemoveListener(Base_StateChanged);
                                break;
                            case Module.Type.ResearchCenter:
                                _trackedObject.GetComponent<Modules.ResearchCenter>().StateChanged.RemoveListener(Base_StateChanged);
                                break;
                        }

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

                    UpdateInformationAboutStar(star);
                    break;
                case BaseObject.ObjectType.Orbit:
                    Orbit orbit = _trackedObject.GetComponent<Orbit>();
                    orbit.StateChanged.AddListener(Orbit_StateChanged);

                    _trackedInformationWindow = _orbitInformation.gameObject;
                    _trackedInformationWindow.SetActive(true);

                    UpdateInformationAboutOrbit(orbit);
                    break;
                case BaseObject.ObjectType.Module:
                    Module module = _trackedObject.GetComponent<Module>();

                    switch (module.ModuleType)
                    {
                        case Module.Type.LookCapsule:
                        case Module.Type.CommentCapsule:
                        case Module.Type.LikeCapsule:
                            _trackedInformationWindow = _capsuleInformation.gameObject;
                            Modules.Capsule capsule = module.GetComponent<Modules.Capsule>();
                            capsule.StateChanged.AddListener(Capsule_StateChanged);
                            UpdateInformationAboutCapsule(capsule);
                            break;
                        case Module.Type.Base:
                            _trackedInformationWindow = _baseInformation.gameObject;
                            Modules.Base baseModule = module.GetComponent<Modules.Base>();
                            baseModule.StateChanged.AddListener(Base_StateChanged);
                            UpdateInformationAboutBase(baseModule);
                            break;
                        case Module.Type.ResearchCenter:
                            _trackedInformationWindow = _researchCenterInformation.gameObject;
                            Modules.ResearchCenter researchCenter = module.GetComponent<Modules.ResearchCenter>();
                            researchCenter.StateChanged.AddListener(Base_StateChanged);
                            UpdateInformationAboutResearchCenter(researchCenter);
                            break;
                    }
                    
                    _trackedInformationWindow.SetActive(true);
                    
                    break;
            }
        }

        private void UpdateInformationAboutStar(Star star)
        {
            _starInformation.UpdateInformationAboutStar(star);
        }

        private void UpdateInformationAboutOrbit(Orbit orbit)
        {
            _orbitInformation.UpdateInformationAboutOrbit(orbit);
        }

        private void UpdateInformationAboutCapsule(Modules.Capsule capsule)
        {
            _capsuleInformation.UpdateInformationAboutCapsule(capsule);
        }

        private void UpdateInformationAboutBase(Modules.Base baseModule)
        {
            _baseInformation.UpdateInformationAboutBase(baseModule);
        }

        private void UpdateInformationAboutResearchCenter(Modules.ResearchCenter researchCenter)
        {
            _researchCenterInformation.UpdateInformationAboutResearchCenter(researchCenter);
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

        #region BaseObject state changed handlers
        private void Star_StateChanged(Star star)
        {
            UpdateInformationAboutStar(star);
        }

        private void Orbit_StateChanged(Orbit orbit)
        {
            UpdateInformationAboutOrbit(orbit);
        }

        private void Capsule_StateChanged(Modules.Capsule capsule)
        {
            UpdateInformationAboutCapsule(capsule);
        }

        private void Base_StateChanged(Modules.Base baseModule)
        {
            UpdateInformationAboutBase(baseModule);
        }

        private void ResearchCenter_StateChanged(Modules.ResearchCenter researchCenter)
        {
            UpdateInformationAboutResearchCenter(researchCenter);
        }
        #endregion
        #endregion
    }
}