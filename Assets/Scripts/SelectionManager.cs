using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vectrosity;
using System.Linq;
using UnityEngine.Events;
using System;

public class SelectionManager : Singleton<SelectionManager>
{
    #region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structs
    #endregion

    #region Classes
    [Serializable]
    public class SelectedChangedEvent : UnityEvent<BaseObject> { }
    #endregion

    #region Fiedls
    private BaseObject _selected;

    private VectorLine _line;
    #endregion

    #region Events
    public SelectedChangedEvent SelectedChanged;
    #endregion

    #region Properties
    public BaseObject Selected
    {
        get { return _selected; }
        set
        {
            if (_selected == value)
            {
                return;
            }

            _selected = value;

            SetLineActive(_selected != null);

            SelectedChanged.Invoke(_selected);
        }
    }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    private void Start()
    {
        _line = new VectorLine("SelectedLine", Enumerable.Repeat(Vector2.zero, 5).ToList(), 2f);
        _line.lineType = LineType.Continuous;
        _line.color = new Color32(255, 255, 0, 255);
        _line.active = false;
    }

    private void SetLineActive(bool state)
    {
        _line.active = state;
    }

    private void LateUpdate()
    {
        RedrawSelectedScreenBound();
    }

    private void RedrawSelectedScreenBound()
    {
        if (_line.active == false)
        {
            return;
        }

        Bounds? bounds = _selected.GetBounds();

        float minX = bounds.Value.center.x - bounds.Value.extents.x;
        float maxX = bounds.Value.center.x + bounds.Value.extents.x;
        float minY = bounds.Value.center.y - bounds.Value.extents.y;
        float maxY = bounds.Value.center.y + bounds.Value.extents.y;
        float minZ = bounds.Value.center.z - bounds.Value.extents.z;
        float maxZ = bounds.Value.center.z + bounds.Value.extents.z;

        Vector3 zero = Camera.main.WorldToScreenPoint(new Vector3(minX, maxY, maxZ));
        Vector3 one = Camera.main.WorldToScreenPoint(new Vector3(maxX, maxY, maxZ));
        Vector3 two = Camera.main.WorldToScreenPoint(new Vector3(maxX, maxY, minZ));
        Vector3 three = Camera.main.WorldToScreenPoint(new Vector3(minX, maxY, minZ));
        Vector3 four = Camera.main.WorldToScreenPoint(new Vector3(minX, minY, maxZ));
        Vector3 five = Camera.main.WorldToScreenPoint(new Vector3(maxX, minY, maxZ));
        Vector3 six = Camera.main.WorldToScreenPoint(new Vector3(maxX, minY, minZ));
        Vector3 seven = Camera.main.WorldToScreenPoint(new Vector3(minX, minY, minZ));

        Vector3[] points = { zero, one, two, three, four, five, six, seven };

        Vector2 minPoint = points[0];
        Vector2 maxPoint = points[0];

        foreach (Vector3 point in points)
        {
            if (point.x < minPoint.x)
            {
                minPoint.x = point.x;
            }

            if (point.y < minPoint.y)
            {
                minPoint.y = point.y;
            }

            if (point.x > maxPoint.x)
            {
                maxPoint.x = point.x;
            }

            if (point.y > maxPoint.y)
            {
                maxPoint.y = point.y;
            }
        }

        _line.MakeRect(minPoint, maxPoint);
        _line.Draw();
    }

    private void SelectFromScreen(bool isMouseInViewport, Vector2 position)
    {
        if (!isMouseInViewport)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        // States:
        // 0 - no raycast;
        // 1 - raycast to non-selectable;
        // 2 - raycast to selectable.
        int raycastResult = 0;

        if (Physics.Raycast(ray, out hitInfo, 64f))
        {
            raycastResult = 1;

            if (hitInfo.collider.GetComponent<BaseObject>() != null)
            {
                raycastResult = 2;
            }
        }

        switch (raycastResult)
        {
            // No raycast or raycast to non-selectable.
            case 0:
            case 1:
                Selected = null;
                break;
            // Raycast to selectable.
            case 2:
                Selected = hitInfo.collider.GetComponent<BaseObject>();
                break;
        }
    }
    #endregion

    #region Event handlers
    public void InputManager_MouseLeftButtonClicked(InputManager.MouseInformation mouseInfo)
    {
        SelectFromScreen(mouseInfo.IsMouseInViewport, mouseInfo.Position);
    }
    #endregion
}
