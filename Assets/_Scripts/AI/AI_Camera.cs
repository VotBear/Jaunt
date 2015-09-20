using UnityEngine;
using System.Collections;

public class AI_Camera : MonoBehaviour {
	
	public Transform target;
	public float speed; 
	public float speedupMult;
	public Vector2 XYMin;
	public Vector2 XYMax; 
	
	//private Rigidbody2D rb;
	private Vector3 targetpos;
	private float newX, newY;

	void Start () {
		//rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindWithTag ("Player").GetComponent<Transform>(); 
	}
	
	void FixedUpdate () 
	{	
		if (target!=null) targetpos = target.position; 
		
		targetpos.x = Mathf.Clamp (targetpos.x, XYMin.x, XYMax.x);
		targetpos.y = Mathf.Clamp (targetpos.y, XYMin.y, XYMax.y);

		Vector2 diff = targetpos - transform.position;
		Vector2 norm = diff.normalized * speed * Time.fixedDeltaTime;

		if (Mathf.Abs (diff.x) < Mathf.Abs (norm.x) || Mathf.Abs (diff.y) < Mathf.Abs (norm.y)) {
			newX = targetpos.x;
			newY = targetpos.y;

		} else if (diff.magnitude >= speedupMult * norm.magnitude){
			newX = transform.position.x + diff.x / speedupMult;
			newY = transform.position.y + diff.y / speedupMult;

		} else {
			newX = transform.position.x + norm.x;
			newY = transform.position.y + norm.y;
		}

		transform.position = new Vector3( newX, newY, transform.position.z);
	}
}
