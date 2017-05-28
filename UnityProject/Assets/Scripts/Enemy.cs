﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	public float detectionRange =7; 		//the range at which the enemies will hear the player in a circle around them
	public GameObject player;
	private NavMeshAgent navAgent;
	public float patrolSpeed = 3.5f; 		// how fast the enemies walk while patrolling
	private float chaseSpeed = 10.0f; 		// how fast the enemies chase the player once they spot them

	public Light spotlight;
	public float viewDistance;
	public LayerMask viewMask;
	private float viewAngle;
	private Color originalSpotlightColor;
	private Transform theif;
	public GameObject guard;

	public GameObject gameLoseUI;
	public GameObject gameWinUI;
	public GameObject alarmUI;
	private bool gameIsOver;


	// Use this for initialization
	void Start () {
		viewAngle = spotlight.spotAngle;
		player = GameObject.FindGameObjectWithTag ("Player");
		theif = GameObject.FindGameObjectWithTag ("Player").transform;
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.speed = patrolSpeed;
		originalSpotlightColor = spotlight.color;



	}

	// Update is called once per frame
	void Update () {
		CheckDistance ();
		CheckVisual ();
		Captured ();
		if (gameIsOver) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				SceneManager.LoadScene (0);
			}
		}
	}
		
	//Added pickups, houses to go into, vision cones, and end game conditions
	private void Captured (){

		if (Vector3.Distance (transform.position, player.transform.position) < 1f) {
			
			guard.GetComponent<PlayerController> ().enabled = false;
			ShowGameLoseUI ();


		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
	}


	private void CheckVisual(){
		if(CanSeePlayer ()){
			spotlight.color = Color.red;
			navAgent.destination = player.transform.position;
			navAgent.speed = chaseSpeed;
			ShowAlarmUI ();

			foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>()) {
				enemy.detectionRange = 75;
			}
		}	else {
			spotlight.color = originalSpotlightColor;			
		}
		
	}
	private void CheckDistance(){
		
		if (!player.GetComponent<PlayerController>().isSneaking) { //If player is not sneaking


			if (Vector3.Distance (transform.position, player.transform.position) < detectionRange) {		//if player is within the sound detection range of an enemy
				navAgent.destination = player.transform.position;
				navAgent.speed = chaseSpeed;
				ShowAlarmUI ();

							foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>()) {
								enemy.detectionRange = 75;
							}
			}
		}
	}
	private bool CanSeePlayer(){
		if (Vector3.Distance(transform.position,theif.position)< viewDistance) {
			Vector3 dirToPlayer = (theif.position - transform.position).normalized;
			float angleBetweenEnemyAndPlayer = Vector3.Angle (transform.forward, dirToPlayer);
			if (angleBetweenEnemyAndPlayer < viewAngle/2f){
				if (!Physics.Linecast(transform.position, theif.position, viewMask)){
					return true;
				}
			}
		}
		return false;
	}
	private void ShowGameLoseUI(){
		gameLoseUI.SetActive (true);
		gameIsOver = true;
		
	}
	private void ShowGameWinUI(){
		gameWinUI.SetActive (true);
		gameIsOver = true;

	}
	private void ShowAlarmUI(){
		alarmUI.SetActive (true);



	}
}



