using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoofFade : MonoBehaviour {

	public GameObject roof;

	void OnTriggerEnter(Collider other){
		if (other.tag == "PlayerBody") {
			roof.gameObject.SetActive (false);
		}
	
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "PlayerBody") {
			roof.gameObject.SetActive (true);
		}

	}














}
