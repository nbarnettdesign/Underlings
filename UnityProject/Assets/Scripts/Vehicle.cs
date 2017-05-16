using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Vehicle : MonoBehaviour {
	public XboxController controller;

	// Use this for initialization
	public List<WheelCollider> wheelList;

	public float enginePower=150.0f;
	public float steer=0.0f;
	public float maxSteer = 25.0f;

	public int currentCheckPoint = 0;

	public Vector3 centerOfMass = new Vector3 (0, -0.5f, 0.3f);

	public void HitCheckPoint(int CheckpointNumber){
		if (CheckpointNumber == currentCheckPoint + 1) {
			currentCheckPoint = CheckpointNumber;
		} else {
			Debug.Log ("Wrong Checkpoint for " + transform.name);
		}
	}


	void Start (){
		GetComponent<Rigidbody> ().centerOfMass = centerOfMass;
	}
	
	// Update is called once per frame
	void Update () {
		//first section of for loop, declaring counter variable   	int i = 0
		//second, when do wse keep looping (in this case while the counter is less than the wheelList count,		i < wheelList.Count 
		//third, the counter will go up by one		i++
		for (int i = 0; i < wheelList.Count; i++) {
			if (XCI.GetAxis (XboxAxis.RightTrigger, controller) != 0) { 
				wheelList [i].motorTorque = enginePower * Time.deltaTime * 250.0f * XCI.GetAxis (XboxAxis.RightTrigger, controller);
			} else if (XCI.GetAxis (XboxAxis.LeftTrigger, controller) != 0) { 
				wheelList [i].motorTorque = enginePower * Time.deltaTime * -250.0f * XCI.GetAxis (XboxAxis.LeftTrigger, controller);
			}
		}

		wheelList [0].steerAngle = XCI.GetAxis (XboxAxis.LeftStickX,controller) * maxSteer;			
		wheelList [1].steerAngle = XCI.GetAxis (XboxAxis.LeftStickX,controller) * maxSteer;
		}
}

