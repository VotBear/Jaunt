using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialActivator : MonoBehaviour {

	public int startPage;
	public int endPage; 
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*if (TutorialController.pageno >= startPage && TutorialController.pageno <= endPage) {
			gameObject.SetActive(true);	
			Debug.Log("YES");
		} else {
			gameObject.SetActive(false); 
		}*/
	}

	public void SetActiveChildren(float alpha){
		MaskableGraphic[] temp = GetComponentsInChildren<MaskableGraphic> ();
		Color tcolor;
		foreach (MaskableGraphic tmp in temp) {
			tcolor = tmp.color;
			tcolor.a = alpha;
			tmp.color = tcolor;
		}
	}
}
