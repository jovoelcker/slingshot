using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

// Uses the left Myo to adjust the slingshot-height
public class LeftArmOrientation : MonoBehaviour
{
	public MyoManager myoManager = null;

	private int accidental = 1;
	private bool axisIsZ = true;
	
	// If there is no MyoManager attached, this script can't work
	void Start() {
		if (myoManager == null)
			enabled = false;
	}
	
	// If the Myo is available, adjust the height
	void Update() {
		// Sometimes the Myo changes between Z- and X-Axes for the pitch of the arm
		if (Input.GetAxis("SetLeftArmX") > 0)
			axisIsZ = false;
		else if (Input.GetAxis("SetLeftArmZ") > 0)
			axisIsZ = true;

		// It also switches the direction of the axes sometimes
		if (Input.GetAxis("SetLeftArm+") > 0)
			accidental = 1;
		else if (Input.GetAxis("SetLeftArm-") > 0)
			accidental = -1;

		// If everythings set up, the arms height will be mapped correctly
		if (myoManager.leftMyo != null)
			transform.localRotation = new Quaternion(accidental * (axisIsZ ? myoManager.leftMyo.transform.rotation.z : myoManager.leftMyo.transform.rotation.x), transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
	}
}
