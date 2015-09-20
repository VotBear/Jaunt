using UnityEngine;
using System.Collections;

public class JauntManager : MonoBehaviour {
	
	public float maxEnergy;
	public float regenRate;
	public float regenFastRate;
	public float backjauntThreshold;

	public GameObject coll;
	public SpriteRenderer lineSprite;
	public SpriteRenderer mainSprite;
	public PlayerController playerCont;

	private bool backjaunt;
	private bool lclick;
	private bool rclick;
	private float backjauntTimer;
	private float scaleMult; 
	private float curEnergy; 
	private float preEnergy;
 
	private Vector2 backjauntDest;

	void Start () {  

		scaleMult = 0.78f;
		curEnergy = maxEnergy;
		preEnergy = curEnergy;

		backjaunt = false; 
	}
	
	void ReadInput() {
		lclick |= Input.GetMouseButtonDown (0);
		rclick |= Input.GetMouseButtonDown (1);
	}
	
	void ResetInput() { 
		lclick = false;
		rclick = false;
	}
	
	void Update() {
		if (!GameController.paused){
			SetPreview ();
			ReadInput ();
		}
	}
	
	void FixedUpdate(){
		float regen = regenRate * Time.fixedDeltaTime;
		if (playerCont.IsSlow()) regen = regenFastRate * Time.fixedDeltaTime;

		curEnergy = Mathf.Clamp (curEnergy + regen, 0, maxEnergy);

		if (lclick) JauntCheck();	
		if (rclick) BackJauntCheck();
		
		ResetInput ();
	}
	
	void BackJauntCheck()
	{
		if (backjaunt && Time.time <= backjauntTimer) 
		{
			backjaunt = false; 
			float mag = (backjauntDest - (Vector2) transform.position).magnitude;
			UseEnergy (mag/2f);
			Jaunt (transform.position, backjauntDest);
			playerCont.SlowPlayer();
		}
	}
	
	void JauntCheck()
	{
		backjaunt = true;
		backjauntDest = transform.position; 
		backjauntTimer = Time.time + backjauntThreshold; 

		Vector2 destination = getJauntDestination ();
		float mag = (destination - (Vector2) transform.position).magnitude;
		UseEnergy (mag);

		Jaunt (transform.position, destination);	
	}
	
	void Jaunt(Vector2 source, Vector2 destination){	
		
		transform.position = destination; 
		GameObject jauntcoll = Instantiate (coll, new Vector3(destination.x,destination.y,0f), Quaternion.identity) as GameObject;
		jauntcoll.GetComponent<JauntCollider> ().Create (source, destination);  
		
		playerCont.AfterJaunt ();

	} 

	void UseEnergy(float mag){
		curEnergy = Mathf.Clamp(curEnergy-mag, 0, maxEnergy);
	}

	Vector2 getJauntDestination(){
		Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		destination.x = Mathf.Clamp (destination.x, GameController.XYMin.x, GameController.XYMax.x);
		destination.y = Mathf.Clamp (destination.y, GameController.XYMin.y, GameController.XYMax.y);

		destination = CheckDistance (destination);
		return destination;
	}

	void SetPreview(){
		Vector2 cur = transform.position;
		Vector2 des = getJauntDestination ();
		Vector2 mid = (cur + des) / 2;
		float rot = Mathf.Atan2 (des.y - cur.y, des.x - cur.x) * Mathf.Rad2Deg;
		float mag = (des - cur).magnitude;

		mainSprite.transform.position = des;

		lineSprite.transform.position = mid;
		lineSprite.transform.localScale = new Vector3(mag * scaleMult, 1f, 1f); 
		lineSprite.transform.localEulerAngles = new Vector3(0f, 0f, rot + 180);

		preEnergy = curEnergy - mag;
	}

	Vector2 CheckDistance(Vector2 tar){
		Vector2 ret = tar;
		Vector2 now = transform.position;
		float dist = (tar-now).magnitude;

		if (curEnergy < dist) {
			float newmag = curEnergy/dist;
			ret = now + ((tar-now)*newmag);
		}
		return ret;

	}
	
	public float GetMaxEnergy(){
		return maxEnergy;
	}
	
	public float GetPreEnergy(){
		return preEnergy;
	}
	
	public float GetCurEnergy(){
		return curEnergy;
	}
}
