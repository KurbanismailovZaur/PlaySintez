using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    #region Fiedls
    private SphereCollider _collider;
    private MeshRenderer _renderer;

    [SerializeField]
    private BaseObject _connectedModule;

    private bool _isEmpty = true;
    #endregion

    #region Events
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

        return true;
    }

    public void RemoveModule()
    {
        _connectedModule.transform.SetParent(null);
        _connectedModule = null;

        _collider.enabled = true;
        _renderer.enabled = true;
    }
    #endregion

    #region Event handlers
    #endregion
}
