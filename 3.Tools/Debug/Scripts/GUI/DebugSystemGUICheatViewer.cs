﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyLibrary.Tools
{
    public class DebugSystemGUICheatViewer : MonoBehaviour, IDebugSystemGUI
    {
        Vector2 scrollPosition = new Vector2(0, 0);

        private void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void OnDrawGUI(DebugSystemDataScriptable systemData, DebugSystemGUIDataScriptable guiData)
        {
            DebugEventHandler handler = DebugSystem.Instance.GetEventHandler;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            {
                for (int index = 0; index < handler.Count; index++)
                {
                    CheatEvent cheat;
                    if (handler.TryGetIndex(index, out cheat))
                    {
                        if (cheat.GetName != "")
                        {
                            GUIStyle titleStyle = GUI.skin.GetStyle("TitleTextStyle");
                            GUILayout.Label("이름 : " + cheat.GetName, titleStyle);
                            GUILayout.Label("설명 : " + cheat.GetInfo);
                            if (GUILayout.Button(cheat.GetName,
                                GUILayout.Width(Screen.width * 0.5f * systemData.scaleFactor),
                                GUILayout.Height((int) (Screen.height * 0.05f * systemData.scaleFactor))))
                            {
                                cheat.CallCheatEvent();
                            }
                        }
                    }
                }
            }
            GUILayout.EndScrollView();
        }
    }
}