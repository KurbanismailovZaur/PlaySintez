using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableMonoBehaviour : MonoBehaviour, ISelectable
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
    public void OnSelect()
    {

    }

    public Bounds? GetBounds()
    {
        Bounds? bounds = null;

        CalculateBounds(ref bounds, transform);

        return bounds;
    }

    private void CalculateBounds(ref Bounds? bounds, Transform parent)
    {
        Renderer renderer = parent.GetComponent<Renderer>();

        if (renderer != null)
        {
            if (bounds == null)
            {
                bounds = renderer.bounds;
            }
            else
            {
                bounds.Value.Encapsulate(renderer.bounds);
            }
        }

        foreach (Transform child in parent)
        {
            CalculateBounds(ref bounds, child);
        }
    }
    #endregion

    #region Event handlers
    #endregion
}
