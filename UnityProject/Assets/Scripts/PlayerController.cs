using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {


	public float walkSpeed = .15f;
	public float speed = .15f;
	public float timerBetweenRolls = 1f;
	public float runSpeed = .25f;
	public float rollSpeed = .5f;
	public float sneakSpeed = .075f;
	private float timer;

	// Update is called once per frame
	void Start () {
		timer = Time.time;
	}



	void Update () {
			
		//Basic wasd to move in the directions at the speed walkSpeed
		if(Input.GetKey(KeyCode.W)){
			transform.position += transform.forward * speed;
		}
		if(Input.GetKey(KeyCode.S)){
			transform.position -= transform.forward * speed;
		}
		if(Input.GetKey(KeyCode.A)){
			transform.position += (new Vector3 (-1, 0, 0) * speed);
		}
		if(Input.GetKey(KeyCode.D)){
			transform.position += (new Vector3 (1, 0, 0) * speed);
		}
		//Sneak Function, need to revise to clean up
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			speed = sneakSpeed;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = walkSpeed;
		}
		//Run Function
//		if(Input.GetKeyDown(KeyCode.Space)){
//			walkSpeed = runSpeed ;
//		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			speed = walkSpeed;
		}
		if (Input.GetKeyDown (KeyCode.Space)){
			if (Time.time - timer > timerBetweenRolls) {
				timer = Time.time;
				speed = (rollSpeed);
				yield return new WaitForSeconds (timerBetweenRolls);
				speed = runSpeed;
			}
		}
	}
}


	