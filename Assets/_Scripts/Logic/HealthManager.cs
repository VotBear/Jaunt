using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	public float maxHealth;
	public float invulDuration;
	public float trailDeathDelay;
	public GameObject hitAnimation;
	public GameObject deathAnimation;
	public GameObject parentObject;
	public bool respawn;

	private float curHealth;
	private int invul;
	private bool isInvul;

	private float eps = 1e-7f;

	void Start () {
		curHealth = maxHealth; 
	}
	 
	void FixedUpdate () { 
		if (invul > 0) {
			--invul;
			isInvul = (invul > 0);
		}
	}

	public void Damage(float dmg){
		if (isInvul || curHealth <= eps)
			return;
		
		curHealth = Mathf.Clamp (curHealth - dmg, 0f, maxHealth);
	
		if (hitAnimation != null) {
			Instantiate (hitAnimation, transform.position, transform.rotation);
			//hit.transform.parent = transform;
		}

		if (curHealth <= eps) {
			Death ();
		} else if (invulDuration > 0) {
			isInvul = true;
			invul = (int) Mathf.Floor(invulDuration / Time.fixedDeltaTime);
		}
	}

	public void Kill(){
		curHealth = 0;
		Death ();
	}

	void Death(){
		if (deathAnimation != null) 
			Instantiate (deathAnimation, transform.position, transform.rotation);

		SeparateTrails ();
		if (!respawn) {
			Destroy (parentObject.gameObject);
		} else {
			curHealth = maxHealth;
			parentObject.transform.position = new Vector2(0f,0f);
		}
	}

	void SeparateTrails(){ 
		Transform[] allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform child in allChildren){
			if(child.CompareTag("Trail")){
				child.parent = null;
				Destroy(child.gameObject , trailDeathDelay);
			}
		}  
	} 
	
	public float GetMaxHealth(){
		return maxHealth;
	}
	
	public float GetCurrentHealth(){
		return curHealth;
	} 

}
