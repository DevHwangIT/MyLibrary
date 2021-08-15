using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeActor
{
	GameObject _cube;

	// Use this for initialization
	public void OnNotify()
	{
		_cube = GameObject.Find("Cube") as GameObject;
		DoMove();
	}

	void DoMove()
	{
		_cube.transform.position = new Vector3(3f, 3f, 3f);
	}
}
