﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class LookCapsule : Capsule
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
        private const ModuleType _moduleType = ModuleType.LookCapsule;
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public override ModuleType GetModuleType()
        {
            return _moduleType;
        }
        #endregion

        #region Event handlers
        #endregion
    }
}