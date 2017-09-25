using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
	}

	public void StartDialogue (Dialogue dialogue){

		animator.SetBool ("IsOpen", true);

		Cursor.visible = true;

		nameText.text = dialogue.name;

		sentences.Clear ();

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue (sentence);
		}

		DisplayNextSentece ();

	}

	public void DisplayNextSentece (){
		if (sentences.Count == 0) {
			EndDialogue ();
			return;
		}

		string sentece = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentece(sentece));

	}

	IEnumerator TypeSentece (string sentence){
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue(){
		animator.SetBool ("IsOpen", false);
		Cursor.visible = false;

	}

}
