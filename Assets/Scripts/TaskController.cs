using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour 
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
    private Text _taskText;
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

	#region Constructors
	#endregion
	
    #region Methods
    public void SetTaskDescription(string description)
    {
        _taskText.text = description;
    }
    #endregion

    #region Event handlers
    #endregion
}
