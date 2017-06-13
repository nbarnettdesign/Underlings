using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

	//default character walking speed
	public float walkSpeed = 7.5f;

	//current speed of character
	public float speed = 7.5f;	

	// Character run speed
	public float runSpeed = 10f;

	public float diagonalRunSpeed;
	public float diagonalWalkSpeed;

	// Character sneak speed
	public float sneakSpeed = 3f;	

	//display of Characters current speed
	public float currentSpeed;	

	//Bool to indicate if character is sneaking
	public bool isSneaking = false;

	//UI of sneaking man
	public GameObject sneaking;

	//UI of running man
	public GameObject running;

	//bool for when game is paused
	public bool paused;


	//--------------------------------------------------------------------------------------
	//	Start()
	// Runs during initialisation
	//Sets the Diagonal Speeds to .7171 of the origional speeds
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Start () {
		diagonalRunSpeed = runSpeed * .7171f;
		diagonalWalkSpeed = walkSpeed * .7171f;
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
	void Update (){
		Sneaking ();
		PlayerMovement ();
		SneakAndRun ();
	
	}

	
		
	//--------------------------------------------------------------------------------------
	//	SneakAndRun()
	// Slows the player to a sneak speed when left shift is pressed 
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
	// Basic WASD to move in the directions at the speed walkSpeed, and adjusting for moving Diagonally
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void PlayerMovement(){
		//Basic WASD to move in the directions at the speed walkSpeed .71717171
		if (Input.GetKey (KeyCode.W)) {
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

		//an attempt to normalise speed
		if (
			(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) && (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)) || 
			(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) && (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)) || 
			(Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) && (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)) ||
			(Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) && (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift))){
			speed = diagonalWalkSpeed ;
		}else if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift)){
			speed = walkSpeed;
		}
		if (
			(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) && (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) ||
			(Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) && (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) ||
			(Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) && (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) ||
			(Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) && (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift))) {
			speed = diagonalRunSpeed;
		} else if (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) {
			speed = runSpeed;
		}
			

	}


	//--------------------------------------------------------------------------------------
	//	Sneaking()
	// Makes sure the player either isnt moving, is moving while holding down the shift key(Sneak), and not holding down the spacebar(Run)
	// or if you press run and then sneak so sneaking but also holding down space. And when sneaking or running, showing the appropriate icon
	// used for the purpose of enemy sound detection
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void Sneaking(){
		//if the player is NOT moving or is sneaking, then the is sneaking bool is true, which is used in the Enemy sound detection
		if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) ||
			Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.Space) && speed != runSpeed || Input.GetKey (KeyCode.LeftShift) && 
			Input.GetKey (KeyCode.Space) && speed != runSpeed) {
			isSneaking = true;
			sneaking.gameObject.SetActive (true);
		} else {
			isSneaking = false;
			sneaking.gameObject.SetActive (false);
		}
		if (Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)){
			running.gameObject.SetActive (true);
		} else {
			running.gameObject.SetActive (false);
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

		
	



	