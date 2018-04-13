using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType {RotationX, RotationXY, PositionX};
public class LevelManager : MonoBehaviour {
	public Transform[] objects;
	public Vector3[] positions;
	public Vector3[] rotations;
	public LevelType type = LevelType.RotationX;
	float rotationSpeed = 90f;

	void Start () {
		for (int i = 0; i < objects.Length; ++i)
		{
			positions[i] = objects[i].position;
			rotations[i] = objects[i].eulerAngles;
		}
	}

	void Update () {
		if (Input.GetMouseButton(0))
		{
			float rotX = 0f, rotY = 0f;
			
			rotY = -Input.GetAxis("Mouse X");
			if (type >= LevelType.RotationXY) rotX = Input.GetAxis("Mouse Y");

			objects[0].Rotate(new Vector3(rotX, rotY, 0) * Time.deltaTime * rotationSpeed);
		}
	}
}
