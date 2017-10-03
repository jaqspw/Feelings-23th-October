using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

	[SerializeField] DialogueTrigger dialogueTrigger;
	public GameObject pressPanel;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F) && !DialogueManager.Instance.isActive)
		{
			ShowDialogue ();
			pressPanel.SetActive (false);
		}
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player") && !pressPanel.activeInHierarchy) {
			pressPanel.SetActive (true);

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			pressPanel.SetActive (false);
			if (DialogueManager.Instance.isActive) 
			{
				DialogueManager.Instance.EndDialogue ();
			}
		}
	}

	private void ShowDialogue()
	{
		dialogueTrigger.TriggerDialogue ();
	}
}
