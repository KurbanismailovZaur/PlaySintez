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
        _star.TakeEnergy(new Energy(Energy.EnergyType.Video, Energy.EnergyOwner.Me));
    }

    public void MakeComment()
    {
        _star.TakeEnergy(new Energy(Energy.EnergyType.Comment, Energy.EnergyOwner.Me));
    }

    public void MakeLike()
    {
        _star.TakeEnergy(new Energy(Energy.EnergyType.Like, Energy.EnergyOwner.Me));
    }

    public void LookMyVideo()
    {
        _star.TakeEnergy(new Energy(Energy.EnergyType.Video, Energy.EnergyOwner.Other));
    }

    public void MakeMeComment()
    {
        _star.TakeEnergy(new Energy(Energy.EnergyType.Video, Energy.EnergyOwner.Other));
    }

    public void MakeMeLike()
    {
        _star.TakeEnergy(new Energy(Energy.EnergyType.Video, Energy.EnergyOwner.Other));
    }
    #endregion
}
