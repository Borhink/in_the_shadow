using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType {RotationX, RotationXY, PositionY};
public class LevelManager : MonoBehaviour {
	public Transform[] objects;
	public Vector3[] positions;
	public Vector3[] rotations;
	public LevelType type = LevelType.RotationX;
	public float maxErrorPosY = 0.1f;
	public float maxErrorRotVert = 3f;
	public float maxErrorRotHoriz = 3f;
	float rotationSpeed = 90f;
	float translationSpeed = 2f;
	float maxY = 1.15f;
	float minY = 0.63f;
	bool win = false;
	void Start () {
		for (int i = 0; i < objects.Length; ++i)
		{
			positions[i] = objects[i].localPosition;
			rotations[i] = objects[i].eulerAngles;
		}
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		for (int i = 0; i < objects.Length; ++i)
		{
			float verticalRotation = 0f, horizontalRotation = 0f, verticalPosition = 0f;
			if (type >= LevelType.PositionY)
				verticalPosition += Random.Range(minY, maxY);
			if (type >= LevelType.RotationXY)
				verticalRotation = Random.Range(20f, GameManager.gm.difficulty);
			horizontalRotation = Random.Range(20f, GameManager.gm.difficulty);
			objects[i].eulerAngles = new Vector3(0f, horizontalRotation, verticalRotation);
			if (type >= LevelType.PositionY)
				objects[i].position = new Vector3(objects[i].position.x, verticalPosition, objects[i].position.z);
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
			if (objects[0].position.y > maxY)
				objects[0].position = new Vector3(objects[0].position.x, maxY, objects[0].position.z);
			if (objects[0].position.y < minY)
				objects[0].position = new Vector3(objects[0].position.x, minY, objects[0].position.z);
		}
		checkVictory();
	}

	void checkVictory()
	{
		for (int i = 0; i < objects.Length; ++i)
		{
			int error = 0;
			if (type >= LevelType.PositionY)
			{
				if (objects[i].position.y < positions[i].y - maxErrorPosY || objects[i].position.y > positions[i].y + maxErrorPosY)
					++error;
			}
			if (type >= LevelType.RotationXY)
			{
				if (objects[i].eulerAngles.z < rotations[i].z - maxErrorRotVert && objects[i].eulerAngles.z > rotations[i].z + maxErrorRotVert)
					++error;
			}
			if (objects[i].eulerAngles.y < rotations[i].y - maxErrorRotHoriz && objects[i].eulerAngles.y > rotations[i].y + maxErrorRotHoriz)
				++error;
			if (error == 0)
			{
				win = true;
				Debug.Log("OK");
			}
		}
	}
}
