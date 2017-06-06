using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//default character walking speed
	public float walkSpeed = 7.5f;

	//current speed of character
	public float speed = 7.5f;	

	// Character run speed
	public float runSpeed = 10f;		

	// Character roll speed
	//public float rollSpeed = 12f;		

	// Character sneak speed
	public float sneakSpeed = 3f;	

	//display of Characters current speed
	public float currentSpeed;	

	//Bool to indicate if character is sneaking
	public bool isSneaking = false;


	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	//
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Update ()
	{
		Sneaking ();
		PlayerMovement ();
		SneakAndRun ();



	}
	//--------------------------------------------------------------------------------------
	//	SneakAndRun()
	// Slows the player to a sneak speed when left shift is pressed, and a roll for half a second then run speed when spacebar is pressed
	// and a timer to stop roll being spammed for massive increased speed 
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void SneakAndRun(){
		//Sneak Function, made public to edit as played to find good feel, Key down sneak, key up walk
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			speed = sneakSpeed;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = walkSpeed;
		}

		//Run Function, Made public to edit as played to find good feel, key down run, key up walk
		if (Input.GetKeyDown (KeyCode.Space)) {
			speed = runSpeed;
		}
		if (Input.GetKeyUp (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) {
				speed = walkSpeed;
		}else if (Input.GetKeyUp (KeyCode.Space) && Input.GetKey (KeyCode.LeftShift)){
			speed = sneakSpeed;
		}
	}

	//--------------------------------------------------------------------------------------
	//	PlayerMovement()
	// Basic WASD to move in the directions at the speed walkSpeed
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void PlayerMovement(){
		//Basic WASD to move in the directions at the speed walkSpeed .71717171
		if ((Input.GetKey (KeyCode.W)) && !(Input.GetKey (KeyCode.A))) {
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position -= transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += (new Vector3 (-1, 0, 0) * speed) * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position += (new Vector3 (1, 0, 0) * speed) * Time.deltaTime;
		}
		if ((Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.A))){
			transform.position += transform.forward * speed * Time.deltaTime * 0.7171f;
			transform.position += (new Vector3 (-1, 0, 0) * speed) * Time.deltaTime * 0.7171f;
		}
		if ((Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.D))){
			speed = speed * 0.7171f;
		}
		if ((Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.A))){
			speed = speed * 0.7171f;
		}
		if ((Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.D))){
			speed = speed * 0.7171f;
		}
	}


	//--------------------------------------------------------------------------------------
	//	Sneaking()
	// Makes sure the player either isnt moving, is moving while holding down the shift key(Sneak), and not holding down the spacebar(Run)
	// used for the purpose of enemy sound detection
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void Sneaking(){
		//if the player is NOT moving or is sneaking, then the is sneaking bool is true, which is used in the Enemy sound detection
		if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) ||
			Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.Space) && speed != runSpeed) {
			isSneaking = true;
		} else {
			isSneaking = false;
		}
	}
	//--------------------------------------------------------------------------------------
	//	Caught()
	// When the player is caught, they can no longer move
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	// if the player gets caught, his speeds are reduced to zero and he cant move
	private void Caught(){
		speed = 0f;
		walkSpeed = 0f;
		runSpeed = 0f;
		sneakSpeed = 0f;
	}
}
			
		
	



	