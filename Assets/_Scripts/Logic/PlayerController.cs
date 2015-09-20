using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public static PlayerController instance;

	public float minSpeed;
	public float maxSpeed;
	public float sloSpeed;
	public float fastRegenCooldown;
	public float speedRecovRate; 
	public GameObject deathAnim; 

	private float speed;
	private int isSlow;

	private Rigidbody2D rb;
	private HealthManager hpManager;

	// Use this for initialization
	void Start () 
	{
		instance = this;

		speed = minSpeed; 
		rb = GetComponent<Rigidbody2D> ();
		hpManager = GetComponent<HealthManager> (); 
	}
 
	void FixedUpdate () 
	{ 
		isSlow = Mathf.Max(isSlow - 1, 0); 

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical"); 
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		float spd = Mathf.Clamp (speed + speedRecovRate * Time.fixedDeltaTime, minSpeed, maxSpeed);

		speed = spd;
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = Mathf.Min(spd,sloSpeed);
		}
		rb.velocity = movement * speed; 
		
		if (rb.velocity.magnitude >= sloSpeed * 1.5) {
			RefreshRegenCooldown();
		}
	} 

	public void Damage(int dmg){
		hpManager.Damage (dmg);
	}
	
	public void AfterJaunt(){
		RefreshRegenCooldown(); 
	}
	
	public void SlowPlayer(){ 
		speed = minSpeed;
	}

	public bool IsSlow(){
		return (isSlow <= 0);
	}

	void RefreshRegenCooldown(){ 
		isSlow = (int)	Mathf.Floor(fastRegenCooldown / Time.fixedDeltaTime); 
	}

	void OnDestroy(){
		GameObject temp = GameObject.FindWithTag ("GameController");
		if (temp != null) temp.GetComponent<GameController>().PlayerDeath();
	}
}
