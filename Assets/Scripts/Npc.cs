using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

	//[SerializeField] DialogueTrigger dialogueTrigger;
	DialogueManager dm;
	public GameObject pressPanel;
	public int idDialogue;

	void Start()
	{
		dm = FindObjectOfType<DialogueManager> ();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (Input.GetKeyDown (KeyCode.F) && !dm.isActive)
			{
				print (idDialogue);
				print (this.name);
				ShowDialogue (idDialogue);
				pressPanel.SetActive (false);
			}
		}

	}


	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player") && !pressPanel.activeInHierarchy) {
			print (this.name);
			pressPanel.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			pressPanel.SetActive (false);
			if (dm.isActive) 
			{
				dm.EndDialogue ();
			}
		}
	}

	private void ShowDialogue(int id)
	{
		FindObjectOfType<DialogueTrigger>().TriggerDialogue (this, id);
	}
}
