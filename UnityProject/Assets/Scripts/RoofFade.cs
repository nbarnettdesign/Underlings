using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoofFade : MonoBehaviour {

	//The Roof of the building the trigger is under
	public GameObject roof;

	//--------------------------------------------------------------------------------------
	//	OnTriggerEnter()
	// Trigger detection, Detects when the player goes near the building, and makes the roof disappear
	//
	// Param:
	//		Collider other - The collider of any objects that pass into this trigger
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if (other.tag == "PlayerBody") {
			roof.gameObject.SetActive (false);
		}
	
	}
	//--------------------------------------------------------------------------------------
	//	OnTriggerExit()
	// Trigger detection, Detects when the player leaves the building area, and makes the roof reappear 
	//
	// Param:
	//		Collider other - The collider of any objects that pass into this trigger
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void OnTriggerExit(Collider other){
		if (other.tag == "PlayerBody") {
			roof.gameObject.SetActive (true);
		}

	}














}
