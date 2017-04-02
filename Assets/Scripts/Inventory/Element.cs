using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Element : BaseElement
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
        private Image _icon;

        [SerializeField]
        private Text _text;
        #endregion

        #region Events
        #endregion

        #region Properties
        public Sprite Icon
        {
            set { _icon.sprite = value; }
        }

        public string Name
        {
            get { return _text.text; }
            set { _text.text = value.Substring(0, 8); }
        }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region Event handlers
        #endregion
    }
}