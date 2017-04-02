using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vectrosity;
using System.Linq;

public class SelectionManager : Singleton<SelectionManager>
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
    private SelectableMonoBehaviour _selected;

    private VectorLine _line;
    #endregion

    #region Events
    #endregion

    #region Properties
    public SelectableMonoBehaviour Selected
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
        }
    }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    private void Start()
    {
        SelectableMonoBehaviour.Selected += (selectable) => { Selected = selectable; };

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
    #endregion

    #region Event handlers
    #endregion
}
