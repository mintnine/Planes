using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {
	private GameObject mainMenu;
	private GameObject gameOver;
	private SessionManager sessionManager;
	private SoundManager soundManager;
	[SerializeField]
	private GameObject gameHUD;
	//
	private List<GameObject> bullethits = new List<GameObject>();
	[SerializeField]
	private GameObject bullethitPrefab;
	// Use this for initialization
	void Start () {
		sessionManager = GameObject.Find ("Play Game").GetComponent<SessionManager> ();
		soundManager = GameObject.Find ("Play Game").GetComponent<SoundManager> ();
		mainMenu = GameObject.Find ("Main Menu");
		mainMenu.GetComponent<Animator> ().Play ("Open Menu");
		gameOver = GameObject.Find ("GameOver");
	}
	
	// Update is called once per frame
	void Update () {
		if (SessionManager.gameStarted) {
			gameHUD.SetActive (true);
			gameHUD.transform.FindChild ("Score").GetComponent<Text> ()
				.text = SessionManager.score.ToString ();
		} else {
			gameHUD.SetActive (false);
		}

	}

	public void StartGame(){
		mainMenu.GetComponent<Animator> ().Play ("Close");
		sessionManager.StartGame ();
	}

	public void GameOver(){
		RefreshBulletHits ();
		gameOver.transform.FindChild ("Score").GetComponent<Text> ().text = SessionManager.score.ToString ();
		SessionManager.score = 0;
		gameOver.SetActive (true);
		gameOver.GetComponent<Animator> ().Play ("Open");
	}
	public void ResetGameOver(){
		gameOver.GetComponent<Animator> ().Play ("Close");
		gameOver.SetActive (false);
		sessionManager.ResetGame ();
	}

	void RefreshBulletHits(){
		for (int i = 0; i < bullethits.Count; i++) {
			Destroy (bullethits [i].gameObject);
		}
		bullethits = new List<GameObject>();
	}
	public void AddBulletHit(){
		soundManager.BulletHit ();
		bullethits.Add (Instantiate (bullethitPrefab));
		bullethits [bullethits.Count-1].transform.SetParent (gameObject.transform.FindChild("Canvas"));
		bullethits[bullethits.Count-1].transform.localPosition = new Vector2 (Random.Range(-690,690),Random.Range(-1200,1200));
		int r = Random.Range (1, 3);
		bullethits [bullethits.Count - 1].transform.localScale = new Vector3 (r, r, 1);
	}



}
