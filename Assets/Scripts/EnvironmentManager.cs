using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {
	[Header("Generation Points")]

	[Header("Cloud Prefabs")]
	[SerializeField]
	private GameObject[] cloudprefabs;
	// Use this for initialization
	void Start () {
		StartCoroutine ("CloudGenerationCall");
	}
	
	// Update is called once per frame
	void Update () {
		CloudOverload ();
	}

	IEnumerator CloudGenerationCall(){
		for (;;) {
			yield return new WaitForSeconds (1);
			SetupGenerate ();
		}
	}

	void Generate(Transform dir){
		GameObject cld = GameObject.Instantiate (
			cloudprefabs [Random.Range (
				0, cloudprefabs.Length - 1)]
		);
		cld.transform.SetParent (gameObject.transform);
		cld.transform.position = new Vector3(dir.position.x+Random.Range(-500,500),
			dir.position.y+Random.Range(-500,500),0);
	}

	void SetupGenerate(){
		
		GameObject playerinst = GameObject.Find ("Player");
		if (playerinst != null &&
			EnoughSpaceAvailable()) {
			PlayerManager playerscr = playerinst.GetComponent<PlayerManager> ();
			Transform upgen = playerscr.upgen;
			Transform downgen = playerscr.downgen;
			Transform leftgen = playerscr.leftgen;
			Transform rightgen = playerscr.rightgen;

			Generate (upgen);
			Generate (rightgen);
			Generate (leftgen);
			Generate (downgen);

		}

	}


	void CloudOverload(){
		int threshold = 2000;
		GameObject playerinst = GameObject.Find ("Player");
		if (gameObject.transform.childCount >= 40) {
			for (int i = 0; i < 20; i++) {
				if (transform.GetChild (i).transform.position
					.x > playerinst.transform.position.x + threshold||
					transform.GetChild (i).transform.position
					.x < playerinst.transform.position.x - threshold||
					transform.GetChild (i).transform.position
					.y > playerinst.transform.position.y + threshold||
					transform.GetChild (i).transform.position
					.y < playerinst.transform.position.y - threshold
					) {
					Destroy (transform.GetChild (i).gameObject);
				}
			}
		}
	}


	#region LogicCalls

	bool EnoughSpaceAvailable(){
		int existingobjs = 0;
		foreach (Transform child in transform) {
			existingobjs++;
		}
		if (existingobjs > 20) {
			return false;
		} else {
			return true;
		}
	}

	#endregion
}
