using UnityEngine;
using System.Collections;

public class AI_Drifter : MonoBehaviour {
	
	public float minSpeed;
	public float maxSpeed;
	public float speedInc;
	public float radius;

	private Rigidbody2D rb;  
	private float speed;

	void Start () { 

		speed = minSpeed;
		rb = GetComponent<Rigidbody2D> ();
		transform.Rotate (0f, 0f, Random.Range(0,360));
	}

	void FixedUpdate () {
		 
		//flip against y axis, z -> 180-z (rotate by -2z + 180)
		if (transform.position.x - radius >= GameController.XYMax.x ||
		    transform.position.x + radius <= GameController.XYMin.x) { 
			transform.Rotate(0f, 0f, (-2 * transform.eulerAngles.z + Random.Range(-5,5)) + 180f);  
		}
		
		//flip against x axis, z -> -z (rotate by -2z)
		if (transform.position.y - radius >= GameController.XYMax.y ||
		    transform.position.y + radius <= GameController.XYMin.y) { 
			transform.Rotate(0f, 0f, (-2 * transform.eulerAngles.z + Random.Range(-5,5))) ;  
		}

		speed = Mathf.Clamp (speed + speedInc * Time.fixedDeltaTime, minSpeed, maxSpeed);
		rb.velocity = transform.right * speed;
	}
	
	 
}
