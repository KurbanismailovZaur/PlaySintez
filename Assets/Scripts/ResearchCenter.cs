using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules
{
    public class ResearchCenter : Module
    {
        #region Enums
        #endregion

        #region Delegates
        #endregion

        #region Structs
        #endregion

        #region Classes
        [Serializable]
        public class StateChangedEvent : UnityEvent<Base> { }
        #endregion

        #region Fiedls
        public StateChangedEvent StateChanged;
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region Event handlers
        #endregion
    }
}