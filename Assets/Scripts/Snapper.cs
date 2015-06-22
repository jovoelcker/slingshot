using UnityEngine;
using System.Collections;

public class Snapper : MonoBehaviour {

	public Shoot shootScript = null;
	public Transform slingshotLeft = null;
	public Transform slingshotRight = null;
	public Transform slingLeftWrapper = null;
	public Transform slingRightWrapper = null;
	public Transform rightHand = null;

	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		// This script only works with these objects
		if (shootScript == null || slingshotLeft == null || slingshotRight == null || slingLeftWrapper == null || slingRightWrapper == null || rightHand == null)
			enabled = false;
		else {
			startPosition = transform.localPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Fixate the anchor at the right position
		if (shootScript.WasSnapped())
			transform.position = rightHand.position;
		else
			transform.localPosition = startPosition;

		// First the Wrapper have to be positioned between slingshot and snapper
		slingLeftWrapper.transform.position = slingshotLeft.position + 0.5f * (transform.position - slingshotLeft.position);
		slingRightWrapper.transform.position = slingshotRight.position + 0.5f * (transform.position - slingshotRight.position);

		// Then they need to look at the sling
		slingLeftWrapper.transform.LookAt(slingshotLeft);
		slingRightWrapper.transform.LookAt(slingshotRight);

		// Last the slings have to get the right length
		Vector3 newScaleLeft = slingLeftWrapper.transform.localScale;
		Vector3 newScaleRight = slingRightWrapper.transform.localScale;
		newScaleLeft.z = Vector3.Distance(slingshotLeft.position, transform.position) / shootScript.transform.localScale.z / 2;
		newScaleRight.z = Vector3.Distance(slingshotRight.position, transform.position) / shootScript.transform.localScale.z / 2;
		slingLeftWrapper.localScale = newScaleLeft;
		slingRightWrapper.localScale = newScaleRight;
	}
}
