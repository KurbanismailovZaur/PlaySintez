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
            orbit.BaseObjectType = ObjectType.Orbit;
            orbit._radius = radius;
            orbit._maxSocketCount = (int)radius * 2;

            int orbitSegmentsCount = 64;
            VectorLine orbitLine = new VectorLine("OrbitLine", new List<Vector3>(orbitSegmentsCount + 1), 1f);
            orbitLine.lineType = LineType.Continuous;
            orbitLine.MakeCircle(Vector3.zero, Vector3.up, orbit._radius, orbitSegmentsCount);
            orbit._prefix = prefix ?? (PrefixType)UnityEngine.Random.Range(0, 3);

            Material lineMaterial = new Material(Shader.Find("Unlit/Color"));
            lineMaterial.color = new Color32(128, 128, 128, 255);
            orbitLine.material = lineMaterial;
            
            Transform socketPrefab = Resources.Load<Transform>("Socket/Socket");

            orbit._socketCount = UnityEngine.Random.Range(1, orbit._maxSocketCount);
            orbit._distanceBetweenSockets = 1f / orbit._socketCount;

            for (int i = 0; i < orbit._socketCount; i++)
            {
                Transform socket = Instantiate(socketPrefab, orbit.transform, false);
                Vector3 pos = orbitLine.GetPoint3D01(orbit._distanceBetweenSockets * i);
                socket.position = pos;

                socket.rotation = Quaternion.LookRotation(pos - orbit.transform.position);

                socket.GetComponent<Socket>().ModuleChanged.AddListener(orbit.Socket_ModuleChanged);
            }

            orbitLine.drawTransform = orbit.transform;
            orbitLine.layer = LayerMask.NameToLayer("Default");

            orbitLine.Draw3DAuto();

            orbit._line = orbitLine;

            return orbit;
        }
    }

    [Serializable]
    public class StateChangedEvent : UnityEvent<State> { }

    [SerializeField]
    public class SocketModuleChangedEvent : UnityEvent<Orbit, Socket, Module> { }
    #endregion

    #region Fiedls
    private PrefixType _prefix;
    private VectorLine _line;

    private float _radius;

    private int _maxSocketCount;
    private int _socketCount;
    private float _distanceBetweenSockets;
    #endregion

    #region Events
    public StateChangedEvent StateChanged;
    public SocketModuleChangedEvent SocketModuleChanged = new SocketModuleChangedEvent();
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

    private void Update()
    {
        transform.Rotate(Vector3.up, 1f / (_radius / 4f) * Time.deltaTime);
    }
    #endregion

    #region Event handlers
    public void Socket_ModuleChanged(Socket socket, Module module)
    {
        SocketModuleChanged.Invoke(this, socket, module);
    }
    #endregion
}
