using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	
	public float StX;
	public float EdX;
	public float StY;
	public float EdY;
	public float Speed;
	
	//private Rigidbody2D rb;
	
	void Start(){
		//rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate(){
		transform.position = new Vector2(transform.position.x + Speed * Time.fixedDeltaTime, transform.position.y);
		if (transform.position.x >= EdX) {
			transform.position = new Vector2(StX, Random.Range (StY, EdY));
		}
	}
}
