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

	public float rollCool = 1f;
	private float rollCount;
	private bool didRoll = false;

	// Update is called once per frame
	void Start () {
		rollCount = Time.deltaTime;
	}



	void Update ()
	{
			
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
		if (Input.GetKey (KeyCode.Space)) {
			if (didRoll == false) {
				if (Time.deltaTime - rollCount <= rollCool) {
					rollCount++;
					speed = rollSpeed;
				}
				if (rollCount > rollCool) {
					speed = runSpeed;
					didRoll = false;
				}

				}			
			}
		if (Input.GetKeyUp (KeyCode.Space)) {
			speed = walkSpeed;
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
	



	