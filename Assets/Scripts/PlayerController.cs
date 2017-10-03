using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -12;
	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	bool disableControl;

	Animator animator;
	Transform cameraT;
	CharacterController controller;

//	[Header("Sounds")]
//	public AudioClip[] grassSounds;
//	public AudioClip[] woodSounds;
//	public AudioClip[] waterSounds;
	public int audioTypeIndex;
//	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController> ();
	}
		
	// Update is called once per frame
	void Update () {
		if (DialogueManager.Instance.isActive)
			return;

		//Input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);
		if (running) {
			audioTypeIndex = 1;
		} else {
			audioTypeIndex = 0;
		}

		Move (inputDir, running);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}

		//Animator
		float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

	}

	void Move (Vector2 inputDir, bool running){
		
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move (velocity * Time.deltaTime);
		currentSpeed = new Vector2 (controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded) {
			velocityY = 0;
		}

	}

	void Jump (){
		if (controller.isGrounded) {
			float jumpVelocity = Mathf.Sqrt (-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
			animator.SetTrigger ("Jumping");
		}
	}

	float GetModifiedSmoothTime(float SmoothTime){
		if (controller.isGrounded) {
			return SmoothTime;
		}

		if (airControlPercent == 0) {
			return SmoothTime;
		}
		return SmoothTime / airControlPercent;
	}
}
