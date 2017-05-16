using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {


	public float walkSpeed = 1.2f;

	// Update is called once per frame
	void Update () {
		//Basic wasd to move in the directions at the speed walkSpeed
		if(Input.GetKey(KeyCode.W)){
			transform.position += transform.forward * walkSpeed;
		}
		if(Input.GetKey(KeyCode.S)){
			transform.position -= transform.forward * walkSpeed;
		}
		if(Input.GetKey(KeyCode.A)){
			transform.position += (new Vector3 (-1, 0, 0) * walkSpeed);
		}
		if(Input.GetKey(KeyCode.D)){
			transform.position += (new Vector3 (1, 0, 0) * walkSpeed);
		}
		//Sneak Function, need to revise to clean up
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			walkSpeed = (walkSpeed * .25f);
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			walkSpeed = (walkSpeed * 4f);
		}
	}
}
