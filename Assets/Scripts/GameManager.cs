using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	// enum LevelType {RotationX, }
	public Transform[] objects;
	public Vector3[] positions;
	public Vector3[] rotations;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < objects.Length; ++i)
		{
			positions[i] = objects[i].position;
			rotations[i] = objects[i].eulerAngles;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			// rotations[i]
		}
	}
}
