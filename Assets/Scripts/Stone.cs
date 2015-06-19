using UnityEngine;
using System.Collections;

// A single projectile of the slingshot (moves forward and bounces off rigid bodies)
public class Stone : MonoBehaviour {

	// One Prefab for every generated Stone
	public static Object stonePrefab = Resources.Load("Stone");

	// Time in seconds after which the stone stops moving
	public float timeToStop = 3;

	// Stones have got an initial state and current time to calculate movement
	private Vector3 startOrientation;
	private Vector3 startPosition;
	private float startVelocity;
	private float currentTime;

	// Sets the initial state
	public void setStartParameters(Vector3 startOrientation, Vector3 startPosition, float startVelocity) {
		this.startPosition = startPosition;
		this.startOrientation = startOrientation;
		this.startVelocity = startVelocity;
		this.currentTime = 0;
		transform.position = startPosition;
		transform.LookAt(startPosition + startOrientation);
	}

	// The Stone-prefab has to be existent so this script can be working
	void Start() {
		if (stonePrefab == null)
			enabled = false;
	}
	
	// Moves the stone until the time reaches his maximum
	void Update() {
		currentTime += Time.deltaTime;
		if (timeToStop > currentTime)
			transform.Translate(Physics.gravity * Mathf.Pow(Time.deltaTime, 2) + (1 - currentTime / timeToStop) * new Vector3(Vector3.forward.x, -startOrientation.normalized.y, Vector3.forward.z) * startVelocity * Time.deltaTime);
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
