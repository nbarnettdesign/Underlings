using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public Transform target;

	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	// makes the camera stay on the target
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Update () {
		// makes the camera stay on the target
		transform.position = target.position;
	}
}

