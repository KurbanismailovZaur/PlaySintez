﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy 
{
    #region Enums
    public enum EnergyType
    {
        Video,
        Comment,
        Like
    }

    public enum EnergyOwner
    {
        Me,
        Other
    }
    #endregion

    #region Delegates
    #endregion

    #region Structs
    #endregion

    #region Classes
    #endregion

    #region Fiedls
    private EnergyType _type;
    private EnergyOwner _owner;
    #endregion

    #region Events
    #endregion

    #region Properties
    public EnergyType Type { get { return _type; } }
    public EnergyOwner Owner { get { return _owner; } }
    #endregion

    #region Constructors
    public Energy(EnergyType energyType, EnergyOwner energyOwner)
    {
        _type = energyType;
        _owner = energyOwner;
    }
    #endregion

    #region Methods
    #endregion

    #region Event handlers
    #endregion
}
