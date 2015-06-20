using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

// Uses the left Myo to adjust the slingshot-height
public class LeftArmOrientation : MonoBehaviour
{
	public MyoManager myoManager = null;
	
	// If there is no MyoManager attached, this script can't work
	void Start() {
		if (myoManager == null)
			enabled = false;
	}
	
	// If the Myo is available, adjust the height
	void Update() {
		if (myoManager.leftMyo != null)
			transform.localRotation = new Quaternion(-myoManager.leftMyo.transform.rotation.z, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
	}
}
