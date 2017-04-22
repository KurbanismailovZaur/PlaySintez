using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vectrosity;
using System.Linq;

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
    #endregion

    #region Classes
    public class Factory
    {
        public Orbit Create(Star star, float radius)
        {
            Orbit orbit = new GameObject("Orbit").AddComponent<Orbit>();
            orbit._star = star;
            orbit.BaseObjectType = ObjectType.Orbit;
            orbit._radius = radius;
            orbit._maxSocketCount = (int)radius * 2;

            int orbitSegmentsCount = 64;
            VectorLine orbitLine = new VectorLine(string.Format("OrbitLine{0}", star.Level), new List<Vector3>(orbitSegmentsCount + 1), 1f);
            orbitLine.lineType = LineType.Continuous;
            orbitLine.MakeCircle(Vector3.zero, Vector3.up, orbit._radius, orbitSegmentsCount);

            Material lineMaterial = new Material(Shader.Find("Unlit/Color"));
            lineMaterial.color = new Color32(128, 128, 128, 255);

            orbit._lineMaterial = lineMaterial;
            orbitLine.material = lineMaterial;

            Transform socketPrefab = Resources.Load<Transform>("Socket/Socket");

            int socketCount = UnityEngine.Random.Range(1, orbit._maxSocketCount);
            if (star.Level == 2)
            {
                if (socketCount == 1)
                {
                    socketCount = 2;
                }
            }

            orbit._distanceBetweenSockets = 1f / socketCount;

            for (int i = 0; i < socketCount; i++)
            {
                Transform socketTransform = Instantiate(socketPrefab, orbit.transform, false);
                Vector3 pos = orbitLine.GetPoint3D01(orbit._distanceBetweenSockets * i);
                socketTransform.position = pos;

                socketTransform.rotation = Quaternion.LookRotation(pos - orbit.transform.position);

                Socket socket = socketTransform.GetComponent<Socket>();
                socket.SetOrbit(orbit);
                socket.ModuleChanged.AddListener(orbit.Socket_ModuleChanged);

                orbit._sockets.Add(socket);
            }

            orbitLine.drawTransform = orbit.transform;
            orbitLine.layer = LayerMask.NameToLayer("Default");

            orbitLine.Draw3DAuto();

            orbit._line = orbitLine;

            if (orbit._sockets.Count > 1)
            {
                int maxLinksCount = orbit._sockets.Count / 2;
                int linksCount = UnityEngine.Random.Range(0, maxLinksCount + 1);

                if (star.Level == 2)
                {
                    if (linksCount == 0)
                    {
                        linksCount = 1;
                    }
                }

                int linkedArkSegmentsCount = orbitSegmentsCount / orbit._sockets.Count;

                for (int i = 0; i < linksCount; i += 2)
                {
                    orbit._sockets[i].LinkedSocket = orbit._sockets[i + 1];
                    orbit._sockets[i + 1].LinkedSocket = orbit._sockets[i];

                    float angleBettwenLinkedSockets = 360f / orbit._sockets.Count;
                    VectorLine linkedLine = new VectorLine(string.Format("Orbit{0}LinkLine{1}", star.Level, i), new List<Vector3>(linkedArkSegmentsCount * 2), 4f);
                    linkedLine.lineType = LineType.Discrete;
                    linkedLine.MakeArc(orbit.transform.position, Vector3.up, radius, radius, i * angleBettwenLinkedSockets, (i + 1) * angleBettwenLinkedSockets, linkedArkSegmentsCount);

                    Material linkedMaterial = new Material(Shader.Find("Unlit/Color"));
                    linkedMaterial.color = new Color32(255, 255, 0, 255);

                    linkedLine.material = linkedMaterial;

                    linkedLine.drawTransform = orbit.transform;
                    linkedLine.layer = LayerMask.NameToLayer("Default");

                    linkedLine.Draw3DAuto();

                    orbit._linkedLines.Add(linkedLine);
                }
            }

            return orbit;
        }
    }

    [Serializable]
    public class StateChangedEvent : UnityEvent<Orbit> { }

    [SerializeField]
    public class SocketModuleChangedEvent : UnityEvent<Orbit, Socket, Module> { }
    #endregion

    #region Fiedls
    private Star _star;

    private List<PrefixType> _prefixes = new List<PrefixType>();
    private VectorLine _line;

    private float _radius;

    private Material _lineMaterial;

    private int _maxSocketCount;
    private float _distanceBetweenSockets;

    private byte _level;
    private float _lookLevelProgress;
    private float _commentLevelProgress;
    private float _likeLevelProgress;
    private float _whiteLevelProgress;

    private List<Socket> _sockets = new List<Socket>();
    private List<VectorLine> _linkedLines = new List<VectorLine>();
    #endregion

    #region Events
    public StateChangedEvent StateChanged = new StateChangedEvent();
    public SocketModuleChangedEvent SocketModuleChanged = new SocketModuleChangedEvent();
    #endregion

    #region Properties
    public List<PrefixType> Prefixes { get { return _prefixes; } }
    public VectorLine Line { get { return _line; } }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    private void Update()
    {
        transform.Rotate(Vector3.up, 1f / (_radius / 4f) * Time.deltaTime);
    }

    public void IncreaseLevelProgress()
    {
        foreach (Socket socket in _sockets)
        {
            Modules.Capsule capsule = socket.ConnectedModule as Modules.Capsule;

            if (capsule != null)
            {
                float bonus = 0;

                switch (capsule.CapsuleEnergyType)
                {
                    case Modules.Capsule.EnergyType.Look:
                        bonus = _prefixes.FindAll(x => x == PrefixType.R).Count + 1;
                        _lookLevelProgress += (1f / Mathf.Pow(_level + 2, 1.75f)) * bonus;
                        break;
                    case Modules.Capsule.EnergyType.Comment:
                        bonus = _prefixes.FindAll(x => x == PrefixType.G).Count + 1;
                        _commentLevelProgress += 1f / Mathf.Pow(_level + 2, 1.75f) * bonus;
                        break;
                    case Modules.Capsule.EnergyType.Like:
                        bonus = _prefixes.FindAll(x => x == PrefixType.B).Count + 1;
                        _likeLevelProgress += 1f / Mathf.Pow(_level + 2, 1.75f) * bonus;
                        break;
                }
            }
            else if (socket.ConnectedModule != null)
            {
                float bonus = _prefixes.FindAll(x => x == PrefixType.A).Count + 1;
                _whiteLevelProgress += 1f / Mathf.Pow(_level + 2, 1.75f) * bonus;
            }
        }

        StateChanged.Invoke(this);

        SendLevelProgressIncreasingToAllSockets();

        float sum = _lookLevelProgress + _commentLevelProgress + _likeLevelProgress + _whiteLevelProgress;

        if (sum >= 1f)
        {
            LevelUpOrbit();
        }
    }

    public void LevelUpOrbit()
    {
        int prefixNumber = 0;

        if (_level == 0)
        {
            List<float> energys = new List<float> { _lookLevelProgress, _commentLevelProgress, _likeLevelProgress, _whiteLevelProgress };
            float max = energys.Max();

            List<int> indexes = new List<int>();
            for (int i = 0; i < energys.Count; i++)
            {
                if (Mathf.Approximately(energys[i], max))
                {
                    indexes.Add(i);
                }
            }

            prefixNumber = indexes[UnityEngine.Random.Range(0, indexes.Count)];
        }
        else
        {
            prefixNumber = UnityEngine.Random.Range(0, 4);
        }

        PrefixType? lastPrefix = null;
        if (_prefixes.Count() != 0)
        {
            lastPrefix = _prefixes.Last();
        }

        PrefixType newPrefix = (PrefixType)prefixNumber;
        _prefixes.Add(newPrefix);

        if (newPrefix != lastPrefix)
        {
            foreach (Socket socket in _sockets)
            {
                if (socket.ConnectedModule != null && socket.ConnectedModule.SuitablePrefix != newPrefix)
                {
                    _star.PutModuleInInventory(socket.ConnectedModule);
                }
            }
        }

        switch (_prefixes.Last())
        {
            case PrefixType.R:
                _lineMaterial.color = Color.red;
                break;
            case PrefixType.G:
                _lineMaterial.color = Color.green;
                break;
            case PrefixType.B:
                _lineMaterial.color = Color.blue;
                break;
            case PrefixType.A:
                _lineMaterial.color = Color.white;
                break;
        }

        _lookLevelProgress = _commentLevelProgress = _likeLevelProgress = _whiteLevelProgress = 0f;
        _level += 1;

        StateChanged.Invoke(this);
    }

    private void SendLevelProgressIncreasingToAllSockets()
    {
        foreach (Socket socket in _sockets)
        {
            socket.SendLevelProgressIncreasingToModule();
        }
    }
    #endregion

    #region Event handlers
    public void Socket_ModuleChanged(Socket socket, Module module)
    {
        SocketModuleChanged.Invoke(this, socket, module);
    }
    #endregion
}
