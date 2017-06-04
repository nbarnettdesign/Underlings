using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour {
	// count of how many coins collected
	public int count = 0;		

	// text to display on bottom of screen with coin count
	public Text countText;

	// text to display on exit of level with how many total coins collected
	public Text winCount;		

	//The Game Win Screen after making it to the exit without being caught
	public GameObject gameWinUI;

	// The count that is displayed during gameplay
	public GameObject gameUI;	

	// block stopping player being attacked after finishing
	public GameObject blockedOff;	

	// bool to say the player has won
	private bool gameIsWon;		

	
	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	//
	//if the game is won, the player can press enter to start again.
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Update () {
		
		if (gameIsWon){
			if (Input.GetKeyDown (KeyCode.Return)) {
				SceneManager.LoadScene (0);
			}
			
		}
	}
	//--------------------------------------------------------------------------------------
	//	OnTriggerEnter()
	// Trigger detection, Detects when the player passes through a Coin for pickup, disables the coin, and adds one to the coin count.
	// Detects when the player passes through the trigger box finish and ends the game.
	//
	// Param:
	//		Collider other - The collider of any objects that pass into this trigger
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("PickUps")){
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}

		if (other.gameObject.CompareTag ("Finish")){
			
			ShowGameWinUI ();
			SetCountText ();
		}

	}

	//--------------------------------------------------------------------------------------
	//	SetCountText()
	// the normal overlay text shows some text and the amount of coins collected,
	// the win screen also updates the amount of coins collected, but doesnt display it until it is enabled on game end
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void SetCountText(){
		countText.text = "Shinies Value: " + count.ToString ();
		winCount.text = "You Escaped with " + count.ToString() +" Shinies";
	}

	//--------------------------------------------------------------------------------------
	//	ShowGameWinUI()
	// When the game is won, the Game win screen enables, the Normal UI disables, the block stopping the player from dying is enabled,
	// the game is won and can be reset with Enter
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void ShowGameWinUI(){
		gameWinUI.SetActive (true);
		gameIsWon = true;
		gameUI.SetActive (false);
		blockedOff.SetActive (true);

	}
}
