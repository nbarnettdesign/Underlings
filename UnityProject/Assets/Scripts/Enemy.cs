using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
	//the range at which the enemies will hear the player in a circle around them
	public float detectionRange =7; 		

	// defining the player
	public GameObject player;				

	//the navAgent attached to the Enemies
	private NavMeshAgent navAgent;	

	// how fast the enemies walk while patrolling
	public float patrolSpeed = 3.5f; 

	// how fast the enemies chase the player once they spot them
	private float chaseSpeed = 10.0f; 		

	// the light that is the vision of the enemies
	public Light spotlight;	

	// how far the enemies can see
	public float viewDistance;	

	//the layer that an object is on
	public LayerMask viewMask;	

	// the angle at which the enemies can see the player from looking directly forward
	private float viewAngle;  	

	// the colour of the vision cone when the enemies start
	private Color originalSpotlightColor;

	// the player as used by the vision cone
	private Transform theif;		

	// the UI of when the player is captured
	public GameObject gameLoseUI;	

	// the UI of when the player has been spotted
	public GameObject alarmUI;	

	// Bool indicating the game is over and can be restarted
	private bool gameIsOver;		

	//--------------------------------------------------------------------------------------
	//	Start()
	// Runs during initialisation
	// Sets the view of the Enemies to the angle of their vision spotlight, their patrol speed, and the origional spotlight color.
	//
	//
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Start () {
		viewAngle = spotlight.spotAngle;			// the Enemies view angle is equal to that of its spotlight
	
		theif = GameObject.FindGameObjectWithTag ("Player").transform;		
		navAgent = GetComponent<NavMeshAgent> ();				
		navAgent.speed = patrolSpeed;					// the enemies on the navmesh have a set speed of patrolling
		originalSpotlightColor = spotlight.color;		// the color the spotlights currently are become the default



	}

	//--------------------------------------------------------------------------------------
	//	Update()
	// Runs every frame
	// Lets you reset game if you get caught.
	// Param:
	//		None
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void Update () {
		CheckDistance ();
		CheckVisual ();
		Captured ();
		//if the player is captured, the enter key will resart the level
		if (gameIsOver) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				SceneManager.LoadScene (0);
			}
		}
	}
		
	//--------------------------------------------------------------------------------------
	//	Captured()
	//If the player and the enemy get close enough that they are basically touching the game ends and the player can not move
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void Captured (){
		// if the enemy gets close enough to the player as though they were basically touching, the game over screen shows, and the player controller is disabled
		if (Vector3.Distance (transform.position, player.transform.position) < 2f) {
			
			player.GetComponent<PlayerController> ().enabled = false;
			ShowGameLoseUI ();


		}
	}

	//--------------------------------------------------------------------------------------
	//	OnDrawGizmos()
	// Shows Enemy view distance in Scene view for adjustment
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	void OnDrawGizmos() {
		//used to show the distance the enemies can see with a red line to make it easier to get distances feeling right
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
	}


	//--------------------------------------------------------------------------------------
	//	CheckVisual()
	// If the player is seen by an enemy then its spotlight turns red while it can see you, its speed is increaded to chase speed, and the enemy will chase the player
	// all other guards are alerted to the player.
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void CheckVisual(){
		if(CanSeePlayer ()){
			spotlight.color = Color.red;		
			navAgent.destination = player.transform.position;		
			navAgent.speed = chaseSpeed;		
			ShowAlarmUI ();						
			//all objects in the scene with the tag Enemy have their detection range increased and will spot and chase after the player
			foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>()) {
				enemy.detectionRange = 75;
			}
		}	
		//when out of sight of the enemy, their vision cone returns to the origional colour
		else {
			spotlight.color = originalSpotlightColor;			
		}
		
	}

	//--------------------------------------------------------------------------------------
	//	CheckDistance()
	// if player is within the sound detection range of an enemy, the enemy will go to the players location, the enemies speed will increase to its chase speed
	// The Alarm UI shows, the enemies are alerted to the player.
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void CheckDistance(){
		//if player is not sneaking
		if (!player.GetComponent<PlayerController>().isSneaking) { 


			if (Vector3.Distance (transform.position, player.transform.position) < detectionRange) {		
				navAgent.destination = player.transform.position;		
				navAgent.speed = chaseSpeed;			
				ShowAlarmUI ();
				//all objects in the scene with the tag Enemy have their detection range increased and will spot and chase after the player
							foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>()) {
								enemy.detectionRange = 75;
							}
			}
		}
	}

	//--------------------------------------------------------------------------------------
	//	CanSeePlayer()
	// Basic WASD to move in the directions at the speed walkSpeed
	//If the player is within the view distance of the enemy
	// the enemy will look at the angle the player is.
	// if the ange is within its detection angle in from of it, it will then check to see if there is
	//any obstacle between it and the player, if so, it ignores them, if not, the player has been seen.
	// Param:
	//		none
	// Return:
	//		Bool of if the player is seen or not.
	//--------------------------------------------------------------------------------------
	private bool CanSeePlayer(){
		if (Vector3.Distance(transform.position,theif.position)< viewDistance) {
			Vector3 dirToPlayer = (theif.position - transform.position).normalized;
			float enemyPlayerAngle = Vector3.Angle (transform.forward, dirToPlayer);

			if (enemyPlayerAngle < viewAngle/2f){
				if (!Physics.Linecast(transform.position, theif.position, viewMask)){
					return true;
				}
			}
		}
		return false;
	}

	//--------------------------------------------------------------------------------------
	//	ShowGameLoseUI()
	//show the Game Over overlay, and sets the game over variable to true, which as above lets the player restart.
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void ShowGameLoseUI(){
		gameLoseUI.SetActive (true);
		gameIsOver = true;
	}

	//--------------------------------------------------------------------------------------
	// ShowAlarmUI()
	//shows the Alarm UI overlay
	//
	// Param:
	//		none
	// Return:
	//		Void
	//--------------------------------------------------------------------------------------
	private void ShowAlarmUI(){
		alarmUI.SetActive (true);



	}
}



