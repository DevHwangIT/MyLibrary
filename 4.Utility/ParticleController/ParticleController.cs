using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace MyLibrary.Utility
{
	public enum HideType
	{
		Destroy,
		Scaling,
		Enable,
		ObjectPooling
	}

	public enum DestroyCondition
	{
		Distance,
		Time,
		DeleteCall
	}
	
	public class ParticleController : MonoBehaviour
	{
		private HideType hideType = HideType.Enable;
		public HideType HideType
		{
			get { return hideType; }
			set
			{
				hideType = value;
				if (hideType == HideType.Scaling)
				{
					for (int i = 0; i < particleSystems.Length; i++)
					{
						ParticleSystem.MainModule particle = particleSystems[i].main;
						particle.scalingMode = ParticleSystemScalingMode.Hierarchy;
					}
				}
				else
				{
					for (int i = 0; i < particleSystems.Length; i++)
					{
						ParticleSystem.MainModule particle = particleSystems[i].main;
						particle.scalingMode = ParticleSystemScalingMode.Local;
					}
				}
			}
		}
		public DestroyCondition destroyType = DestroyCondition.DeleteCall;

		public Transform distanceCompareTarget = null;
		public float distanceValue = 0;
		private float destructionTimer = 0f;
		public float durationTime = 0;

		private ParticleSystem[] particleSystems;
		private bool IsPaused = true;
		private Vector3 InitPos;

		public UnityEvent onObjectPoolCalling;
		void Awake()
		{
			particleSystems = this.GetComponentsInChildren<ParticleSystem>(true);
			Initialize();
		}
		
		void FixedUpdate()
		{
			destructionTimer += Time.fixedDeltaTime;
			switch (destroyType)
			{
				case DestroyCondition.Distance:
					if (Vector3.Distance(InitPos, distanceCompareTarget.position) > distanceValue)
						Stop();
					break;
				
				case DestroyCondition.Time:
					if (destructionTimer > durationTime)
						Stop();
					break;
			}
		}

		private void Initialize()
		{
			InitPos = transform.position;
			destructionTimer = 0f;
			foreach (var particle in particleSystems)
			{
				particle.gameObject.transform.localScale = Vector3.one;
				if (particle.isPlaying)
					particle.Stop();
			}
			this.gameObject.SetActive(true);
		}

		public void Play()
		{
			Initialize();
			foreach (var particle in particleSystems)
			{
				if (!particle.isPlaying) 
					particle.Play();
			}
		}
		
		public void Pause()
		{
			foreach (var particle in particleSystems)
			{
				if (particle.isPlaying)
					particle.Pause();
			}
		}

		public void Stop()
		{
			foreach (var particle in particleSystems)
			{
				if (particle.isPlaying)
					particle.Stop();
			}
			
			switch (hideType)
			{
				case HideType.Destroy:
					Destroy(this.gameObject);
					break;
				
				case HideType.Scaling:
					for (int i = 0; i < particleSystems.Length; i++)
					{
						particleSystems[i].gameObject.transform.localScale = Vector3.zero;
					}
					break;
				
				case HideType.Enable:
					this.gameObject.SetActive(false);
					break;
				
				case HideType.ObjectPooling:
					onObjectPoolCalling?.Invoke();
					break;
			}
		}
	}
}
