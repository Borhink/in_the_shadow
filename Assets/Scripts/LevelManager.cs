using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType {RotationX, RotationXY, PositionY};
public class LevelManager : MonoBehaviour {
	public Transform[] objects;
	public Vector3[] positions;
	public Vector3[] rotations;
	public LevelType type = LevelType.RotationX;
	float rotationSpeed = 90f;
	float translationSpeed = 2f;

	void Start () {
		for (int i = 0; i < objects.Length; ++i)
		{
			positions[i] = objects[i].position;
			rotations[i] = objects[i].eulerAngles;
		}
	}

	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		if (Input.GetMouseButton(0))
		{
			float verticalRotation = 0f, horizontalRotation = 0f, verticalPosition = 0f;
			
			if (type >= LevelType.PositionY && Input.GetKey(KeyCode.LeftShift))
				verticalPosition = Input.GetAxis("Mouse Y");
			else if (type >= LevelType.RotationXY && Input.GetKey(KeyCode.LeftControl))
				verticalRotation = Input.GetAxis("Mouse Y");
			else
				horizontalRotation = -Input.GetAxis("Mouse X");
			objects[0].eulerAngles += new Vector3(0f, horizontalRotation, verticalRotation) * Time.deltaTime * rotationSpeed;
			objects[0].position += new Vector3(0f, verticalPosition, 0) * Time.deltaTime * translationSpeed;
			if (objects[0].position.y > 1.3f)
				objects[0].position = new Vector3(objects[0].position.x, 1.4f, objects[0].position.z);
			if (objects[0].position.y < 0.35f)
				objects[0].position = new Vector3(objects[0].position.x, 0.35f, objects[0].position.z);
		}
	}

}
