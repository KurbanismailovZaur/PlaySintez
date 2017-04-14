using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

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

    public struct State
    {
        private byte _energy;
        private float _energyProgress;
        private byte _level;
        private float _levelProgress;

        public byte Energy { get { return _energy; } }
        public float EnergyProgress { get { return _energyProgress; } }
        public byte Level { get { return _level; } }
        public float LevelProgress { get { return _levelProgress; } }

        public State(byte energy, float energyProgress, byte level, float levelProgress)
        {
            _energy = energy;
            _energyProgress = energyProgress;
            _level = level;
            _levelProgress = levelProgress;
        }
    }
    #endregion

    #region Classes
    [Serializable]
    public class StateChangedEvent : UnityEvent<State> { }

    [Serializable]
    public class LevelIncrementedEvent : UnityEvent<byte> { }
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
    #endregion

    #region Events
    public StateChangedEvent StateChanged;
    public LevelIncrementedEvent LevelIncremented;
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

        StateChanged.Invoke(GetState());

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
            IncreaseStarLevelProgress();
        }

        int index = Mathf.Clamp(_energy, 0, _colorsPerEnergy.Length - 1);
        _material.DOColor(_colorsPerEnergy[index].color, "_Color", _animationDuration).Play();
        _material.DOColor(_colorsPerEnergy[index].emissionColor, "_EmissionColor", _animationDuration).Play();

        StateChanged.Invoke(GetState());
    }

    private void IncreaseStarLevelProgress()
    {
        if (_energy == 0)
        {
            return;
        }

        _levelProgress += 1f / Mathf.Pow(_level + 1, 2);

        StateChanged.Invoke(GetState());

        if (_levelProgress >= 1f)
        {
            LevelUpStar();
        }
    }

    private void LevelUpStar()
    {
        _levelProgress = 0f;
        _level += 1;

        CreateNewOrbit(_level + 1);

        StateChanged.Invoke(GetState());
        LevelIncremented.Invoke(_level);
    }

    private void CreateNewOrbit(float radius)
    {
        Orbit.Factory orbitFactory = new Orbit.Factory();
        Orbit orbit = orbitFactory.Create(radius);

        orbit.transform.SetParent(_orbitsContainer, false);
        _orbits.Add(orbit);
    }

    public State GetState()
    {
        return new State(_energy, _energyProgress, _level, _levelProgress);
    }
    #endregion

    #region Event handlers
    #endregion
}
