using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (Serialize Fields)
	[SerializeField]
	private GameObject playerCamera;

	[SerializeField]
	private float lookSensitivity = 150.0f;
	#endregion

	#region Variable Declarations
	private Vector2 lookVector = Vector2.zero;
	private Quaternion horizontalRotation = Quaternion.identity;
	#endregion

	#region Private Functions
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		HorizontalRotation();
		VerticalRotation();
	}

	private void HorizontalRotation() {
		Vector3 playerRotation = gameObject.transform.rotation.eulerAngles;
		if(lookVector.x > 0.0f) {
			playerRotation.y += lookSensitivity * Time.deltaTime;
		} else if(lookVector.x < 0.0f) {
			playerRotation.y -= lookSensitivity * Time.deltaTime;
		}

		//This actually moves the player characters camera and movement.
		horizontalRotation = Quaternion.Euler(0.0f, playerRotation.y, 0.0f);
		transform.localRotation = horizontalRotation;
	}

	private void VerticalRotation() {
		Vector3 playerRotation = playerCamera.transform.rotation.eulerAngles;
		if(lookVector.y > 0.0f) {
			playerRotation.x -= lookSensitivity * Time.deltaTime;
		} else if(lookVector.y < 0.0f) {
			playerRotation.x += lookSensitivity * Time.deltaTime;
		}

		//This actually moves the player characters camera and movement.
		playerCamera.transform.localRotation = Quaternion.Euler(playerRotation.x, 0.0f, 0.0f);
	}

	private void OnLook(InputValue value) {
		Debug.Log("Looking!!");
		lookVector = value.Get<Vector2>();
	}
	#endregion

	#region Public Access Functions (Getters and Setters)
	/// <summary>
	/// Returns the horizontal rotation of the object.
	/// </summary>
	/// <returns></returns>
	public Quaternion GetHorizontalRotation() {
		return horizontalRotation;
	}
	#endregion
}
