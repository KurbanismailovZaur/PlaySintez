using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
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
    private static T _instance;
    #endregion

    #region Events
    #endregion

    #region Properties
    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            T[] currents = GameObject.FindObjectsOfType<T>();

            if (currents.Length == 0)
            {
                _instance = new GameObject(typeof(T).Name).AddComponent<T>();

                return _instance;
            }

            if (currents.Length == 1)
            {
                _instance = currents[0];

                return _instance;
            }

            Debug.LogErrorFormat("Scene have more than one objects with {0} component attached", typeof(T).Name);

            return null;
        }
    }
    #endregion

    #region Constructors
    #endregion

    #region Methods
    #endregion

    #region Event handlers
    #endregion
}
