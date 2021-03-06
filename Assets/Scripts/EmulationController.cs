﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EmulationController : MonoBehaviour
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
    private RectTransform _emulationList;

    [SerializeField]
    private Image _arrowImage;

    [SerializeField]
    private float _animationDuration = 1f;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    public void Open()
    {
        if(_isBusy || _isOpen)
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
        if(_isBusy || !_isOpen)
        {
            return;
        }

        _isBusy = true;

        Sequence seq = DOTween.Sequence();

        seq.Insert(0f, ((RectTransform)transform).DOAnchorPosX(-_emulationList.sizeDelta.x, _animationDuration));
        seq.Insert(0f, _arrowImage.rectTransform.DORotate(Vector3.zero, _animationDuration));

        seq.AppendCallback(() =>
        {
            _isBusy = false;
            _isOpen = false;
        });

        seq.Play();
    }

    public void SetInteractableState(bool state)
    {
        foreach (Transform child in _emulationList)
        {
            child.GetComponent<Button>().interactable = state;
        }
    }
    #endregion

    #region Event handlers
    public void SwitchState()
    {
        if(_isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    #endregion
}
