using UnityEngine;
using System.Collections;

public class AI_Sniper : MonoBehaviour {

	public float initdelay;
	public float cooldown;
	public float rotationSpeed;
	public Transform spawnSpot;
	public GameObject projectile;
	
	private int coold;
	private float speed;
	private float nextShot;
	private Transform target;
	private Vector2 pos;
	private Vector2 randompos;

	void Start () {   
		GameObject temp = GameObject.FindWithTag ("Player");
		if (temp != null) target = temp.GetComponent<Transform>();

		pos = transform.position;
		speed = rotationSpeed * Time.fixedDeltaTime;
		nextShot = Time.time + initdelay;
		
		coold = 0;
	}

	void FixedUpdate () {
		if (Time.time >= nextShot) { 
			Shoot();
			nextShot = Time.time + cooldown;
		}

		float rot = FindRotation ();
		transform.Rotate (new Vector3( 0f, 0f, rot));
	}

	void Shoot(){
		GameObject bullet = Instantiate (projectile, spawnSpot.position, Quaternion.identity) as GameObject;
		bullet.GetComponentInChildren<AI_Projectile> ().SetAngle (transform.eulerAngles.z);
	}

	float FindRotation(){

		Vector2 tar;
		if (target != null)
			tar = target.transform.position;
		else
			tar = getRandom();

		float dx = tar.x - pos.x;
		float dy = tar.y - pos.y;

		float targetRot = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg;
		float currentRot = transform.rotation.eulerAngles.z;

		float tmp = targetRot - currentRot;
		while (tmp < 0)
			tmp = 360 + tmp;

		if (tmp <= 180) 
			return Mathf.Min( speed, tmp);
		else 
			return Mathf.Max(-speed, tmp-360);

	}
	
	Vector2 getRandom(){
		if (coold == 0) {
			randompos = GameController.GetRandomCoord();
			coold = (int) Mathf.Floor(Random.Range(1f,1.5f) / Time.fixedDeltaTime);
		} else {
			--coold;
		}
		return randompos;
	}
}
