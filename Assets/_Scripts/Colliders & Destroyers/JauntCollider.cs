using UnityEngine;
using System.Collections;

public class JauntCollider : MonoBehaviour {

	public float damage;
	public string[] tags;
	
	private bool destroyable;
	private Collider2D jauntCollider;
 
	void Start () {
		jauntCollider = GetComponent<Collider2D> ();
		destroyable = false;
	}
	 
	void Update () {  
		CheckAndDestroy ();
		if (destroyable) {
			Destroy();
		}
	}

	void FixedUpdate(){
		destroyable = true;
	}

	void CheckAndDestroy(){
		foreach (var tag in tags){
			GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);  

			foreach (var target in targets){	
				if (jauntCollider.IsTouching(target.GetComponent<Collider2D>())){ 
					//Debug.Log ("Yes");
					//other.GetComponentInParent<Rotator>().enabled = false;
					//Destroy(other.gameObject);
					target.GetComponent<HealthManager>().Damage(1);
				} 	
			}
		}
		destroyable = true; 
	}

	/*
	void OnTriggerStay2D(Collider2D other)
	{
		bool hit = false;

		foreach (var tag in tags) if (other.tag == tag) hit = true;
		if (!hit) return;

		if (jauntCollider.IsTouching(other)){ 
			//Debug.Log ("Yes");
			//other.GetComponentInParent<Rotator>().enabled = false;
			//Destroy(other.gameObject);
			other.GetComponent<HealthManager>().Damage(1);
		} 
		destroyable = true; 
	}*/

	public void Create (Vector2 st, Vector2 ed){
		//X is length, Y is width (dont change)
		float xpos = (st.x + ed.x) / 2f;
		float ypos = (st.y + ed.y) / 2f; 
		float dx = (ed.x - st.x);
		float dy = (ed.y - st.y);

		float length = Mathf.Sqrt ( dx*dx + dy*dy );
		float rotation = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg;

		transform.position = new Vector2 (xpos, ypos);
		transform.localScale = new Vector2 (length, transform.localScale.y);
		transform.Rotate (0f, 0f, rotation, Space.Self);
		
		destroyable = false;
		//jauntCollider.bounds.Intersects
	} 

	void Destroy(){
		Destroy(gameObject);
	}
}
