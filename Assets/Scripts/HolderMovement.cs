using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderMovement : MonoBehaviour {
	public float startDuration = 10f;
	public float power = 0.03f;
	public float originDir = 1f;

	bool toOrigin = false;
	float duration;
	Vector3 originRotation;
	float timer;

	void Start()
	{
		duration = startDuration;
		originRotation = transform.eulerAngles;
		timer = 0f;
	}

	void Update()
	{
		float speed = Mathf.PingPong(timer, startDuration / 4) / (startDuration / 4);
		if (!toOrigin)
		{
			transform.Rotate(new Vector3(originDir * power * speed * Time.deltaTime, 0, 0));
			if (duration <= startDuration / 2)
			{
				// transform.eulerAngles = new Vector3(rotationMax, transform.rotation.y, transform.rotation.z);
				toOrigin = true;
			}
		}
		else
		{
			transform.Rotate(new Vector3(originDir * -power * speed * Time.deltaTime, 0, 0));
			if (duration <= 0)
			{
				transform.eulerAngles = originRotation;
				duration = startDuration;
				toOrigin = false;
			}
		}
		duration -= Time.deltaTime;
		timer += Time.deltaTime;
	}
}