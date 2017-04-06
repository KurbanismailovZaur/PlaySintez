using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseObject : BoundableObject
{
    #region Enums
    public enum ObjectType
    {
        Star,
        Orbit,
        Module
    }
    #endregion

    #region Delegates
    #endregion

    #region Structs
    #endregion

    #region Classes
    #endregion

    #region Fiedls
    [SerializeField]
    private ObjectType _type;
    #endregion

    #region Events
    #endregion

    #region Properties
    public ObjectType Type { get { return _type; } }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    #endregion

    #region Event handlers
    #endregion
}
