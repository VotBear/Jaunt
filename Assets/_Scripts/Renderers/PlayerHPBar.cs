using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour {
	 
	public enum BarType{
		currentHP, backHP	
	}
	
	public HealthManager hpManager; 
	public BarType barType;
	public float backHealthSpeed; 

	public Image img; 
	public Color[] colors;
	
	private RectTransform trans;
	private float maxAxisSize;
	private float maxHP;
	private float curHP; 
	private float bacHP;

	void Start () {
		trans = GetComponent<RectTransform>();

		maxHP = hpManager.GetMaxHealth ();
		curHP = maxHP;
		bacHP = curHP;

		maxAxisSize = trans.localScale.y; 
	}
	 
	void Update () {
		if (barType == BarType.currentHP) {
			curHP = hpManager.GetCurrentHealth ();
		
		} else {
			bacHP = hpManager.GetCurrentHealth ();
			if (curHP > bacHP) curHP = Mathf.Clamp (curHP - backHealthSpeed*Time.fixedDeltaTime, bacHP, maxHP); 
			else curHP = bacHP; 
		}

		float rat = curHP / maxHP;
		Vector3 cur = trans.localScale;

		trans.localScale = new Vector3(cur.x, rat * maxAxisSize, cur.z); 
 
		GetColor ();
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
		
		if (img != null)
			img.color = color; 

	}
}
