using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules
{
    public class Base : Module
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
        private bool _isInitialized;
        #endregion

        #region Events
        public StateChangedEvent StateChanged;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void Initialize(Inventory.Element baseElement)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;


        }
        #endregion

        #region Event handlers
        #endregion
    }
}