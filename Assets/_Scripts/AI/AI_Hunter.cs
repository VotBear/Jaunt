using UnityEngine;
using System.Collections;

public class AI_Hunter : MonoBehaviour {
	
	public float speedMinSt;
	public float speedMaxSt;
	public float speedMax;
	public float speedInc;
	//public float speedInc;

	private Rigidbody2D rb;
	private Transform target;
	private Vector3 targetpos; 
	private Vector2 randompos; 
	private int cooldown;
	private float speed;

	void Start () {
		rb = GetComponent<Rigidbody2D> (); 

		GameObject temp = GameObject.FindWithTag ("Player");
		if (temp != null) target = temp.GetComponent<Transform>();

		cooldown = 0; 
		speed = Random.Range (speedMinSt, speedMaxSt);
	}
	
	void FixedUpdate () {

		speed = Mathf.Min (speedMax, speed + speedInc*Time.fixedDeltaTime);

		if (target != null)
			targetpos = target.position;
		else
			targetpos = getRandom ();
	
		Vector2 diff = targetpos - transform.position; 
		rb.velocity = (diff.normalized * speed);

	}

	Vector2 getRandom(){
		if (cooldown == 0) {
			randompos = GameController.GetRandomCoord();
			cooldown = (int) Mathf.Floor(Random.Range(3f,7f) / Time.fixedDeltaTime);
		} else {
			--cooldown;
		}
		return randompos;
	}
}
