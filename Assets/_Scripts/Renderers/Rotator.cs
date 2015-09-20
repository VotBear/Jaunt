using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public Vector3 rotationMin;
	public Vector3 rotationMax;

	public bool canReverse;
	
	private Vector3 rotation;

	void Start(){
		float rotationX = Random.Range(rotationMin.x, rotationMax.x);
		float rotationY = Random.Range(rotationMin.y, rotationMax.y);
		float rotationZ = Random.Range(rotationMin.z, rotationMax.z);

		rotation = new Vector3 (rotationX, rotationY, rotationZ);

		if (canReverse && Random.value >= 0.5f) {
			rotation = rotation*-1;
		}
	}

	void FixedUpdate(){
		Vector3 rotate = rotation * Time.fixedDeltaTime;
		transform.Rotate (rotate);
	}
}
