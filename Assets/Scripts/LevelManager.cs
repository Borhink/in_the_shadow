using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelType {RotationX, RotationXY, PositionY};
public class LevelManager : MonoBehaviour {
	public int id = 1;
	public Transform winMenu;
	public Transform[] objects;
	public Vector3[] positions;
	public Vector3[] rotations;
	public Vector3[] rotations2;
	public LevelType type = LevelType.RotationX;
	public float maxErrorPosY = 0.1f;
	public float maxErrorRotVert = 3f;
	public float maxErrorRotHoriz = 3f;

	float rotationSpeed = 90f;
	float translationSpeed = 2f;
	float maxY = 1.1f;
	float minY = 0.55f;
	bool win = false;
	Transform toMove = null;
	void Start () {
		for (int i = 0; i < objects.Length; ++i)
		{
			if (i >= 1)
				positions[i] = new Vector3(objects[i].position.x - objects[i - 1].position.x, objects[i].position.y - objects[i - 1].position.y, objects[i].position.z - objects[i - 1].position.z);
		}
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		randomizeObjects();
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Q))
			SceneManager.LoadScene("Scenes/MainMenu");
		if (win)
		{
			if (Input.GetKeyDown(KeyCode.N) && (id + 1) <= GameManager.gm.nbLevels)
				SceneManager.LoadScene("Scenes/Level" + (id + 1));
			if (winMenu.position.y > 1.120736f)
				winMenu.position += new Vector3(0f, -0.5f * Time.deltaTime, 0f);
			if (winMenu.position.y < 1.120736f)
				winMenu.position = new Vector3(winMenu.position.x, 1.120736f, winMenu.position.z);

		}
		if (Input.GetMouseButtonDown(0))
		{
			selectObject();
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			toMove = null;
		}
		if (Input.GetMouseButton(0))
			transformObject();
		if (!win)
			checkVictory();
	}

	void checkRotation(int i, ref int error)
	{
		float val = -666f;

		foreach (Vector3 angle in (i == 0 ? rotations : rotations2))
		{
			if (type >= LevelType.RotationXY)
			{
				val = objects[i].eulerAngles.z > 180 ? objects[i].eulerAngles.z - 360 : objects[i].eulerAngles.z;
				if (Mathf.Abs(val - angle.z) > maxErrorRotVert)
					continue;
			}
			val = objects[i].eulerAngles.y > 180 ? objects[i].eulerAngles.y - 360 : objects[i].eulerAngles.y;
			if (Mathf.Abs(val - angle.y) <= maxErrorRotHoriz)
				return;
			Debug.Log(i + ") ErrorRotVert: " + val + " - " + angle.z + " <= " + maxErrorRotVert);
			Debug.Log(i + ") ErrorRotHoriz: " + val + " - " + angle.y + " <=> " + maxErrorRotHoriz);
		}
		++error;
	}

	void checkVictory()
	{
		int error = 0;

		for (int i = 0; i < objects.Length; ++i)
		{
			if (type >= LevelType.PositionY)
			{
				if (i >= 1 && Mathf.Abs(objects[i].position.y - objects[i - 1].position.y - positions[i].y) > maxErrorPosY)
				{
					++error;
					Debug.Log(i + ") ErrorPos: " + Mathf.Abs(objects[i].position.y - objects[i - 1].position.y - positions[i].y) + " > " + maxErrorPosY);
				}
			}
			checkRotation(i, ref error);
		}
		if (error == 0)
		{
			win = true;
			rotationSpeed = 1f;
			translationSpeed = 0.02f;
			if (PlayerPrefs.HasKey("success" + id) && PlayerPrefs.GetInt("success" + id) == 1)
				return;
			PlayerPrefs.SetInt("success" + id, 1);
			PlayerPrefs.SetInt("newSuccess" + id, 1);
			PlayerPrefs.SetInt("unlocked" + (id + 1), 1);
			PlayerPrefs.SetInt("newUnlock" + (id + 1), 1);
		}
	}

	void randomizeObjects()
	{
		for (int i = 0; i < objects.Length; ++i)
		{
			float verticalRotation = 0f, horizontalRotation = 0f, verticalPosition = 0f;

			if (type >= LevelType.PositionY)
				verticalPosition += UnityEngine.Random.Range(minY, maxY);
			if (type >= LevelType.RotationXY)
				verticalRotation = UnityEngine.Random.Range(20f, GameManager.gm.difficulty);
			horizontalRotation = UnityEngine.Random.Range(20f, GameManager.gm.difficulty);
			objects[i].eulerAngles = new Vector3(0f, horizontalRotation, verticalRotation);
			if (type >= LevelType.PositionY)
				objects[i].position = new Vector3(objects[i].position.x, verticalPosition, objects[i].position.z);
		}
	}

	void selectObject()
	{
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Physics.Raycast(cameraRay, out hit);
		if (hit.collider && !toMove)
		{
			for (int i = 0; i < objects.Length; ++i)
			{
				if (hit.collider.transform == objects[i])
				{
					toMove = objects[i];
					break;
				}
			}
		}
	}

	void transformObject()
	{
		float verticalRotation = 0f, horizontalRotation = 0f, verticalPosition = 0f;

		if (type >= LevelType.PositionY && Input.GetKey(KeyCode.LeftShift))
			verticalPosition = Input.GetAxis("Mouse Y");
		else if (type >= LevelType.RotationXY && Input.GetKey(KeyCode.LeftControl))
			verticalRotation = Input.GetAxis("Mouse Y");
		else
			horizontalRotation = -Input.GetAxis("Mouse X");
		if (!toMove)
			return;
		toMove.eulerAngles += new Vector3(0f, horizontalRotation, verticalRotation) * Time.deltaTime * rotationSpeed;
		toMove.position += new Vector3(0f, verticalPosition, 0) * Time.deltaTime * translationSpeed;
		if (toMove.position.y > maxY)
			toMove.position = new Vector3(toMove.position.x, maxY, toMove.position.z);
		if (toMove.position.y < minY)
			toMove.position = new Vector3(toMove.position.x, minY, toMove.position.z);
	}
}
