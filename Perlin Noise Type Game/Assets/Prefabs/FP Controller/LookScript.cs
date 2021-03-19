using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (Serialize Fields)
	[SerializeField]
	private GameObject playerCamera = null;

	[SerializeField]
	private float lookSensitivity = 20.0f;
	#endregion

	#region Variable Declarations

	#endregion

	#region Private Functions
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnLookAround(InputValue lookValue) {
		Debug.Log("Look function called");
		//Get the direction the mouse is moving in and normalize it.
		Vector2 rotation = lookValue.Get<Vector2>();
		rotation.Normalize();

		//Look left or right.
		Vector3 rotationY = transform.rotation.eulerAngles;
		if(rotation.x > 0.0f) {
			rotationY.y += lookSensitivity * Time.deltaTime;
		} else if (rotation.x < 0.0f) {
			rotationY.y -= lookSensitivity * Time.deltaTime;
		}
		transform.rotation = Quaternion.Euler(rotationY);

		//Look up down.
		Vector3 cameraRotation = playerCamera.transform.rotation.eulerAngles;
		if(rotation.y > 0.0f) {
			cameraRotation.x -= lookSensitivity * Time.deltaTime;
		} else if(rotation.y < 0.0f) {
			cameraRotation.x += lookSensitivity * Time.deltaTime;
		}
		cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90.0f, 90.0f);
		playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
	}
	#endregion

	#region Public Access Functions (Getters and setters)

	#endregion
}
