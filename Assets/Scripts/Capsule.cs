using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        #endregion

        #region Fiedls
        [SerializeField]
        private EnergyType _energyType;

        private bool _isInitialized;
        #endregion

        #region Events
        #endregion

        #region Properties
        public EnergyType CapsuleEnergyType { get { return _energyType; } }
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



            _isInitialized = true;
        }
        #endregion

        #region Event handlers
        #endregion
    }
}