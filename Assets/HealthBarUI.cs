using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour {
	Vector2 offset;

	private void Start() {
		offset = transform.position - transform.parent.position;
	}

	private void Update() {
		transform.position = (Vector2)transform.parent.position + offset;
		transform.rotation = Quaternion.identity;
	}
}
