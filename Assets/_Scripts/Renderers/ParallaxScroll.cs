using UnityEngine;
using System.Collections;

public class ParallaxScroll : MonoBehaviour {

	public Transform cam;
	public float ratio;

	
	void Update () {
		transform.position = cam.position * ratio;
	}
}
