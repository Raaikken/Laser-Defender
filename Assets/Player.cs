using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	// Variables
	[SerializeField] float moveSpeed = 1f;

	// Internal Variables


	// Debug


	// Start is called before the first frame update
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {
		Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		LookAtMouse();
	}

	void Move(float moveX, float moveY) {
		Vector2 movementVector = new Vector2(moveX, moveY);
		transform.position = (Vector2)transform.position + movementVector.normalized * moveSpeed * Time.deltaTime;
	}

	void LookAtMouse() {
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
		float AngleDeg = (180 / Mathf.PI) * AngleRad;
		transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
	}
}
