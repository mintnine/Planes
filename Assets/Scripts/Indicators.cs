using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicators : MonoBehaviour {
//	private GameObject enemyAI;
	private GameObject player;
	[SerializeField]
	private GameObject enemyIndicator;
	private List <GameObject> indicatorinuse = new List<GameObject>();

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.Find ("Player");
		if (player != null) {
			for (int i = 0; i < SessionManager.Enemies.Count; i++) {
				GameObject enemy1 = SessionManager.Enemies [i];
				player = GameObject.Find ("Player");
				if (player != null) {
					if (enemy1.transform.position.x > player.transform.position.x + 760 ||
					   enemy1.transform.position.x < player.transform.position.x - 760 ||
					   enemy1.transform.position.y > player.transform.position.y + 1320 ||
					   enemy1.transform.position.y < player.transform.position.y - 1320
					   && player != null) {
						if (indicatorinuse.Count < SessionManager.Enemies.Count) {
							indicatorinuse.Add (Instantiate (enemyIndicator));
							OutsideViewEnemy (enemy1, i);
						} else {
							OutsideViewEnemy (enemy1, i);
						}
					} else if (indicatorinuse [i] != null) {
						Destroy (indicatorinuse [i]);
						indicatorinuse.Remove (indicatorinuse [i]);
					}
				}
			}
		}
//		#region Enemy
//		//check if enemy outside port
//		if (enemyAI.transform.position.x > player.transform.position.x + 760 ||
//		    enemyAI.transform.position.x < player.transform.position.x - 760 ||
//		    enemyAI.transform.position.y > player.transform.position.y + 1320 ||
//		    enemyAI.transform.position.y < player.transform.position.y - 1320) {
//			if (indicatorinuse == null) {
//				indicatorinuse = GameObject.Instantiate (enemyIndicator);
//				OutsideViewEnemy ();
//			} else {
//				OutsideViewEnemy ();
//			}
//		} else {
//			Destroy (indicatorinuse);
//		}
//		#endregion
	}

	void OutsideViewEnemy(GameObject enemyAI,int i){
		//right
		if (enemyAI.transform.position.x > player.transform.position.x + 760) {
			float dify = player.transform.position.y - enemyAI.transform.position.y;
			indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x+630, player.transform.position.y-dify,
				player.transform.position.z);
			if (dify < -1100) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x +630 , player.transform.position.y + 1150,
					player.transform.position.z);
			} else if (dify > 1100 ) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x+630, player.transform.position.y - 1150,
					player.transform.position.z);
			}
			Vector3 dir = enemyAI.transform.position - indicatorinuse[i].transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			indicatorinuse[i].transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		}
		//left
		if (enemyAI.transform.position.x < player.transform.position.x - 760) {
			float dify = player.transform.position.y - enemyAI.transform.position.y;
			indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x-630, player.transform.position.y-dify,
				player.transform.position.z);
			if (dify < -1100) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x -630, player.transform.position.y + 1150,
					player.transform.position.z);
			} else if (dify > 1100) {
				indicatorinuse [i].transform.position = new Vector3 (player.transform.position.x -630, player.transform.position.y - 1150,
					player.transform.position.z);
			}
			Vector3 dir = enemyAI.transform.position - indicatorinuse[i].transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			indicatorinuse[i].transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		}
		//enemy below
		if (enemyAI.transform.position.y < player.transform.position.y - 1320) {
			float difx = player.transform.position.x - enemyAI.transform.position.x;
			indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x-difx, player.transform.position.y-1150,
				player.transform.position.z);
			indicatorinuse[i].transform.rotation = Quaternion.identity;
			indicatorinuse[i].transform.eulerAngles = new Vector3(0,0, -180);
			if (difx > 530) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x - 600, player.transform.position.y - 1150,
					player.transform.position.z);
				indicatorinuse[i].transform.rotation = Quaternion.identity;
				indicatorinuse[i].transform.eulerAngles = new Vector3 (0, 0, -225);
			} else if (difx < -530) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x + 600, player.transform.position.y - 1150,
					player.transform.position.z);
				indicatorinuse[i].transform.rotation = Quaternion.identity;
				indicatorinuse[i].transform.eulerAngles = new Vector3 (0, 0, -135);
			}
			Vector3 dir = enemyAI.transform.position - indicatorinuse[i].transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			indicatorinuse[i].transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		}
		//enemy above
		if (enemyAI.transform.position.y > player.transform.position.y + 1320) {
			float difx = player.transform.position.x - enemyAI.transform.position.x;
			indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x-difx, player.transform.position.y+1150,
				player.transform.position.z);
			indicatorinuse[i].transform.rotation = Quaternion.identity;
			if (difx > 550) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x - 600, player.transform.position.y + 1150,
					player.transform.position.z);
				indicatorinuse[i].transform.rotation = Quaternion.identity;
				indicatorinuse[i].transform.eulerAngles = new Vector3 (0, 0, 45);
			} else if (difx < -550) {
				indicatorinuse[i].transform.position = new Vector3 (player.transform.position.x + 600, player.transform.position.y + 1150,
					player.transform.position.z);
				indicatorinuse[i].transform.rotation = Quaternion.identity;
				indicatorinuse[i].transform.eulerAngles = new Vector3 (0, 0, -45);
			}
			Vector3 dir = enemyAI.transform.position - indicatorinuse[i].transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			indicatorinuse[i].transform.rotation = Quaternion.AngleAxis (angle - 90, Vector3.forward);
		}

	}
}
