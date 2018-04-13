using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public float startDuration = 20f;
	public float power = 0.03f;
	Vector3 originalPos;
	Vector3 randomDirection;
	Vector3 dirOrigin;
	float duration;
	bool toOrigin = false;
	Transform board;

	void Start()
	{
		duration = startDuration;
		originalPos = transform.position;
		randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		board = GameObject.FindWithTag("Board").transform;
	}
	void Update()
	{
		float speed = Mathf.PingPong(Time.time, startDuration / 4) / (startDuration / 4);
		if (!toOrigin)
		{
			transform.position += randomDirection * power * Time.deltaTime * speed;
			transform.LookAt(board);
			duration -= Time.deltaTime;
			if (duration <= startDuration / 2)
			{
				dirOrigin = (originalPos - transform.position).normalized;
				toOrigin = true;
			}
		}
		else
		{
			transform.position += dirOrigin * power * Time.deltaTime * speed;
			transform.LookAt(board);
			duration -= Time.deltaTime;
			if (duration <= 0)
			{
				randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
				duration = startDuration;
				transform.position = originalPos;
				toOrigin = false;
			}
		}
	}
}