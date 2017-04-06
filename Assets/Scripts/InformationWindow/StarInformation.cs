using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InformationWindow
{
    public class StarInformation : MonoBehaviour
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
        private Text _energyValueText;

        [SerializeField]
        private Text _energyProgressValueText;

        [SerializeField]
        private Text _levelValueText;

        [SerializeField]
        private Text _levelProgressValueText;
        #endregion

        #region Events
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods
        public void UpdateInformationAboutStar(Star.State state)
        {
            _energyValueText.text = state.Energy.ToString();
            _energyProgressValueText.text = string.Format("{0:F}", state.EnergyProgress);

            _levelValueText.text = state.Level.ToString();
            _levelProgressValueText.text = string.Format("{0:F}", state.LevelProgress);
        }
        #endregion

        #region Event handlers
        #endregion
    }
}