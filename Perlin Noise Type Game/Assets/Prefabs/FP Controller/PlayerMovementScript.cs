using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (Serialize Fields)
	[SerializeField]
	private LayerMask groundLayer;

	[SerializeField]
	private float movementSpeed = 10.0f;

	[SerializeField]
	private float gravityValue = 9.81f;
	#endregion

	#region Variable Declarations
	private CharacterController playerController;
	private Vector3 playerVelocity = Vector3.zero;
	private Vector2 i_Movement = Vector2.zero;
	private bool onGround = false;
	#endregion

	#region Private Functions
	// Start is called before the first frame update
	void Start() {
		playerController = gameObject.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update() {
		onGround = CheckIfOnGround();
		Move();
	}

	private bool CheckIfOnGround() {
		Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 0.5f, groundLayer);
		if(colliders.Length > 0) {
			return true;
		} else {
			return false;
		}
	}

	private void Move() {
		playerVelocity.y -= gravityValue * Time.deltaTime;
		if (onGround) {
			playerVelocity.y = 0.0f;
		}

		playerVelocity.x = i_Movement.x;
		playerVelocity.z = i_Movement.y;
		playerVelocity = gameObject.GetComponent<LookScript>().GetHorizontalRotation() * playerVelocity;
		playerController.Move(playerVelocity);
	}

	private void OnMove(InputValue value) {
		Debug.Log("Moving!!");
		i_Movement = value.Get<Vector2>() * movementSpeed * Time.deltaTime;
	}
	#endregion

	#region Public Access Functions (Getters and setters)

	#endregion
}
