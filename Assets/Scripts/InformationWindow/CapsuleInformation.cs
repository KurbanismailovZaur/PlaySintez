﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InformationWindow
{
    public class CapsuleInformation : MonoBehaviour
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
        private Text _energyTypeText;

        [SerializeField]
        private Text _capacityText;

        [SerializeField]
        private Text _energyText;
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void UpdateInformationAboutCapsule(Modules.Capsule capsule)
        {
            _energyTypeText.text = capsule.CapsuleEnergyType.ToString();
            _capacityText.text = capsule.Capacity.ToString();
            _energyText.text = capsule.Energy.ToString();
        }
        #endregion

        #region Event handlers
        #endregion
    }
}