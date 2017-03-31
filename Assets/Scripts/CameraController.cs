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
    private bool _isMouseDown;
    private bool _isMouseDownInViewport;

    private Vector2 _mousePosition;

    [SerializeField]
    private float _moveDistancePerPixel = 1f;

    [SerializeField]
    private float _heightPositionMin = 6f;

    [SerializeField]
    private float _heightPositionMax = 12f;

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
        CalculateHorizontalPosition();
        CalculateHeight();
        LerpCameraPosition();
    }

    private void CalculateHorizontalPosition()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            _isMouseDownInViewport = !EventSystem.current.IsPointerOverGameObject();

            _mousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
            _isMouseDownInViewport = false;
        }

        if(_isMouseDown && _isMouseDownInViewport)
        {
            Vector2 deltaPosition = (Vector2)Input.mousePosition - _mousePosition;
            _targetPosition -= new Vector3(deltaPosition.x, 0f, deltaPosition.y) * _moveDistancePerPixel;

            _mousePosition = Input.mousePosition;
        }
    }

    private void CalculateHeight()
    {
        bool isMouseInViewport = !EventSystem.current.IsPointerOverGameObject();

        if(!isMouseInViewport)
        {
            return;
        }

        float scrollValue = Input.mouseScrollDelta.y;
        if(!Mathf.Approximately(scrollValue, 0f))
        {
            float height = _targetPosition.y;
            height = Mathf.Clamp(height - scrollValue, _heightPositionMin, _heightPositionMax);

            _targetPosition = new Vector3(_targetPosition.x, height, _targetPosition.z);
        }
    }

    private void LerpCameraPosition()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpValue * Time.deltaTime);
    }
    #endregion

    #region Event handlers
    #endregion
}
