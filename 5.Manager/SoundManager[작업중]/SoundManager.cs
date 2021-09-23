using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.Manager
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (SoundManager) FindObjectOfType(typeof(SoundManager));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(SoundManager)} (Singleton)");
                        _instance = singletonObject.AddComponent<SoundManager>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
