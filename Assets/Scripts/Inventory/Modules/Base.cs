using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Base : Element
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
        private bool _isInitialized;
        private int _pureEnergy;
        #endregion

        #region Events
        #endregion

        #region Properties
        public int PureEnergy { get { return _pureEnergy; } }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public override ModuleType GetModuleType()
        {
            return ModuleType.Base;
        }

        public void Initialize(Modules.Base baseModule)
        {
            if (_isInitialized)
            {
                return;
            }

            _pureEnergy = baseModule.PureEnergy;

            _isInitialized = true;
        }
        #endregion

        #region Event handlers
        #endregion
    }
}