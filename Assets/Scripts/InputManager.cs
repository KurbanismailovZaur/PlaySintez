using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class InputManager : Singleton<InputManager> 
{
    #region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structs
    [Serializable]
    public struct MouseInformation
    {
        public Vector2 Position { get; set; }
        public Vector2 MouseDeltaPosition { get; set; }
        public bool IsMouseInViewport { get; set; }
        public ButtonsPressedState ButtonsPressedState { get; set; }
        public float MouseScrollDelta { get; set; }
    }

    [Serializable]
    public struct ButtonsPressedState
    {
        public bool Left { get; set; }
        public bool Right { get; set; }
    }
    #endregion

    #region Classes
    [Serializable]
    public class MousePositionChangedEvent : UnityEvent<MouseInformation> { }

    [Serializable]
    public class MouseLeftButtonPressedEvent : UnityEvent<MouseInformation> { }

    [Serializable]
    public class MouseLeftButtonReleasedEvent : UnityEvent<MouseInformation> { }

    [Serializable]
    public class MouseScrollDeltaChangedEvent : UnityEvent<MouseInformation> { }

    [Serializable]
    public class MouseLeftButtonClickedEvent : UnityEvent<MouseInformation> { }
    #endregion

    #region Fiedls
    private MouseInformation _mouseInfo;

    private bool _isMouseMoved;
    #endregion

    #region Events
    public MousePositionChangedEvent MousePositionChanged;
    public MouseLeftButtonPressedEvent MouseLeftButtonPressed;
    public MouseLeftButtonReleasedEvent MouseLeftButtonReleased;
    public MouseScrollDeltaChangedEvent MouseScrollDeltaChanged;
    public MouseLeftButtonClickedEvent MouseLeftButtonClicked;
    #endregion

    #region Properties
    public MouseInformation MouseInfo { get { return _mouseInfo; } }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    private void Awake()
    {
        _mouseInfo.Position = Input.mousePosition;
    }

    private void Update()
    {
        CalculateMouseInformation();

        if (!Mathf.Approximately(_mouseInfo.MouseDeltaPosition.sqrMagnitude, 0f))
        {
            _isMouseMoved = true;

            MousePositionChanged.Invoke(_mouseInfo);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _isMouseMoved = false;

            MouseLeftButtonPressed.Invoke(_mouseInfo);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseLeftButtonReleased.Invoke(_mouseInfo);

            if (!_isMouseMoved)
            {
                MouseLeftButtonClicked.Invoke(_mouseInfo);
            }
        }

        if (!Mathf.Approximately(_mouseInfo.MouseScrollDelta, 0f))
        {
            MouseScrollDeltaChanged.Invoke(_mouseInfo);
        }
    }

    private void CalculateMouseInformation()
    {
        _mouseInfo.MouseDeltaPosition = ((Vector2)Input.mousePosition) - _mouseInfo.Position;
        _mouseInfo.Position = Input.mousePosition;

        _mouseInfo.IsMouseInViewport = !EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0))
        {
            ButtonsPressedState buttonsPressedState = _mouseInfo.ButtonsPressedState;
            buttonsPressedState.Left = true;

            _mouseInfo.ButtonsPressedState = buttonsPressedState;
        }

        if (Input.GetMouseButtonUp(0))
        {
            ButtonsPressedState buttonsPressedState = _mouseInfo.ButtonsPressedState;
            buttonsPressedState.Left = false;

            _mouseInfo.ButtonsPressedState = buttonsPressedState;
        }

        _mouseInfo.MouseScrollDelta = Input.mouseScrollDelta.y;
    }
    #endregion

    #region Event handlers
    #endregion
}
