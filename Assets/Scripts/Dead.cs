using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour {


	private Scene scene;

	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene ();
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Application.LoadLevel (scene.name);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
