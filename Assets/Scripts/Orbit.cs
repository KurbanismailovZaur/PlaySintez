using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vectrosity;

public class Orbit : BaseObject
{
    #region Enums
    public enum PrefixType
    {
        R,
        G,
        B,
        A
    }
    #endregion

    #region Delegates
    #endregion

    #region Structs
    public struct State
    {

    }
    #endregion

    #region Classes
    public class Factory
    {
        public Orbit Create(float radius, PrefixType? prefix = null)
        {
            Orbit orbit = new GameObject("Orbit").AddComponent<Orbit>();
            orbit.Type = ObjectType.Orbit;

            int orbitSegmentsCount = 64;
            VectorLine orbitLine = new VectorLine("OrbitLine", new List<Vector3>(orbitSegmentsCount + 1), 1f);
            orbitLine.lineType = LineType.Continuous;
            orbitLine.MakeCircle(Vector3.zero, Vector3.up, radius, orbitSegmentsCount);
            orbit._prefix = prefix ?? (PrefixType)UnityEngine.Random.Range(0, 3);
            orbitLine.color = Color.red;

            orbitLine.drawTransform = orbit.transform;
            orbitLine.layer = LayerMask.NameToLayer("Default");

            orbitLine.Draw3DAuto();

            orbit._line = orbitLine;

            return orbit;
        }
    }

    [Serializable]
    public class StateChangedEvent : UnityEvent<State> { }
    #endregion

    #region Fiedls
    private PrefixType _prefix;
    private VectorLine _line;
    #endregion

    #region Events
    public StateChangedEvent StateChanged;
    #endregion

    #region Properties
    public PrefixType Prefix { get { return _prefix; } }
    public VectorLine Line { get { return _line; } }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    public State GetState()
    {
        return new State();
    }
    #endregion

    #region Event handlers
    #endregion
}
