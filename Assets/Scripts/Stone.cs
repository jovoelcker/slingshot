using UnityEngine;
using System.Collections;

// A single projectile of the slingshot (moves forward and bounces off rigid bodies)
[RequireComponent (typeof (Rigidbody))]
public class Stone : MonoBehaviour {

	// One Prefab for every generated Stone
	public static Object stonePrefab = Resources.Load("Stone");

	private Rigidbody rigidbody;

	// Sets the initial state
	public void setStartParameters(Vector3 startOrientation, Vector3 startPosition, float startVelocity) {
		transform.position = startPosition;
		transform.LookAt(startPosition + startOrientation);

		rigidbody = GetComponent<Rigidbody>();
		rigidbody.AddForce(startOrientation.normalized * startVelocity);
	}

	// The Stone-prefab has to be existent so this script can be working
	void Start() {
		if (stonePrefab == null)
			enabled = false;
	}
	
	// Moves the stone until the time reaches his maximum
	void Update() {

	}

	// Sends a message to every penetrated trigger
	void OnTriggerEnter(Collider other) {
		other.SendMessage("Hit");
	}

	// Creates a new Stone-instance with initial state
	public static void CreateStone(Vector3 startOrientation, Vector3 startPosition, float startVelocity) {
		if (startVelocity > 0) {
			GameObject clone = (GameObject)Instantiate(stonePrefab);
			clone.GetComponent<Stone>().setStartParameters(startOrientation, startPosition, startVelocity);
		}
	}
}
