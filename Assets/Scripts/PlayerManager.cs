using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

	public static int hits;
	[SerializeField]
	private Rigidbody2D rigidBody;
	[SerializeField]
	[Range(0,3000)]
	private int speed;
	[SerializeField]
	[Range(0,10)]
	private float rspeed;
	[SerializeField]
	private GameObject explosionPrefab;
	[SerializeField]
	private GameObject cam;
	private SessionManager sessionManager;
	//
	public Transform upgen;
	public Transform downgen;
	public Transform leftgen;
	public Transform rightgen;

	// Use this for initialization
	void Start () {
		sessionManager = GameObject.Find ("Play Game").GetComponent<SessionManager> ();

		upgen = transform.FindChild("GenPoints").FindChild ("upgen");
		downgen = transform.FindChild("GenPoints").FindChild ("downgen");
		rightgen = transform.FindChild("GenPoints").FindChild ("rightgen");
		leftgen = transform.FindChild("GenPoints").FindChild ("leftgen");
	}
	
	// Update is called once per frame
	void Update () {

		if (hits > 10) {
			Die ();
		}

		rigidBody.velocity = rigidBody.transform.up *speed;
		if (SessionManager.gameStarted) {
			if (Input.GetMouseButton (0)) {
				if (Input.mousePosition.x > 720) {
					Turn (-1);
				}
				if (Input.mousePosition.x < 720) {
					Turn (1);
				}
			}
		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.name.Contains("Enemy")) {
			Die ();
		}
	}
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.name.Contains("Bullet")) {
			Destroy (collider.gameObject);
			hits++;
			sessionManager.AddBulletHit ();
		}
	}

	void Turn(int dir){
		rigidBody.velocity = rigidBody.transform.up*speed;
		rigidBody.transform.Rotate (0, 0, 0 + rspeed*dir);
		rigidBody.velocity = rigidBody.transform.up*speed;
	}

	void Die(){
		GameObject expl = Instantiate (explosionPrefab);
		rigidBody.velocity = new Vector2 (0, 0);
		expl.transform.position = 
			new Vector3 (transform.position.x,
				transform.position.y, -30);
		expl.name = "Player Explosion";
		sessionManager.EndGame();
		Destroy(gameObject);
	}
		

}
