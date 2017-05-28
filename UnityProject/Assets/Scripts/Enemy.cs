using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public float detectionRange =5;
	public GameObject player = GameObject.FindGameObjectWithTag ("Player");
	private NavMeshAgent navAgent;
	public float patrolSpeed = 3.5f;
	private float chaseSpeed = 10.0f;

	public Light spotlight;
	public float viewDistance;
	private float viewAngle;
	Transform player;


	// Use this for initialization
	void Start () {
		viewAngle = spotlight.spotAngle;

		//player = GameObject.FindGameObjectWithTag ("Player");
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.speed = patrolSpeed;

	}

	// Update is called once per frame
	void Update () {
		CheckDistance ();
			

	}
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
	}


	private void CheckDistance(){
		
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



