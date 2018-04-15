using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	Transform selected = null;
	Vector3 selectedScale;
	public float zoomRange = 0.3f;
	public float zoomSpeed = 5f;
	float timer = 0f;
	void Start () {
		
	}

	void Update () {
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
			if (hit.collider.tag == "Button")
			{
				timer += Time.deltaTime;
				if (selected != hit.transform)
				{
					selected = hit.transform;
					selectedScale = selected.localScale;
				}
				else
				{
					float zoom = Mathf.PingPong(timer, zoomRange * zoomSpeed) / zoomSpeed;
					selected.localScale = new Vector3(selectedScale.x * (1f + zoom), selectedScale.y * (1f + zoom), selectedScale.z * (1f + zoom));
				}
			}
			else if (selected)
			{
				selected.localScale = selectedScale;
				selected = null;
				timer = 0;
			}
		}
	}
}
