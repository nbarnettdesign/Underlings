using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//default character walking speed
	public float walkSpeed = .075f;

	//current speed of character
	public float speed = .075f;	

	// Character run speed
	public float runSpeed = .15f;		

	// Character roll speed
	public float rollSpeed = .2f;		

	// Character sneak speed
	public float sneakSpeed = .04f;	

	//display of Characters current speed
	public float currentSpeed;	

	// cooldown of roll
	public float rollCool = .5f;	

	// count to compare to roll cooldown
	private float rollCount;	

	// bool to tell if character has rolled so they cant roll again until cooldown is up
	private bool didRoll = false;		

	//Bool to indicate if character is sneaking
	public bool isSneaking = false;

	//--------------------------------------------------------------------------------------
	//	Start()
	// Runs during initialisation
	//
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Start () {
		rollCount = Time.deltaTime;
	}

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

		//Run and Roll Function, Made public to edit as played to find good feel, key down run, key up walk
		//on key pressed, if the roll is off cooldown the roll speed is on first, then drops down to run
		//may remove roll as it currently doesnt seem neccisary, but requires futher testing
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (didRoll == false) {
				speed = rollSpeed;
				didRoll = true;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			speed = walkSpeed;
		}

		//Roll Countdown, lets player roll then disables the roll until the cooldown time has passed

		if (didRoll == true) {
			if (rollCount <= rollCool) {
				rollCount += Time.deltaTime; 
			}
			else if (rollCount > rollCool) {
				rollCount = 0;
				didRoll = false;
				speed = runSpeed;
			}
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
		//Basic WASD to move in the directions at the speed walkSpeed
		if (Input.GetKey (KeyCode.W)) {
			transform.position += transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position -= transform.forward * speed;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += (new Vector3 (-1, 0, 0) * speed);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position += (new Vector3 (1, 0, 0) * speed);
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
		rollSpeed = 0f;
		sneakSpeed = 0f;
	}
}
			
		
	



	