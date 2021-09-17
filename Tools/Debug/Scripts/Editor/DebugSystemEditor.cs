using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyLibrary
{
	public class DebugSystemEditor : Editor
	{
		[MenuItem("Aloha/Debug/Create Debug Instance")]
		public static void CreateInstanceBinder()
		{
			DebugSystem initInstance = DebugSystem.Instance;
		}

		public class DebugSystemModificationProcessor : UnityEditor.AssetModificationProcessor
		{
			[InitializeOnLoad]
			public class BuildInfo
			{
				static BuildInfo()
				{
					EditorApplication.update += Update;
				}

				static bool isCompiling = true;

				static void Update()
				{
					if (!EditorApplication.isCompiling && isCompiling)
					{
						//Debug.Log("Finish Compile");
						if (!Directory.Exists(Application.dataPath + "/StreamingAssets"))
						{
							Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
						}

						string info_path = Application.dataPath + "/StreamingAssets/build_info";
						StreamWriter build_info = new StreamWriter(info_path);
						build_info.Write(
							"Build from " + SystemInfo.deviceName + " at " + System.DateTime.Now.ToString());
						build_info.Close();
					}

					isCompiling = EditorApplication.isCompiling;
				}
			}
		}
	}
}

