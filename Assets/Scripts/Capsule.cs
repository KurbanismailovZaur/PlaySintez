using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules
{
    public class Capsule : Module
    {
        #region Enums
        public enum EnergyType
        {
            Look,
            Comment,
            Like
        }
        #endregion

        #region Delegates
        #endregion

        #region Structs
        #endregion

        #region Classes
        [Serializable]
        public class StateChangedEvent : UnityEvent<Capsule> { }
        #endregion

        #region Fiedls
        [SerializeField]
        private EnergyType _energyType;

        private int _capacity = 32;
        private int _energy;

        private bool _isInitialized;
        #endregion

        #region Events
        public StateChangedEvent StateChanged;
        #endregion

        #region Properties
        public EnergyType CapsuleEnergyType { get { return _energyType; } }
        public int Capacity { get { return _capacity; } }
        public int Energy { get { return _energy; } }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void Initialize(Inventory.Capsule capsule)
        {
            if (_isInitialized)
            {
                return;
            }

            _capacity = capsule.Capacity;
            _energy = capsule.Energy;

            _isInitialized = true;
        }
        #endregion

        #region Event handlers
        #endregion
    }
}