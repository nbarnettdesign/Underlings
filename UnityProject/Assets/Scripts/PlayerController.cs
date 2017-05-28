using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {


	public float walkSpeed = .15f;
	public float speed = .15f;
	public float runSpeed = .25f;
	public float rollSpeed = .5f;
	public float sneakSpeed = .075f;
	public float currentSpeed;
	public float rollCool = 1f;
	private float rollCount;
	private bool didRoll = false;

	//For sneaking
	public bool isSneaking = false;

	// Update is called once per frame
	void Start () {
		rollCount = Time.deltaTime;
	}



	void Update ()
	{
		//if the player is NOT moving or is sneaking, then the is sneaking bool is true, which is used in the Enemy sound detection
		if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.Space)) {
			isSneaking = true;
		} else {
			isSneaking = false;
		}


		//Basic wasd to move in the directions at the speed walkSpeed
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
		//Sneak Function, made public to edit as played to find good feel
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			speed = sneakSpeed;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = walkSpeed;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (didRoll == false) {
				speed = rollSpeed;
				didRoll = true;
			}
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			speed = walkSpeed;
			}


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
}
			
			
	//Run Function
	//		if(Input.GetKeyDown(KeyCode.Space)){
	//			walkSpeed = runSpeed ;
	//		}

	//		if (Input.GetKeyUp (KeyCode.Space)) {
	//			speed = walkSpeed;
	//		}
	//		if (Input.GetKeyDown (KeyCode.Space)){
	//			if (Time.time - timer > timerBetweenRolls) {
	//				timer = Time.time;
	////				Invoke (RollSpeed, timerBetweenRolls);
	//			}
	////
	//			}
	//		}

	//private string RollSpeed () {
	//	speed = rollSpeed;
	//	}
	



	