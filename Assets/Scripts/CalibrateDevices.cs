﻿using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

// Calibrates all the used devices
public class CalibrateDevices : MonoBehaviour
{
	// The Myo-Wrapper which have to be calibrated
	public MyoOrientation leftMyoOrientation = null;
	public MyoOrientation rightMyoOrientation = null;

	// The Myos themselves
	private ThalmicMyo leftMyo = null;
	private ThalmicMyo rightMyo = null;

	// If the script got the wrapper, it extracts the Myos
	void Start() {
		if (leftMyoOrientation != null && leftMyoOrientation.myoManager != null)
			leftMyo = leftMyoOrientation.myoManager.leftMyo;
		if (rightMyoOrientation != null && rightMyoOrientation.myoManager != null)
			rightMyo = rightMyoOrientation.myoManager.rightMyo;
	}
	
	// Checking for calibration command every frame
	void LateUpdate() {
		// Update references when the Calibrate-Axis is positive (Default: Right Mouse-Key)
		bool updateReference = false;
		if (Input.GetAxis("Calibrate") > 0) {
			updateReference = true;
		}

		// If the key is pressed, calibrate
		if (updateReference) {
			if (leftMyo != null) {
				CalibrateMyo(leftMyo, leftMyoOrientation);
			}
			
			if (rightMyo != null) {
				CalibrateMyo(rightMyo, rightMyoOrientation);
			}
		}
	}

	// Using the Thalmic Labs provided functionality
	void CalibrateMyo(ThalmicMyo myo, MyoOrientation myoOrientation) {
		// _antiYaw represents a rotation of the Myo armband about the Y axis (up) which aligns the forward
		// vector of the rotation with Z = 1 when the wearer's arm is pointing in the reference direction.
		Quaternion _antiYaw = Quaternion.FromToRotation(
			new Vector3(myo.transform.forward.x, 0, myo.transform.forward.z),
			new Vector3(0, 0, 1)
		);
		
		// _referenceRoll represents how many degrees the Myo armband is rotated clockwise
		// about its forward axis (when looking down the wearer's arm towards their hand) from the reference zero
		// roll direction. This direction is calculated and explained below. When this reference is
		// taken, the joint will be rotated about its forward axis such that it faces upwards when
		// the roll value matches the reference.
		Vector3 referenceZeroRoll = computeZeroRollVector(myo.transform.forward, myo);
		float _referenceRoll = rollFromZero(referenceZeroRoll, myo.transform.forward, myo.transform.up);
		
		myoOrientation.updateReference(_antiYaw, _referenceRoll);
	}
	
	// Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction.
	// As the armband is rotated about the forward axis this value will change, regardless of which way the
	// forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees.
	float rollFromZero(Vector3 zeroRoll, Vector3 forward, Vector3 up)
	{
		// The cosine of the angle between the up vector and the zero roll vector. Since both are
		// orthogonal to the forward vector, this tells us how far the Myo has been turned around the
		// forward axis relative to the zero roll vector, but we need to determine separately whether the
		// Myo has been rolled clockwise or counterclockwise.
		float cosine = Vector3.Dot(up, zeroRoll);
		
		// To determine the sign of the roll, we take the cross product of the up vector and the zero
		// roll vector. This cross product will either be the same or opposite direction as the forward
		// vector depending on whether up is clockwise or counter-clockwise from zero roll.
		// Thus the sign of the dot product of forward and it yields the sign of our roll value.
		Vector3 cp = Vector3.Cross(up, zeroRoll);
		float directionCosine = Vector3.Dot(forward, cp);
		float sign = directionCosine < 0.0f ? 1.0f : -1.0f;
		
		// Return the angle of roll (in degrees) from the cosine and the sign.
		return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
	}
	
	// Compute a vector that points perpendicular to the forward direction,
	// minimizing angular distance from world up (positive Y axis).
	// This represents the direction of no rotation about its forward axis.
	Vector3 computeZeroRollVector(Vector3 forward, ThalmicMyo myo)
	{
		Vector3 antigravity = Vector3.up;
		Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
		Vector3 roll = Vector3.Cross(m, myo.transform.forward);
		
		return roll.normalized;
	}
}
