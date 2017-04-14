using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public abstract class Element : MonoBehaviour
    {
        #region Enums
        public enum ModuleType
        {
            LookCapsule,
            CommentCapsule,
            LikeCapsule,
            Base,
            ResearchCenter
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
        public abstract ModuleType GetModuleType();
        #endregion

        #region Event handlers
        #endregion
    }
}