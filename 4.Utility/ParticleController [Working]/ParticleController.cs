using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyLibrary.Utility
{
	public class ParticleController : MonoBehaviour
	{
		public HideType HideType = HideType.Destroy;
		public DestroyType DestroyType = DestroyType.Distance;
		public bool IsPaused = true;
		public bool EnableTimer = false;

		[Min(0)] public float Timer = 1f;
		[Range(0, 10)] public float Distance = 1f;
		public VolumeParticle ParticleMesh;

		[Range(0, 1)] public float PositionOffset = 0;

		[Min(0)] public int DestroyTime = 1000;

		[Range(0, .5f)] public float InterpFactorIn = .01f;
		[Range(0, .5f)] public float InterpFactorOut = .01f;

		float counter = 0;
		float timerCounter = 0;

		void FixedUpdate()
		{
			
		}

		public void Pause()
		{
			IsPaused = true;
		}

		public void Play()
		{
			IsPaused = false;
		}
	}
}
