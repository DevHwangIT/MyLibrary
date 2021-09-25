using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace MyLibrary.Manager
{
    public class UnityAnalyticsManager : MonoBehaviour
    {
        #region Singleton

        private static UnityAnalyticsManager _instance;
        public static UnityAnalyticsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (UnityAnalyticsManager) FindObjectOfType(typeof(UnityAnalyticsManager));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(UnityAnalyticsManager)} (Singleton)");
                        _instance = singletonObject.AddComponent<UnityAnalyticsManager>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
        #endregion

        private bool quitFlag = false;
        
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnApplicationQuit()
        {
            quitFlag = true;
            Analytics.CustomEvent("Application Quit", new Dictionary<string, object>
            {
                {"Is Application Quit?", quitFlag}
            });
        }
    }
}
