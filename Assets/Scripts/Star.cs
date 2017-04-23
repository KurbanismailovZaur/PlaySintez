using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;
using System.Linq;

[RequireComponent(typeof(MeshRenderer))]
public class Star : BaseObject
{
    #region Enums
    #endregion

    #region Delegates
    #endregion

    #region Structs
    [System.Serializable]
    public struct StarColorBlock
    {
        public Color color;

        [ColorUsage(false, true, 0f, 1f, 0.125f, 3)]
        public Color emissionColor;
    }
    #endregion

    #region Classes
    [Serializable]
    public class StateChangedEvent : UnityEvent<Star> { }

    [Serializable]
    public class LevelIncrementedEvent : UnityEvent<byte> { }

    [Serializable]
    public class OrbitSocketModuleChagedEvent : UnityEvent<Star, Orbit, Socket, Module> { }

    [Serializable]
    public class ModuleRemovedEvent : UnityEvent<Module> { }

    [Serializable]
    public class ModulesLinkedEvent : UnityEvent<Module, Module> { }
    #endregion

    #region Fiedls
    private byte _energy;

    private float _energyProgress;

    [SerializeField]
    private StarColorBlock[] _colorsPerEnergy;

    [SerializeField]
    private byte _level;

    private float _levelProgress;

    private Material _material;

    [SerializeField]
    private float _animationDuration = 1f;

    [SerializeField]
    private Transform _orbitsContainer;

    private List<Orbit> _orbits = new List<Orbit>();

    [SerializeField]
    private Inventory.InventoryController _inventoryController;
    #endregion

    #region Events
    public StateChangedEvent StateChanged;
    public LevelIncrementedEvent LevelIncremented;
    public OrbitSocketModuleChagedEvent OrbitSocketModuleChanged;
    public ModuleRemovedEvent moduleRemoved;
    public ModulesLinkedEvent modulesLinked;
    #endregion

    #region Properties
    public byte Energy { get { return _energy; } }
    public float EnergyProgress { get { return _energyProgress; } }

    public byte Level { get { return _level; } }
    public float LevelProgress { get { return _levelProgress; } }
    #endregion

