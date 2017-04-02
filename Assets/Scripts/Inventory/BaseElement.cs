using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public abstract class BaseElement : MonoBehaviour
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
        private ElementType _elementType;
        #endregion

        #region Events
        #endregion

        #region Properties
        public ElementType ElementType { get { return _elementType; } }
        #endregion

        #region Constructors
        #endregion

        #region Methods
        #endregion

        #region Event handlers
        #endregion
    }
}