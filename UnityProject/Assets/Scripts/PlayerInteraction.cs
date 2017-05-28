using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour {

	public int count = 0;
	public Text countText;
	public Text winCount;
	public GameObject gameWinUI;
	public GameObject gameUI;
	public GameObject blockedOff;
	private bool gameIsWon;

	// Use this for initialization
	void Start () {
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameIsWon){
			if (Input.GetKeyDown (KeyCode.Return)) {
				SceneManager.LoadScene (0);
			}
			
		}
	}
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
	private void SetCountText(){
		countText.text = "Shinies Value: " + count.ToString ();
		winCount.text = "You Escaped with " + count.ToString() +" Shinies";
	}

	private void ShowGameWinUI(){
		gameWinUI.SetActive (true);
		gameIsWon = true;
		gameUI.SetActive (false);
		blockedOff.SetActive (true);

	}
}