    #region Methods
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.EnableKeyword("_EMISSION");
    }

    public void TakeEnergy(EnergyUnit energy)
    {
        switch(energy.Owner)
        {
            case EnergyUnit.EnergyOwner.Me:
                IncreaseStarEnergyProgress();
                break;
            case EnergyUnit.EnergyOwner.Other:
                IncreaseStarLevelProgress();
                break;
        }
    }

    private void IncreaseStarEnergyProgress()
    {
        _energyProgress += 1f / Mathf.Pow(_energy + 1, 2);

        StateChanged.Invoke(this);

        if (_energyProgress >= 1f)
        {
            EnerguUpStar();
        }
    }

    private void EnerguUpStar()
    {
        _energyProgress = 0f;

        _energy += 1;

        if (_energy == 1)
        {
            LevelUpStar();
        }

        int index = Mathf.Clamp(_energy, 0, _colorsPerEnergy.Length - 1);
        _material.DOColor(_colorsPerEnergy[index].color, "_Color", _animationDuration).Play();
        _material.DOColor(_colorsPerEnergy[index].emissionColor, "_EmissionColor", _animationDuration).Play();

        StateChanged.Invoke(this);
    }

    private void IncreaseStarLevelProgress()
    {
        if (_energy == 0)
        {
            return;
        }

        _levelProgress += 1f / Mathf.Pow(_level + 2, 2);

        StateChanged.Invoke(this);

        if (_levelProgress >= 1f)
        {
            LevelUpStar();
        }

        SendLevelProgressIncreasingToAllOrbits();
    }

    private void SendLevelProgressIncreasingToAllOrbits()
    {
        List<Orbit> orbits = new List<Orbit>();
        orbits.AddRange(_orbits);

        foreach (Orbit orbit in orbits)
        {
            orbit.IncreaseLevelProgress();
        }
    }

    public void LevelUpStar()
    {
        _levelProgress = 0f;
        _level += 1;

        CreateNewOrbit(_level + 1);

        StateChanged.Invoke(this);
        LevelIncremented.Invoke(_level);
    }

    private void CreateNewOrbit(float radius)
    {
        Orbit.Factory orbitFactory = new Orbit.Factory();
        Orbit orbit = orbitFactory.Create(this, radius);

        orbit.SocketModuleChanged.AddListener(Orbit_SocketModuleChanged);

        orbit.transform.SetParent(_orbitsContainer, false);
        _orbits.Add(orbit);

    }

    private void InstallModuleFromInventory(Inventory.Element element, Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        RaycastHit hitInfo;
        bool isHited = Physics.Raycast(ray, out hitInfo);

        if (!isHited)
        {
            return;
        }

        Socket socket = hitInfo.collider.GetComponent<Socket>();

        if (socket == null)
        {
            return;
        }

        Orbit.PrefixType? orbitLastPrefixType = null;

        if (socket.SocketOrbit.Prefixes.Count != 0)
        {
            orbitLastPrefixType = socket.SocketOrbit.Prefixes.Last();
        }

        if (orbitLastPrefixType.HasValue)
        {
            switch (element.GetModuleType())
            {
                case Inventory.Element.ModuleType.LookCapsule:
                    if (orbitLastPrefixType != Orbit.PrefixType.R)
                    {
                        return;
                    }

                    break;
                case Inventory.Element.ModuleType.CommentCapsule:
                    if (orbitLastPrefixType != Orbit.PrefixType.G)
                    {
                        return;
                    }

                    break;
                case Inventory.Element.ModuleType.LikeCapsule:
                    if (orbitLastPrefixType != Orbit.PrefixType.B)
                    {
                        return;
                    }

                    break;
                default:
                    if (orbitLastPrefixType != Orbit.PrefixType.A)
                    {
                        return;
                    }
                    return;
            }
        }

        Module module = Converter.Instance.ConvertToModule(element);

        if (socket.InstallModule(module))
        {
            _inventoryController.RemoveElement(element);

            if (module.Socket.LinkedSocket != null && module.Socket.LinkedSocket.ConnectedModule != null)
            {
                modulesLinked.Invoke(module, module.Socket.LinkedSocket.ConnectedModule);
            }
        }
    }

    private void RemoveModule(Module module)
    {
        module.Socket.RemoveModule();

        Destroy(module.gameObject);
    }

    private void PutCurrentSelectedModuleInInventory()
    {
        BaseObject baseObject = SelectionManager.Instance.Selected;

        if (baseObject.BaseObjectType != ObjectType.Module)
        {
            return;
        }

        PutModuleInInventory((Module)baseObject);
    }

    public void PutModuleInInventory(Module module)
    {
        switch (module.ModuleType)
        {
            case Module.Type.LookCapsule:
                MoveCapsuleToInventory(module, "Inventory/InventoryModules/LookCapsule");
                break;
            case Module.Type.CommentCapsule:
                MoveCapsuleToInventory(module, "Inventory/InventoryModules/CommentCapsule");
                break;
            case Module.Type.LikeCapsule:
                MoveCapsuleToInventory(module, "Inventory/InventoryModules/LikeCapsule");
                break;
            case Module.Type.Base:
                break;
            case Module.Type.ResearchCenter:
                break;
        }

        moduleRemoved.Invoke(module);
    }

    private void MoveCapsuleToInventory(Module module, string pathToInventoryElement)
    {
        Inventory.Capsule lookCapsule = Instantiate(Resources.Load<Inventory.Capsule>(pathToInventoryElement));
        lookCapsule.Initialize((Modules.Capsule)module);

        _inventoryController.AddElement(lookCapsule);

        RemoveModule(module);
    }
    #endregion

    #region Event handlers
    public void Inventory_ElementDroped(Inventory.Element element, Vector2 screenPosition)
    {
        InstallModuleFromInventory(element, screenPosition);
    }

    public void InformationController_PutModuleInInventory()
    {
        PutCurrentSelectedModuleInInventory();
    }

    public void Orbit_SocketModuleChanged(Orbit orbit, Socket socket, Module module)
    {
        OrbitSocketModuleChanged.Invoke(this, orbit, socket, module);
    }
    #endregion
}
