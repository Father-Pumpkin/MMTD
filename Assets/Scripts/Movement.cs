using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Movement : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;

	
	void Start()
	{
		enemy = GetComponent<Enemy>();
		target = Waypoints.points [0];
	}
	// Update is called once per frame
	

		void Update()
	{
		// How to get a vector going from one
		// position to another.

		Vector3 dir = target.position - transform.position;
		//Translate is the movement command
		// We normalize because he said to
		// Multiply by speed because we want to move at that speed(obviously)
		// Time.deltaTime in order to notmalize for framerate
		transform.Translate (dir.normalized * enemy.speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= 0.5f) {
			GetNextWaypoint ();
		}
		enemy.speed = enemy.startSpeed;
}


	void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.points.Length - 1) {
			EndPath ();
			return;
		}
		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}
	
	void EndPath(){
		PlayerStats.Lives--;
        PlayerStats.numEnemiesAlive--;
        Destroy (gameObject);
	}

}
