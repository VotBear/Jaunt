using UnityEngine;
using System.Collections;

public class AI_Projectile : MonoBehaviour {
	
	public float minSpeed;
	public float maxSpeed;
	public float speedInc; 
	
	private Rigidbody2D rb;  
	private float speed;
	
	void Start () { 
		
		speed = minSpeed;
		rb = GetComponent<Rigidbody2D> (); 
	}
	
	void FixedUpdate () { 
		speed = Mathf.Clamp (speed + speedInc * Time.fixedDeltaTime, minSpeed, maxSpeed);
		rb.velocity = transform.right * speed;
	} 
	
	public void SetAngle(float angle){
		transform.Rotate (0f, 0f, angle);
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			this.GetComponent<HealthManager>().Damage(100);
		}
	}
}
