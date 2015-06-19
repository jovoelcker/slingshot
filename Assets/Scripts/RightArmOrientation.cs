using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

// The right arm uses the Myo-Wrapper to rotate
public class RightArmOrientation : MonoBehaviour
{
	public MyoOrientation rightMyoOrientation = null;
	
	// If there is no MyoOrientation object attached, this script can't work
	void Start() {
		if (rightMyoOrientation == null)
			enabled = false;
	}
	
	// Looks in the Wrapper attached objects direction
	void Update() {
		transform.LookAt(transform.position + rightMyoOrientation.myoHeadingTo.position);
	}
}
