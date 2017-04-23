using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseInformation : MonoBehaviour 
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
    private Text _pureEnergyValueText;

    [SerializeField]
    private Button _convertButton;

    [SerializeField]
    private Button _droneButton;
    #endregion

    #region Events
    #endregion

    #region Properties
    public Button ConvertButton { get { return _convertButton; } }
    public Button DroneButton { get { return _droneButton; } }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    public void UpdateInformationAboutBase(Modules.Base baseModule)
    {
        _pureEnergyValueText.text = baseModule.PureEnergy.ToString();
    }

    public void ConvertDirtyEnergy()
    {
        Modules.Base baseModule = (Modules.Base)SelectionManager.Instance.Selected;

        if (baseModule.Socket.LinkedSocket != null && baseModule.Socket.LinkedSocket.ConnectedModule != null && baseModule.Socket.LinkedSocket.ConnectedModule is Modules.Capsule)
        {
            Modules.Capsule capsule = (Modules.Capsule)baseModule.Socket.LinkedSocket.ConnectedModule;

            if (capsule.Energy > 0)
            {
                baseModule.PureEnergy += capsule.Energy / 3;
                capsule.Energy = 0;
            }
        }
    }
    #endregion

    #region Event handlers
    #endregion
}
