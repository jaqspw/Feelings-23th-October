using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerController))]
public class FootSteps: MonoBehaviour {
	public AudioClip[] grassSounds;
	public AudioClip[] woodSounds;
	public AudioClip[] waterSounds;
	private int audioTypeIndex;
	private CharacterController controller;
	private PlayerController playerController;
	public AudioSource audioSource;

	void Awake() {
		controller = GetComponent<CharacterController>();
		playerController = GetComponent<PlayerController> ();
	}

	// Use this for initialization
	IEnumerator Start () {
		while(true) {
			float vel = controller.velocity.magnitude;
			RaycastHit hit = new RaycastHit();
			string floortag;
			if(controller.isGrounded == true && vel > 0.5f) {
				if(Physics.Raycast(transform.position, Vector3.down,out hit))
				{
					floortag = hit.collider.gameObject.tag;
					if (floortag == "Untagged")
					{
//						audioSource.clip = grassSounds[Random.Range(0,grassSounds.Length)];
						audioSource.clip = grassSounds[playerController.audioTypeIndex];
					}
					else if (floortag == "Wood")
					{
//						audioSource.clip = woodSounds[Random.Range(0,woodSounds.Length)];
						audioSource.clip = woodSounds[playerController.audioTypeIndex];
					}
					else if (floortag == "Water")
					{
//						audioSource.clip = waterSounds[Random.Range(0,waterSounds.Length)];
						audioSource.clip = waterSounds[0];
					}
				}

				float interval;
				if (playerController.audioTypeIndex == 0)
					interval = .75f;
				else
					interval = .35f;
				audioSource.PlayOneShot(audioSource.clip);

				yield return new WaitForSeconds(interval);
			}
			else {
				yield return 0;
			}
		}


	}
}