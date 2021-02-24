using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNDOL : MonoBehaviour
{
    #region Singleton class: Test

    public static DNDOL Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}