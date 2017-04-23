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

        private int _pureEnergy;
        #endregion

        #region Events
        public StateChangedEvent StateChanged;
        #endregion

        #region Properties
        public int PureEnergy
        {
            get { return _pureEnergy; }
            set
            {
                _pureEnergy = value;

                StateChanged.Invoke(this);
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void Initialize(Inventory.Element element)
        {
            if (_isInitialized)
            {
                return;
            }

            Inventory.Base baseElement = (Inventory.Base)element;

            _pureEnergy = baseElement.PureEnergy;

            _isInitialized = true;
        }
        #endregion

        #region Event handlers
        #endregion
    }
}