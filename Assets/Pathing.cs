using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour {
	// Variables
	[SerializeField] float normalMoveSpeed = 5f;
	[SerializeField] float alertedSpeedMultiplier = 2f;
	[SerializeField] List<Transform> patrolRoute = new List<Transform>();

	// Internal Variables
	[SerializeField] int nextWaypoint = 0;
	[SerializeField] bool isAlerted = false;
	[SerializeField] float currentMoveSpeed;


	// Debug


	// Start is called before the first frame update
	void Start() {
		currentMoveSpeed = normalMoveSpeed;
		GameObject path = GameObject.FindWithTag("Path");
		for(int i = 0; i < path.transform.childCount; i++) {
			patrolRoute.Add(path.transform.GetChild(i));
		}
	}

	// Update is called once per frame
	void Update() {
		Move(patrolRoute[nextWaypoint]);
		LookAt(patrolRoute[nextWaypoint].position);
	}

	void Move(Transform target) {
		currentMoveSpeed = isAlerted ? normalMoveSpeed * alertedSpeedMultiplier : normalMoveSpeed;
		transform.position = Vector2.MoveTowards(transform.position, target.position, currentMoveSpeed * Time.deltaTime);
		if(transform.position == target.position) {
			nextWaypoint++;
		}

		if(nextWaypoint >= patrolRoute.Count) {
			nextWaypoint = 0;
		}
	}

	void LookAt(Vector2 target) {
		float AngleRad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
	}

}
