using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Module : BaseObject
{
    #region Enums
    public enum Type
    {
        LookCapsule,
        CommentCapsule,
        LikeCapsule,
        Base,
        ResearchCenter
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
    private Type _moduleType;
    #endregion

    #region Events
    #endregion

    #region Properties
    public Type ModuleType { get { return _moduleType; } }
    public Socket Socket { get; set; }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    #endregion

    #region Event handlers
    #endregion
}
