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
            LookCapsule lookCapsule = Instantiate(Resources.Load<LookCapsule>("Inventory/InventoryModules/LookCapsule"));
            _inventory.AddElement(lookCapsule);

            CommentCapsule commentCapsule = Instantiate(Resources.Load<CommentCapsule>("Inventory/InventoryModules/CommentCapsule"));
            _inventory.AddElement(commentCapsule);

            LikeCapsule likeCapsule = Instantiate(Resources.Load<LikeCapsule>("Inventory/InventoryModules/LikeCapsule"));
            _inventory.AddElement(likeCapsule);

            _emulation.SetInteractableState(false);
            _task.SetTaskDescription("Set capsule to socket");
        }
    }
    #endregion
}
