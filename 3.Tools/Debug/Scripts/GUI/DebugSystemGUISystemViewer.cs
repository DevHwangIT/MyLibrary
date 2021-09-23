using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyLibrary.Tools
{
    public class DebugSystemGUISystemViewer : MonoBehaviour, IDebugSystemGUI
    {
        private float deltaTime = 0.0f;

        private void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        private string GetStringFPS()
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            return string.Format("Frame State : {0:0.0} ms ({1:0.} fps) \n", msec, fps);
        }

        public void OnDrawGUI(DebugSystemDataScriptable systemData, DebugSystemGUIDataScriptable guiData)
        {
            GUIStyle titleStyle = GUI.skin.GetStyle("TitleTextStyle");

            GUILayout.Label("- Device Info", titleStyle);
            StringBuilder deviceinfoString = new StringBuilder();
            deviceinfoString.Append("Device Name : " + SystemInfo.deviceName + "\n");
            deviceinfoString.Append("Device Model : " + SystemInfo.deviceModel + "\n");
            deviceinfoString.Append("Graphics Device Name : " + SystemInfo.graphicsDeviceName + "\n");
            deviceinfoString.Append("Graphics Device Type : " + SystemInfo.graphicsDeviceType + "\n");
            GUILayout.Label(deviceinfoString.ToString());

            GUILayout.Label("- System Info", titleStyle);
            StringBuilder systeminfoString = new StringBuilder();
            systeminfoString.Append(GetStringFPS());
            systeminfoString.Append("Draw Call : " + UnityStats.drawCalls + "\n");
            systeminfoString.Append("Batches : " + UnityStats.batches + "\n");
            systeminfoString.Append("Static Batches : " + UnityStats.staticBatches + " \t " +
                                    "Dynamic Batches : " + UnityStats.dynamicBatches + "\n");
            systeminfoString.Append("Tris: " + UnityStats.triangles + " \t " + "Verts: " + UnityStats.vertices +
                                    "\n");
            systeminfoString.Append("Screen: " + UnityStats.screenRes + "\n");
            systeminfoString.Append("SetPass call: " + UnityStats.setPassCalls + " \t " + "Shadow caster: " +
                                    UnityStats.shadowCasters + "\n");
            systeminfoString.Append("Visible skinned meshes: " + UnityStats.visibleSkinnedMeshes + " \t " +
                                    "Animations: " + UnityStats.visibleAnimations + "\n");
            GUILayout.Label(systeminfoString.ToString());


            GUILayout.Label("- Scene Info", titleStyle);
            StringBuilder sceneinfoString = new StringBuilder();
            sceneinfoString.Append("Scene Index : " + SceneManager.GetActiveScene().buildIndex + "\n");
            sceneinfoString.Append("Scene Name : " + SceneManager.GetActiveScene().name + "\n");
            GUILayout.Label(sceneinfoString.ToString());
        }
    }
}