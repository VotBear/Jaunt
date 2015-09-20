using UnityEngine;
using System.Collections;

public class EnemyHPBar : MonoBehaviour {
	
	
	public enum BarAxis{
		vertical, horizontal
	} 

	public Transform mainBody;
	public HealthManager hpManager;
	public BarAxis barAxis;  
	public bool invisIfFull;
	 
	public SpriteRenderer sprite;
	public Color[] colors;
	
	private Transform trans;
	private Vector3 pos;
	private float maxAxisSize;
	private float maxHP;
	private float curHP;  
	
	void Start () {
		trans = GetComponent<Transform>();
		pos = trans.localPosition;

		maxHP = hpManager.GetMaxHealth ();
		curHP = maxHP; 
		
		if (barAxis == BarAxis.vertical) maxAxisSize = trans.localScale.y;
		else maxAxisSize = trans.localScale.x;
	}
	
	void Update () { 

		//pos and rotation
		trans.position = mainBody.transform.position;
		trans.Translate (pos);
		//trans.rotation = Quaternion.identity();

		//size
		curHP = hpManager.GetCurrentHealth ();  
		float rat = curHP / maxHP;
		Vector3 cur = trans.localScale;
		if (barAxis == BarAxis.vertical) trans.localScale = new Vector3(cur.x, rat * maxAxisSize, cur.z);
		else trans.localScale = new Vector3(rat * maxAxisSize, cur.y, cur.z);

		CheckIfFull ();
		GetColor ();
	}
	
	void CheckIfFull(){
		if (invisIfFull) { 
			if (sprite != null)
				sprite.enabled = (curHP != maxHP); 
		}
	}
	
	void GetColor(){
		Color color;
		float ratio = 1 - (curHP / maxHP); 
		
		if (colors.Length == 0) {
			return;
			
		} else if (colors.Length == 1) {
			color = colors[0];
			
		} else if (colors.Length == 2) {
			color = Color.Lerp (colors [0], colors [1], ratio);
			
		} else {
			float colorRatio = ratio * (colors.Length-1);
			int min = (int) Mathf.Floor(colorRatio);
			float lerp = Mathf.InverseLerp(min, min+1, colorRatio); 
			color = Color.Lerp(colors[min], colors[min+1], lerp); 
		}
		 
		if (sprite != null) {
			sprite.color = color; 
		}
		
	}
}
