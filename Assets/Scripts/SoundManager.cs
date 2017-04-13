using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	[SerializeField]
	private AudioClip[] bullethits;
	private AudioSource source;
	[SerializeField]
	private GameObject audioPrefab;
	[SerializeField]
	private AudioClip enginesound;
	[SerializeField]
	private AudioClip explosion;
	// Use this for initialization
	void Start () {

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

	public void Explosion(Transform location){
		GameObject snd = Instantiate (audioPrefab);
		snd.transform.position = location.position;
		AudioSource asource = snd.GetComponent<AudioSource> ();
		asource.clip = explosion;
		asource.volume = 0.2f;
		asource.Play();
		Destroy (snd, asource.clip.length + 0.3f);
	}
}
