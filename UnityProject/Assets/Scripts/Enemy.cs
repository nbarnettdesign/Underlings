using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public float detectionRange =5;
	public GameObject player;
	private NavMeshAgent navAgent;
	private float patrolSpeed = 3.5f;
	private float chaseSpeed = 10.0f;




	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
		navAgent = GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void Update () {
		CheckDistance ();
			

	}

	void CheckDistance(){
		
		if (!player.GetComponent<PlayerController>().isSneaking) { //If player is not sneaking


			if (Vector3.Distance (transform.position, player.transform.position) < detectionRange) {		//if player is within	
				navAgent.destination = player.transform.position;
				navAgent.speed = chaseSpeed;

							foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>()) {
								enemy.detectionRange = 75;
							}
			}
		}
	}
}



