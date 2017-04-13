using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
	private Transform player;
	private float threshold = 4000;
	// Use this for initialization
	void Start () {
		StartCoroutine ("Close");
	}

	// Update is called once per frame
	void Update () {
		if (transform.parent.parent.FindChild ("Player") != null) {
			player = transform.parent.parent.Find ("Player").transform;
		}
		if (player != null) {
			if (transform.position.x < player.position.x - threshold ||
			   transform.position.x > player.position.x + threshold ||
			   transform.position.y > player.position.y + threshold ||
			   transform.position.y < player.position.y - threshold) {
				Destroy (gameObject);
			}
		}
	}

	IEnumerator Close (){
		yield return new WaitForSeconds (5);
	}
		
}
