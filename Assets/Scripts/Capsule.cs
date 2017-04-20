using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Modules
{
    public class Capsule : Module, ILevelable
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

        private byte _level;
        private float _levelProgress;
        #endregion

        #region Events
        public StateChangedEvent StateChanged;
        #endregion

        #region Properties
        public EnergyType CapsuleEnergyType { get { return _energyType; } }

        public int Capacity { get { return _capacity; } }
        public int Energy { get { return _energy; } }
        public byte Level { get { return _level; } }
        public float LevelProgress { get { return _levelProgress; } }
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

            _level = capsule.Level;
            _levelProgress = capsule.LevelProgress;

            _isInitialized = true;
        }

        public void IncreaseLevelProgress()
        {
            _levelProgress += 1f / Mathf.Pow(_level + 1, 1.5f);

            StateChanged.Invoke(this);

            if (_levelProgress >= 1f)
            {
                LevelUpCapsule();
            }
        }

        private void LevelUpCapsule()
        {
            _levelProgress = 0f;
            _level += 1;

            StateChanged.Invoke(this);
        }
        #endregion

        #region Event handlers
        #endregion
    }
}