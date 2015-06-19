using UnityEngine;
using System.Collections;

// Used to rotate the body in X- and Z-direction
public class BodyOrientation : MonoBehaviour
{
	public MyoOrientation myoOrientation;
	
	// The body is rotated by the left Myo
	void Update() {
		transform.LookAt(new Vector3(myoOrientation.myoHeadingTo.position.x, 0, myoOrientation.myoHeadingTo.position.z));
	}
}
