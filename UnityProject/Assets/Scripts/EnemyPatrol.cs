using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {

	//The Start node of the enemies patrol
	public Transform startNode;

	//The Middle node of the enemies patrol
	public Transform middleNode;

	//The End node of the enemies patrol
	public Transform endNode;

	//Nav mesh on the player
	private NavMeshAgent NMA;

	//--------------------------------------------------------------------------------------
	//	Start()
	// Runs during initialisation
	//
	//
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Start () {
		//gets the nav mesh and tells the enemy to move towards its start position
		NMA = GetComponent<NavMeshAgent> ();
		NMA.destination = startNode.position;
	}
	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	//
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
		void Update () {
		PatrolRoute ();
			
		}
	
	//--------------------------------------------------------------------------------------
	//	PatrolRoute()
	// Patrols from Start node, to middle node, to end node, back to start node.
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------	
	private void PatrolRoute(){

		// If the player is within 2 units of the Start node, its new destination is the middle node,
		// If withing 2 units of the middle node, the new destination is the end node
		// If withing 2 units of the end node, the new destination is the start node
		if (Vector3.Distance (transform.position, startNode.position) < 2) {
			NMA.destination = middleNode.position;
		}
		if (Vector3.Distance (transform.position, middleNode.position) < 2) {
			NMA.destination = endNode.position;
		}
		if (Vector3.Distance (transform.position, endNode.position) < 2) {
			NMA.destination = startNode.position;
		}
		
	}
}
