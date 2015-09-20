using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerJauntBar : MonoBehaviour { 

	public enum BarType{
		front, back
	}
	
	public JauntManager jauntManager; 
	public BarType barType;

	public Image img;  
	
	private RectTransform trans;
	private float maxAxisSize;
	private float maxEner;
	private float curEner;  

	void Start () {
		trans = GetComponent<RectTransform>();

		maxEner = jauntManager.GetMaxEnergy ();
		curEner = maxEner;

		maxAxisSize = trans.localScale.y; 
	}
	 
	void Update () {
		if (barType == BarType.front) {
			curEner = jauntManager.GetPreEnergy(); 
		} else {
			curEner = jauntManager.GetCurEnergy();
		}

		float rat = curEner / maxEner;
		Vector3 cur = trans.localScale;

		trans.localScale = new Vector3(cur.x, rat * maxAxisSize, cur.z); 
   
	}
} 