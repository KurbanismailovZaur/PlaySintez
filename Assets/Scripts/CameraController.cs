using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour 
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
    private bool _allowTranslation;

    [SerializeField]
    private float _moveDistancePerPixel = 1f;

    [SerializeField]
    private float _heightPositionMin = 8f;

    [SerializeField]
    private float _heightPositionMax = 64f;

    private Vector3 _targetPosition;

    [SerializeField]
    [Range(0f, 16)]
    private float _lerpValue = 1f;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    private void Awake()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        LerpCameraPosition();
    }

    private void CalculateHorizontalPosition(Vector2 mouseDeltaPosition)
    {
        _targetPosition -= new Vector3(mouseDeltaPosition.x, 0f, mouseDeltaPosition.y) * _moveDistancePerPixel * (transform.position.y / _heightPositionMin);
    }

    private void CalculateHeight(bool isMouseInViewport, float mouseScrollDelta)
    {
        if(!isMouseInViewport)
        {
            return;
        }

        float height = _targetPosition.y;
        height = Mathf.Clamp(height - mouseScrollDelta, _heightPositionMin, _heightPositionMax);

        _targetPosition = new Vector3(_targetPosition.x, height, _targetPosition.z);
    }

    private void LerpCameraPosition()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpValue * Time.deltaTime);
    }
    #endregion

    #region Event handlers
    public void InputManager_MousePositionChanged(InputManager.MouseInformation mouseInfo)
    {
        if (_allowTranslation)
        {
            CalculateHorizontalPosition(mouseInfo.MouseDeltaPosition);
        }
    }

    public void InputManager_MouseLeftButtonPressed(InputManager.MouseInformation mouseInfo)
    {
        _allowTranslation = mouseInfo.IsMouseInViewport;
    }

    public void InputManager_MouseLeftButtonReleased(InputManager.MouseInformation mouseInfo)
    {
        _allowTranslation = false;
    }

    public void InputManager_MouseScrollDeltaChanged(InputManager.MouseInformation mouseInfo)
    {
        CalculateHeight(mouseInfo.IsMouseInViewport, mouseInfo.MouseScrollDelta);
    }
    #endregion
}
