using UnityEngine;
using System.Collections;

// The right forearm looks towards the middle of the slingshot to provide easy interaction
public class RightForeArmOrientation : MonoBehaviour {

	public Transform slingshotMiddle = null;

	// If there is no slingshot middle, this script can't work
	void Start() {
		if (slingshotMiddle == null)
			enabled = false;
	}
	
	// Looks towards the middle of the slingshot
	void Update() {
		transform.LookAt(slingshotMiddle.position);
	}
}
