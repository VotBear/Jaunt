using UnityEngine;
using System.Collections;

public class TransparencyOscillator : MonoBehaviour {

	public TransparencyManager transp;

	public float minAlpha;
	public float maxAlpha;
	public float minDuration;
	public float maxDuration;
	public float minPauseDuration;
	public float maxPauseDuration;


	// Use this for initialization
	void Start () {
		StartCoroutine (RepeatOscillate());
	}

	
	IEnumerator RepeatOscillate() {

		while(true){
			float nextar = Random.Range (minAlpha, maxAlpha);
			float nexdur = Random.Range (minDuration,maxDuration);
 

			transp.OscillateTransparency (0, nextar, nexdur);
		
			yield return new WaitForSeconds(4*nexdur ); 
 

			yield return new WaitForSeconds( Random.Range( minPauseDuration, maxPauseDuration) );
		}
	}
}
