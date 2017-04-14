using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : BaseObject
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
    #endregion

    #region Events
    #endregion

    #region Properties
    public EnergyType CapsuleEnergyType { get { return _energyType; } }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    #endregion

    #region Event handlers
    #endregion
}
