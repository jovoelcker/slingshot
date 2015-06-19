using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

// Manages the two Myos and is able to switch their sides
public class MyoManager : MonoBehaviour {

	ThalmicMyo myo1, myo2;

	// Expected left and right Myos
	public ThalmicMyo leftMyo = null;
	public ThalmicMyo rightMyo = null;

	bool switchMyos = false;

	// Saves the Myos for switching
	void Start () {
		myo1 = leftMyo;
		myo2 = rightMyo;
	}
	
	// If switching of the sides is called, it's performed
	void Update() {
		if (Input.GetAxis("SetMyo1Left") > 0) {
			leftMyo = myo1;
			rightMyo = myo2;
		}
		else if (Input.GetAxis("SetMyo1Right") > 0) {
			leftMyo = myo2;
			rightMyo = myo1;
		}
	}
}
