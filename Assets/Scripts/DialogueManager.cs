using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public static DialogueManager Instance;

	public Text nameText;
	public Text dialogueText;
	public bool isActive;
	public Animator animator;

	private Queue<string> sentences;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			DisplayNextSentece ();
		}
	}


	public void StartDialogue (Dialogue dialogue){
		isActive = true;
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

	public void EndDialogue(){
		animator.SetBool ("IsOpen", false);
		Cursor.visible = false;
		isActive = false;
	}

}
