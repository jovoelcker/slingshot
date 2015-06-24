using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Shoot : MonoBehaviour {

	// Both player fists
	public Transform rightHand;
	public Transform slingshotMiddle;

	// Maximum of the stones startVelocity in m/s
	public float minVelocity = 13000f;
	public float maxVelocity = 30000f;

	// Shooting-sound of the slingshot
	AudioSource shootSound;

	// Has the sling been snapped in the last frame?
	private bool wasSnapped = false;

	// Current state for the trajectory-calculation
	private Vector3 currentOrientation;
	private Vector3 currentPosition;
	private float currentVelocity;

	// Maximum and minimum distance between both fists
	private float maxDistance = 0;
	private float minDistance = 0;

	// Use this for initialization
	void Start() {
		shootSound = GetComponent<AudioSource>();

		// Both fists have to be set to work properly
		if (rightHand == null || slingshotMiddle == null)
			enabled = false;
	}
	
	// Update is called once per frame
	void Update() {
		
		// Get the current distance between the fists
		float distance = GetDistance();
		
		// Update the maximum distance if the current distance is bigger
		if (maxDistance < distance)
			maxDistance = distance;
		
		// Get the percentage of tension
		float t = Mathf.InverseLerp(minDistance, maxDistance, distance);
		
		// Get the input axis for snapping the sling (PS Move Back-Key)
		bool isSnapped = Input.GetAxis("Snap") > 0;
		
		if (wasSnapped) {
			// Update the state for the trajectory-calculation
			currentOrientation = slingshotMiddle.position - rightHand.position;
			currentPosition = slingshotMiddle.position;
			currentVelocity = Mathf.Lerp(minVelocity, maxVelocity, t);;
			
			// The sling was just released
			if (!isSnapped) {
				wasSnapped = false;
				shootSound.Play();
				// Create a new Stone-object with current state for the trajectory-calculation
				Stone.CreateStone(currentOrientation, currentPosition, currentVelocity);
			}
		}
		// The sling was just snapped
		else if (isSnapped && distance < 1) {
			wasSnapped = true;
			// Set the minDistance as current distance for a smooth slingshot-animation
			minDistance = distance;
		}
	}

	// Gets the distance of the two fists
	public float GetDistance() {
		return Vector3.Distance(slingshotMiddle.position, rightHand.position);
	}

	public bool WasSnapped() {
		return wasSnapped;
	}
}