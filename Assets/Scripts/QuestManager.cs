using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

public class QuestManager : Singleton<QuestManager> 
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
    private InventoryController _inventory;

    [SerializeField]
    private EmulationController _emulation;

    [SerializeField]
    private TaskController _task;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

	#region Constructors
	#endregion
	
    #region Methods
    #endregion

    #region Event handlers
    public void Star_LevelIncremented(byte level)
    {
        if (level == 1)
        {
            Capsule lookCapsule = Instantiate(Resources.Load<Capsule>("Inventory/InventoryModules/LookCapsule"));
            _inventory.AddElement(lookCapsule);

            _emulation.SetInteractableState(false);
            _task.SetTaskDescription("Set capsule to socket");
        }
    }
    #endregion
}
