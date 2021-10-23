using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager : MonoBehaviour
{  
    #region Singleton
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (DataManager) FindObjectOfType(typeof(DataManager));
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject($"{typeof(DataManager)} (Singleton)");
                    _instance = singletonObject.AddComponent<DataManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }
    #endregion
    
    static PlayerData PlayerData = new PlayerData();
    static UserData UserData = new UserData();
}
