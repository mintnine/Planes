using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCamera : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private GameObject player;
	private GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		explosion = GameObject.Find ("Player Explosion");
		player = GameObject.Find ("Player");
		if (player != null) {
			cam.transform.position = new Vector3 (player.transform.position.x,
				player.transform.position.y, cam.transform.position.z);
		} else if (explosion!=null){
			cam.transform.position = new Vector3 (explosion.transform.position.x,
				explosion.transform.position.y, cam.transform.position.z);
			explosion.name = "redundant";
		}
	}
}
