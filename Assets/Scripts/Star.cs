using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshRenderer))]
public class Star : SelectableMonoBehaviour
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
    #endregion

    #region Events
    #endregion

    #region Properties
    public byte Level { get { return _level; } }
    public float LevelProgress { get { return _levelProgress; } }
    #endregion

    #region Methods
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.EnableKeyword("_EMISSION");
    }

    public void TakeEnergy(Energy energy)
    {
        switch(energy.Owner)
        {
            case Energy.EnergyOwner.Me:
                IncreaseStarEnergyProgress();
                break;
            case Energy.EnergyOwner.Other:
                IncreaseStarLevelProgress();
                break;
        }
    }

    private void IncreaseStarEnergyProgress()
    {
        _energyProgress += 1f / Mathf.Pow(_energy + 1, 2);

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
    }

    private void IncreaseStarLevelProgress()
    {
        if (_energy == 0)
        {
            return;
        }

        _levelProgress += 1f / Mathf.Pow(_level + 1, 2);

        if(_levelProgress >= 1f)
        {
            LevelUpStar();
        }
    }

    private void LevelUpStar()
    {
        _levelProgress = 0f;
        _level += 1;
    }
    #endregion

    #region Event handlers
    #endregion
}
