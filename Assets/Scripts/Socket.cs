using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Serializable]
    public class StateChangedEvent : UnityEvent<Socket> { }
    #endregion

    #region Fiedls
    private Orbit _orbit;

    private SphereCollider _collider;
    private MeshRenderer _renderer;

    [SerializeField]
    private Module _connectedModule;

    private bool _isEmpty = true;
    private bool _isInitialized;
    private Socket _linkedSocket;
    #endregion

    #region Events
    public ModuleChangedEvent ModuleChanged = new ModuleChangedEvent();
    public StateChangedEvent StateChanged;
    #endregion

    #region Properties
    public Orbit SocketOrbit { get { return _orbit; } }
    public Module ConnectedModule { get { return _connectedModule; } }

    public Socket LinkedSocket
    {
        get { return _linkedSocket; }
        set
        {
            _linkedSocket = value;
        }
    }
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

    public void SendLevelProgressIncreasingToModule()
    {
        if (_connectedModule != null)
        {
            ILevelable levelable = _connectedModule as ILevelable;

            if (levelable != null)
            {
                levelable.IncreaseLevelProgress();
            }
        }
    }

    public void SetOrbit(Orbit orbit)
    {
        if (!_isInitialized)
        {
            _orbit = orbit;
            _isInitialized = true;
        }
        else
        {
            Debug.LogError("Orbit already initialized");
        }
    }
    #endregion

    #region Event handlers
    #endregion
}
