using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookScript : MonoBehaviour {
	#region Variables to assign via the unity inspector (Serialize Fields)
	[SerializeField]
	private GameObject playerCamera;

	[SerializeField]
	private float lookSensitivity = 10.0f;
	#endregion

	#region Variable Declarations
	private Vector2 lookVector = Vector2.zero;
	#endregion

	#region Private Functions
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void HorizontalRotation() {
		Vector3 playerRotation = gameObject.transform.rotation.eulerAngles;
		if(lookVector.x > 0.0f) {
			playerRotation.y += lookSensitivity * Time.deltaTime;
		} else if(lookVector.x < 0.0f) {
			playerRotation.x -= lookSensitivity * Time.deltaTime;
		}
	}

	private void OnLook(InputValue value) {
		Debug.Log("Looking!!");
		lookVector = value.Get<Vector2>();
	}
	#endregion

	#region Public Access Functions (Getters and Setters)

	#endregion
}
