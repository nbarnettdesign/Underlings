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

	// Character roll speed
	//public float rollSpeed = 12f;		

	// Character sneak speed
	public float sneakSpeed = 3f;	

	//display of Characters current speed
	public float currentSpeed;	

<<<<<<< .mine
	//Bool to indicate if character is sneaking
	public bool isSneaking = false;

	//UI of sneaking man
	public GameObject sneaking;

	//UI of running man
	public GameObject running;

||||||| .r30
	// cooldown of roll
	//public float rollCool = .5f;	

	// count to compare to roll cooldown
	//private float rollCount;	

	// bool to tell if character has rolled so they cant roll again until cooldown is up
	//private bool didRoll = false;		

=======
>>>>>>> .r39
	//bool for when game is paused
	public bool paused;

<<<<<<< .mine
	//Pause screen
	public GameObject pauseUI;
	public Button resume;
	public Button restart;
	public Button mainMenu;
||||||| .r30
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
		//rollCount = Time.deltaTime;
	}
=======
>>>>>>> .r39

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
	
//		if (paused == false){
//			if (Input.GetKeyDown (KeyCode.Escape)) {
//				paused = true;
//				pauseUI.gameObject.SetActive (true);
//			}
//		}
//		if (paused == true ){
//			Time.timeScale = 0;
//			if (Input.GetKeyDown (KeyCode.Escape)) {
//				Time.timeScale = 1;
//				paused = false;
//				pauseUI.gameObject.SetActive (false);
//			}
//		}
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

<<<<<<< .mine
		//Run Function, Made public to edit as played to find good feel, key down run, key up walk

||||||| .r30
		//Run and Roll Function, Made public to edit as played to find good feel, key down run, key up walk
		//on key pressed, if the roll is off cooldown the roll speed is on first, then drops down to run
		//may remove roll as it currently doesnt seem neccisary, but requires futher testing
=======
		//Run Function, Made public to edit as played to find good feel, key down run, key up walk
>>>>>>> .r39
		if (Input.GetKeyDown (KeyCode.Space)) {
			speed = runSpeed;
<<<<<<< .mine

||||||| .r30
//			if (didRoll == false) {
//				speed = rollSpeed;
//				didRoll = true;
//			}
=======
>>>>>>> .r39
		}
		if (Input.GetKeyUp (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) {
				speed = walkSpeed;
		}else if (Input.GetKeyUp (KeyCode.Space) && Input.GetKey (KeyCode.LeftShift)){
			speed = sneakSpeed;
		}
<<<<<<< .mine

||||||| .r30
//
//		//Roll Countdown, lets player roll then disables the roll until the cooldown time has passed
//
//		if (didRoll == true) {
//			if (rollCount <= rollCool) {
//				rollCount += Time.deltaTime; 
//			}
//			else if (rollCount > rollCool) {
//				rollCount = 0;
//				didRoll = false;
//				speed = runSpeed;
//			}
//		}
=======
>>>>>>> .r39
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
			transform.position += transform.forward * speed * Time.deltaTime;
			transform.position += (new Vector3 (-1, 0, 0) * speed) * Time.deltaTime;
		}
		if ((Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.D))){
			speed = speed ;
		}
		if ((Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.A))){
			speed = speed;
		}
		if ((Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.D))){
			speed = speed;
		}
	}


	//--------------------------------------------------------------------------------------
	//	Sneaking()
	// Makes sure the player either isnt moving, is moving while holding down the shift key(Sneak), and not holding down the spacebar(Run)
	// or if you press run and then sneak so sneaking but also holding down space
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

		
	



	