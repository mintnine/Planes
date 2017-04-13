using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour {
	public static bool gameStarted;
	public static List<GameObject>  Enemies = new List<GameObject>();
	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject playerPrefab;
	private GameObject player;
	public static SessionManager thisScript;
	private UIManager uiManager;
	public static int score;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		thisScript = gameObject.GetComponent<SessionManager> ();
		uiManager = GameObject.Find ("UI").GetComponent<UIManager> ();
		StartCoroutine ("GenerateEnemies");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted) {
			if (player == null) {
				EndGame ();
			}
		}
	}

	public void StartGame(){
		gameStarted = true;
		MakeEnemy ();
		MakeEnemy ();	
		MakeEnemy ();

	}
	public static void Reset(){
		if (GameObject.Find ("Player") == null) {
			SceneManager.LoadScene ("Game");
		}
	}

	void MakeEnemy(){
		GameObject instenemy = Instantiate (enemyPrefab);
		Enemies.Add (instenemy);
		int  xi = Random.Range(1000,1500);
		int  yi = Random.Range(1000,1500);
		int xii = Random.Range (1, 100);
		if (xii > 50) {
			instenemy.transform.position = 
				new Vector3 (player.transform.position.x + xi,
					player.transform.position.y + yi,
					instenemy.transform.position.z);
		} else if(xii<50){
			instenemy.transform.position = 
				new Vector3 (player.transform.position.x - xi,
					player.transform.position.y - yi,
					instenemy.transform.position.z);
		}

	}

	public void ResetGame(){
		for (int i = 0; i < Enemies.Count; i++) {
			Destroy (Enemies [i]);
		}
		Enemies = new List<GameObject> ();
		player = Instantiate (playerPrefab);
		player.name = "Player";
		player.transform.SetParent (gameObject.transform);
		StartGame ();
	}

	public void EndGame(){
		if (gameStarted == true) {
			uiManager.GameOver ();
			PlayerManager.hits = 0;
		}
		gameStarted = false;
	}
	

	IEnumerator GenerateEnemies(){
		for (;;) {
			yield return new WaitForSeconds (Random.Range (5, 10));
			if (gameStarted == true) {
				MakeEnemy ();
			}
		}
	}

	public void AddBulletHit(){
		uiManager.AddBulletHit ();
	}
		
		
}
