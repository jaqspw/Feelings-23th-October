using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahController : MonoBehaviour {

	static Animator anim;
	public float speed = 5.0f;
	public float rotationSpeed = 100.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Andar
		float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);

		// Pulo
		if (Input.GetButtonDown ("Jump")) {
			anim.SetTrigger ("isJumping");
		}

		// Verifica se está se movendo
		if (translation != 0) {
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isIdle", false);
		} else {
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isIdle", true);
		}

	}
}
