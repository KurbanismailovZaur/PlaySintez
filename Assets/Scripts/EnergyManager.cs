using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnergyManager : Singleton<EnergyManager>
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
    private Star _star;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Methods
    #endregion

    #region Event handlers
    public void LookVideo()
    {
        _star.TakeEnergy(new EnergyUnit(EnergyUnit.EnergyType.Video, EnergyUnit.EnergyOwner.Me));
    }

    public void MakeComment()
    {
        _star.TakeEnergy(new EnergyUnit(EnergyUnit.EnergyType.Comment, EnergyUnit.EnergyOwner.Me));
    }

    public void MakeLike()
    {
        _star.TakeEnergy(new EnergyUnit(EnergyUnit.EnergyType.Like, EnergyUnit.EnergyOwner.Me));
    }

    public void LookMyVideo()
    {
        _star.TakeEnergy(new EnergyUnit(EnergyUnit.EnergyType.Video, EnergyUnit.EnergyOwner.Other));
    }

    public void MakeMeComment()
    {
        _star.TakeEnergy(new EnergyUnit(EnergyUnit.EnergyType.Video, EnergyUnit.EnergyOwner.Other));
    }

    public void MakeMeLike()
    {
        _star.TakeEnergy(new EnergyUnit(EnergyUnit.EnergyType.Video, EnergyUnit.EnergyOwner.Other));
    }
    #endregion
}
