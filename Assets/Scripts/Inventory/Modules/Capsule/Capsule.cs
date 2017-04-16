using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Inventory
{
    public class Capsule : Element
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
        [SerializeField]
        private ModuleType _moduleType;

        private int _capacity = 32;
        private int _energy;

        private bool _isInitialized;
        #endregion

        #region Events
        #endregion

        #region Properties
        public int Capacity { get { return _capacity; } }
        public int Energy { get { return _energy; } }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public override ModuleType GetModuleType()
        {
            return _moduleType;
        }

        public void Initialize(Modules.Capsule capsule)
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