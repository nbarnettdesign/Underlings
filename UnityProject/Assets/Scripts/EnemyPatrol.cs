using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {

		public Transform startNode;
		public Transform middleNode;
		public Transform endNode;
		private NavMeshAgent NMA;
	
		// Update is called once per frame
		void Update () {
	
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
	
		// Use this for initialization
		void Start () {
			NMA = GetComponent<NavMeshAgent> ();
			NMA.destination = startNode.position;
		}
}
