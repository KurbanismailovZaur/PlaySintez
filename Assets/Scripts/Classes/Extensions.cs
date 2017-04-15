using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
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
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Constructors
    #endregion

    #region Methods
    public static bool CompareByCoordinates(this Vector2 a, Vector2 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }

    public static bool CompareByCoordinates(this Vector3 a, Vector3 b)
    {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
    }

    public static void RemoveAllChilds(this Transform transform)
    {
        Transform[] childs = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }

        foreach (Transform child in childs)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static void RemoveAllChilds(this RectTransform rectTransform)
    {
        ((Transform)rectTransform).RemoveAllChilds();
    }

    public static void DetachChild(this Transform transform, int index)
    {
        if (index >= transform.childCount)
        {
            Debug.LogError("Index out of child count");
            return;
        }

        transform.GetChild(index).SetParent(null);
    }

    public static void DetachChild(this RectTransform rectTransform, int index)
    {
        ((Transform)rectTransform).DetachChild(index);
    }
    #endregion

    #region Event handlers
    #endregion
}
