using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Profiling;

namespace MyLibrary.Tools
{
    public class DebugSystemGUISystemViewer : MonoBehaviour, IDebugSystemGUI
    {
        private float deltaTime = 0.0f;

        ProfilerRecorder totalMemoryRecorder;
        ProfilerRecorder systemMemoryRecorder;
        ProfilerRecorder gcMemoryRecorder;
        ProfilerRecorder mainThreadTimeRecorder;
        ProfilerRecorder setPassCallsRecorder;
        ProfilerRecorder drawCallsRecorder;
        ProfilerRecorder verticesRecorder;

        void OnEnable()
        {
            systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
            totalMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");

            gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
            mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);

            setPassCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            drawCallsRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            verticesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        }

        void OnDisable()
        {
            totalMemoryRecorder.Dispose();
            systemMemoryRecorder.Dispose();
            gcMemoryRecorder.Dispose();
            mainThreadTimeRecorder.Dispose();

            setPassCallsRecorder.Dispose();
            drawCallsRecorder.Dispose();
            verticesRecorder.Dispose();
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

            if (setPassCallsRecorder.Valid)
                systeminfoString.AppendLine($"SetPass Calls: {setPassCallsRecorder.LastValue}");
            if (drawCallsRecorder.Valid)
                systeminfoString.AppendLine($"Draw Calls: {drawCallsRecorder.LastValue}");
            if (verticesRecorder.Valid)
                systeminfoString.AppendLine($"Vertices: {verticesRecorder.LastValue}");
            systeminfoString.AppendLine($"GC Memory: {gcMemoryRecorder.LastValue / (1024 * 1024)} MB");
            systeminfoString.AppendLine($"System Memory: {systemMemoryRecorder.LastValue / (1024 * 1024)} MB");
            systeminfoString.AppendLine($"Total Memory: {totalMemoryRecorder.LastValue / (1024 * 1024)} MB");

            GUILayout.Label(systeminfoString.ToString());


            GUILayout.Label("- Scene Info", titleStyle);
            StringBuilder sceneinfoString = new StringBuilder();
            sceneinfoString.Append("Scene Index : " + SceneManager.GetActiveScene().buildIndex + "\n");
            sceneinfoString.Append("Scene Name : " + SceneManager.GetActiveScene().name + "\n");
            GUILayout.Label(sceneinfoString.ToString());
        }
    }
}