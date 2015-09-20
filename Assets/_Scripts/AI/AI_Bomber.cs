using UnityEngine;
using System.Collections;

public class AI_Bomber : MonoBehaviour {
	
	public float radius;
	public float radiusOffset;
	public ParticleSystem radiusEffect;  
	private float radiusCollider;
	
	public float rotationDuration;
	public float rotationStDelay;
	public float rotationCooldown;
	private int rotationLeft;
	private float nextRotation;
	private float rotationAmount;
	private Transform target;
	private Vector2 targetpos; 

	void Start () {
		GameObject temp = GameObject.FindWithTag ("Player");
		if (temp != null) target = temp.GetComponent<Transform>();

		radiusCollider = radius - radiusOffset;
		radiusCollider = radiusCollider * radiusCollider;
		radiusEffect.startSize = radius;

		rotationLeft = 0;
		rotationAmount = 0;
		nextRotation = Time.time + rotationStDelay;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckDetection ();
		UpdateRotation ();
	}

	void CheckDetection(){
		if (target != null)
			targetpos = (Vector2) target.position;

		Vector2 curpos = transform.position;

		if ((targetpos - curpos).sqrMagnitude <= radiusCollider) {
			Explode();
		}
	}

	void UpdateRotation(){
		if (Time.time >= nextRotation) {
			nextRotation = nextRotation + rotationCooldown;
			rotationLeft = (int) Mathf.Floor(rotationDuration/Time.fixedDeltaTime);
			rotationAmount = GetRandomRotation() / rotationLeft;
		}
		
		if (rotationLeft>0){
			-- rotationLeft;
			transform.Rotate(0,0,rotationAmount);
		}
	}

	float GetRandomRotation(){
		float rot = Mathf.Floor (Random.Range (3, 6)) * 45;
		if (Random.value >= 0.5) rot = -rot; 
		return rot;
	}
 
	void Explode(){
		GetComponent<HealthManager>().Kill();
	}
}
