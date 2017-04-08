using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Module : BaseObject
{
    #region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structs
    public struct State
    {

    }
    #endregion

    #region Classes
    [Serializable]
    public class StateChangedEvent : UnityEvent<State> { }
    #endregion

    #region Fiedls
    #endregion

    #region Events
    public StateChangedEvent StateChanged;
    #endregion

    #region Properties
    #endregion

    #region Constructors
    #endregion

    #region Methods
    public State GetState()
    {
        return new State();
    }
    #endregion

    #region Event handlers
    #endregion
}
