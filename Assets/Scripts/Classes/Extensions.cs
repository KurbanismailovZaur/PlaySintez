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
    #endregion

    #region Event handlers
    #endregion
}
