using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnemyAI : MonoBehaviour {
	//private LayerMask missilelayer = ;
	private Rigidbody2D rgBody;
	[Range(0,3000)]
	[SerializeField]
	private float speed;
	[Range(0,3)]
	[SerializeField]
	private float rspeed;
	private GameObject playerMissile;
	private float myrotationangle;
	[SerializeField]
	private GameObject bulletprefab;
	[Range(0,30)]
	[SerializeField]
	public float accuracy;
	[SerializeField]
	public Transform bulletspawn;
	[Range(0,2000)]
	[SerializeField]
	public float turretrange;
	[SerializeField]
	public float allyRange;
	[Range(1,100)]
	[SerializeField]
	public float turretwidth;
	[SerializeField]
	public GameObject explosionPrefab;
	private SoundManager soundManager;
	// Use this for initialization
	void Start () {
		soundManager = GameObject.Find ("Play Game").GetComponent<SoundManager> ();
		rgBody = GetComponent<Rigidbody2D> ();
		playerMissile = GameObject.Find ("Player");
		StartCoroutine ("FireRate");

	}
	
	// Update is called once per frame
	void Update () {
		rgBody.velocity = rgBody.transform.up * speed;
		FacePlayer ();
		rgBody.velocity = rgBody.transform.up * speed;
	}


	void FacePlayer(){
		playerMissile = GameObject.Find ("Player");
		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 10, transform.up,turretrange,1<<9);
		if (hit.collider == null) {
			if (playerMissile != null) {
				Vector3 dir = playerMissile.transform.position - transform.position;
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				Quaternion q = Quaternion.AngleAxis (angle - 90, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rspeed);
			}
		}

	}

	void Shoot(){
		RaycastHit2D hit = Physics2D.CircleCast (transform.position, turretwidth, transform.up,turretrange,1<<9);
		if (hit.collider != null) {
			GameObject instantiatedbullet = Instantiate (bulletprefab);
			instantiatedbullet.name = "Bullet Shot By:" + gameObject.name;
			instantiatedbullet.transform.position = bulletspawn.position;
			instantiatedbullet.transform.rotation = transform.rotation;
			instantiatedbullet.transform.Rotate (0, 0, Random.Range (-accuracy, accuracy));
			Rigidbody2D bulletbody = instantiatedbullet.GetComponent<Rigidbody2D> ();
			bulletbody.velocity = bulletbody.transform.up * 5000;
		}
	}

//	void AvoidAllyFront(){
//		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 30, transform.up, allyRange, 1 << 8);
//		if (hit.collider != null &&
//			hit.collider.gameObject != gameObject) {
//			transform.Rotate (new Vector3 (0, 0, -rspeed*2));
//		}
//	}
//
//	void AvoidAllyBack(){
//		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 30, -transform.up, allyRange, 1 << 8);
//		if (hit.collider != null &&
//			hit.collider.gameObject != gameObject) {
//			StartCoroutine (HardLeft (180));
//		}
//	}
//
//	void AvoidAllyLeft(){
//		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 30, -transform.right, allyRange, 1 << 8);
//		if (hit.collider != null &&
//			hit.collider.gameObject != gameObject) {
//			transform.Rotate (new Vector3 (0, 0, -rspeed*2));
//			Debug.Log("Performing Evasive Right");
//		}
//	}
//
//	void AvoidAllyRight(){
//		RaycastHit2D hit = Physics2D.CircleCast (transform.position, 30, transform.right, allyRange, 1 << 8);
//		if (hit.collider != null &&
//			hit.collider.gameObject != gameObject) {
//			StartCoroutine (HardLeft (60));
//		}
//	}

	IEnumerator FireRate(){
		for (;;) {
			yield return new WaitForSeconds (0.1f);
			Shoot ();
		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject != gameObject) {
			GameObject expl = Instantiate (explosionPrefab);
			expl.name = "Enemy Explosion of (" + gameObject.name + ")";
			rgBody.velocity = new Vector2 (0, 0);
			collision.gameObject.GetComponent<Rigidbody2D> ().velocity
			= new Vector2 (0, 0);
			expl.transform.position = 
				new Vector3 (transform.position.x,
					transform.position.y, -30);
			expl.transform.localScale = new Vector3 (0.4f, 0.4f, 1);
			soundManager.Explosion (transform);
			SessionManager.Enemies.Remove (gameObject);
			SessionManager.score++;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.name.Contains(gameObject.name) == false) {
			GameObject expl = Instantiate (explosionPrefab);
			expl.name = "Enemy Friendly of (" + gameObject.name + ")";
			rgBody.velocity = new Vector2 (0, 0);
			collision.gameObject.GetComponent<Rigidbody2D> ().velocity
			= new Vector2 (0, 0);
			expl.transform.position = 
				new Vector3 (transform.position.x,
					transform.position.y, -30);
			expl.transform.localScale = new Vector3 (0.4f, 0.4f, 1);
			SessionManager.Enemies.Remove (gameObject);
			SessionManager.score++;
			Destroy (gameObject);
		}
	}


////	IEnumerator HardLeft(int degrees){
////		for (int i = 0; i < degrees; i++) {
////			yield return new WaitForSeconds (0.1666f);
////			transform.Rotate (new Vector3 (0, 0, rspeed));
////		}
//	}
}
