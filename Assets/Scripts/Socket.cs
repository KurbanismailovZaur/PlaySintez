using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Socket : MonoBehaviour 
{
    #region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structs
    #endregion

    #region Classes
    [Serializable]
    public class ModuleChangedEvent : UnityEvent<Socket, Module> { }
    #endregion

    #region Fiedls
    private SphereCollider _collider;
    private MeshRenderer _renderer;

    [SerializeField]
    private BaseObject _connectedModule;

    private bool _isEmpty = true;
    #endregion

    #region Events
    public ModuleChangedEvent ModuleChanged = new ModuleChangedEvent();
    #endregion

    #region Properties
    #endregion

    #region Constructors
    #endregion

    #region Methods
    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public bool InstallModule(Module module)
    {
        if (!_isEmpty)
        {
            Debug.LogError("Socket is not empty");
            return false;
        }

        module.transform.SetParent(transform, false);
        _connectedModule = module;

        module.Socket = this;

        _collider.enabled = false;
        _renderer.enabled = false;

        ModuleChanged.Invoke(this, module);

        return true;
    }

    public void RemoveModule()
    {
        _connectedModule.transform.SetParent(null);
        _connectedModule = null;

        _collider.enabled = true;
        _renderer.enabled = true;

        ModuleChanged.Invoke(this, null);
    }
    #endregion

    #region Event handlers
    #endregion
}
