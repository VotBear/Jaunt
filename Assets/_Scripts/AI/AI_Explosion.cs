using UnityEngine;
using System.Collections;

public class AI_Explosion : MonoBehaviour {

	public float duration;
	public float maxSize;
	public CircleCollider2D coll;

	private float curSize;
	private float startTime;
	private float endTime;

	void Start () {
		startTime = Time.time;
		endTime = startTime + duration;
		coll.radius = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float rad;
		if (Time.time <= endTime) {
			rad = maxSize * (Time.time - startTime) / duration;
		} else {
			rad = 0;
		}

		coll.radius = rad;
	}
}
