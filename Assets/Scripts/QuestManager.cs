using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;
using System;
using UnityEngine.Events;

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
    private int _currentTaskIndex;

    private List<Action> _tasks = new List<Action>();

    [SerializeField]
    private InventoryController _inventory;

    [SerializeField]
    private EmulationController _emulation;

    [SerializeField]
    private TaskController _taskController;

    #region Dependencies
    [SerializeField]
    private Star _star;
    #endregion
    #endregion

    #region Events
    #endregion

    #region Properties
    #endregion

    #region Constructors
    #endregion

    #region Methods
    private void AddTask(Action task)
    {
        _tasks.Add(task);
    }

    private void Start()
    {
        #region Tasks
        // 0
        AddTask(() =>
        {
            UnityAction<byte> action = null;
            action = new UnityAction<byte>((byte level) =>
            {
                if (level == 1)
                {
                    _star.LevelIncremented.RemoveListener(action);
                    PerformCurrentTask();
                }
            });

            _star.LevelIncremented.AddListener(action);
        });

        // 1
        AddTask(() =>
        {
            Capsule lookCapsule = Instantiate(Resources.Load<Capsule>("Inventory/InventoryModules/LookCapsule"));
            _inventory.AddElement(lookCapsule);

            Capsule commentCapsule = Instantiate(Resources.Load<Capsule>("Inventory/InventoryModules/CommentCapsule"));
            _inventory.AddElement(commentCapsule);

            Capsule likeCapsule = Instantiate(Resources.Load<Capsule>("Inventory/InventoryModules/LikeCapsule"));
            _inventory.AddElement(likeCapsule);

            _emulation.SetInteractableState(false);
            _taskController.SetTaskDescription("Set capsule to socket");

            UnityAction<Star, Orbit, Socket, Module> action = null;
            action = new UnityAction<Star, Orbit, Socket, Module>((star, orb, soc, mod) =>
            {
                _star.OrbitSocketModuleChanged.RemoveListener(action);
                PerformCurrentTask();
            });

            _star.OrbitSocketModuleChanged.AddListener(action);
        });

        // 2
        AddTask(() =>
        {
            _emulation.SetInteractableState(true);

            Modules.Capsule capsule = FindObjectOfType<Modules.Capsule>();

            Action<Modules.Capsule> levelUpedHandler = null;
            levelUpedHandler = (c) =>
            {
                capsule.LevelUped.RemoveListener(new UnityAction<Modules.Capsule>(levelUpedHandler));

                capsule.Socket.SocketOrbit.LevelUpOrbit();
                _star.LevelUpStar();
            };

            capsule.LevelUped.AddListener(new UnityAction<Modules.Capsule>(levelUpedHandler));
            _taskController.SetTaskDescription("Earn neseccary energy");
        });
        #endregion

        PerformCurrentTask();
    }

    private void PerformCurrentTask()
    {
        if (_currentTaskIndex >= _tasks.Count)
        {
            Debug.LogErrorFormat("Task with index {0} is not exist", _currentTaskIndex);
            return;
        }

        _tasks[_currentTaskIndex++].Invoke();
    }
    #endregion

    #region Event handlers
    #endregion
}
