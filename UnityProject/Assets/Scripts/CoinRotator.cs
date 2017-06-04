using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour {


	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	//Makes the coin pickups rotate in a way that is appealing and draws the attention of the player
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Update () {
		transform.Rotate (new Vector3 (45, 45, 45) * Time.deltaTime);
	}
}
