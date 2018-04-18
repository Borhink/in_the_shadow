using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {
	public Transform cameraLight;
	public float minX  = -30f;
	public float maxX  = 30f;
	public float minY  = -60f;
	public float maxY  = 60f;
	public float sensitivityX = 6f;
	public float sensitivityY = 6f;

	float rotationY = 0f;
	float rotationX = 0f;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		rotationY = transform.localEulerAngles.y;
		minY += transform.localEulerAngles.y;
		maxY += transform.localEulerAngles.y;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (Input.GetMouseButtonDown(0))
			Cursor.lockState = CursorLockMode.Locked;
		if (Cursor.lockState == CursorLockMode.Locked)
		{
			rotationX += Input.GetAxis("Mouse Y") * sensitivityX;
			rotationY += Input.GetAxis("Mouse X") * sensitivityY;
			rotationX = Mathf.Clamp(rotationX, minX, maxX);
			rotationY = Mathf.Clamp(rotationY, minY, maxY);
			transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
				cameraLight.LookAt(hit.point);
		}
	}
}