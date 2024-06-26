using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour {
	[SerializeField, Range(0f, 100f)]
	float maxSpeed = 10f;
	[SerializeField, Range(0f, 100f)]
	float maxAcceleration = 10f;
	[SerializeField]
	Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

	Vector3 velocity, desiredVelocity;
	Rigidbody body;

	void Awake () {
		body = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void FixedUpdate () {
		Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
		desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
		velocity = body.velocity;
		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
		velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
		body.velocity = velocity;
	}
}
