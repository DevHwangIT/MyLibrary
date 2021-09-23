using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Subject : MonoBehaviour
{
	Transform _tr;
	CubeActor _cube;
	delegate void MoveHandler();

	MoveHandler _moveHandler;
	void Start()
	{
		_tr = gameObject.transform;

		_cube = new CubeActor();
		_moveHandler += new MoveHandler(_cube.OnNotify);

		StartCoroutine(UpdateGoingUp());
	}

	IEnumerator UpdateGoingUp()
	{
		while(true)
		{
			_tr.position = new Vector3(_tr.position.x, _tr.position.y+0.05f, _tr.position.z);

			if(_tr.position.y > 3f)
			{
				_moveHandler();
				yield break;
			}
			yield return null;
		}
	}
}
