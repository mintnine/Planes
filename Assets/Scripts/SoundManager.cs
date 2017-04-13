using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	[SerializeField]
	private AudioClip[] bullethits;
	private AudioSource source;
	[SerializeField]
	private GameObject audioPrefab;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void BulletHit(){
		GameObject snd = Instantiate (audioPrefab);
		AudioSource asource = snd.GetComponent<AudioSource> ();
		asource.clip = bullethits [Random.Range(0,bullethits.Length-1)];
		asource.Play ();
		Destroy (snd, asource.clip.length + 0.3f);
	}
}
