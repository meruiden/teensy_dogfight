using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceShip : MonoBehaviour {
	public float flipSpeed = 1;
	public float overFlipSpeed = 2;
	public float flipBackSpeed = 2;
	public float speed = 1;
	private float trueRotation = 0;
	public float rotateSpeed = 1;
	[SerializeField]private float currotationSpeed = 0;
	float yRot = 0;
	float zRot = 0;
	public Vector2 joyStick = new Vector2();
	public float flipangle = 30;

	Vector2 lastJoyStick = new Vector2();
	public GameObject bullet;
	float lastYrot = 0;
	bool canshoot = true;
	public serialHandler handler;

	float zAcc = 0;
	float zVel = 0;

	public int playerId = 1;
	public RectTransform lockCircle;
	public RectTransform lockCircle2;
	public RectTransform lockCircle3;
	public Canvas can;
	public bool mustShoot = false;

	public GameObject otherPlayer;
	public float minLockDistance = 0.5f;
	public float detectAngle = 45.0f;
	public float detectDistance = 5.0f;

	public GameObject rocketPrefab;

	public Text rocketText;

	public int rockets = 0;
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("SerialController")) {
			handler = GameObject.Find ("SerialController").GetComponent<serialHandler> ();
		}

	}


	
	// Update is called once per frame
	void Update () {
		rocketText.text = "Rockets: " + rockets.ToString ();
		if (lockCircle ) {
			
			if (Vector3.Angle(transform.forward, otherPlayer.transform.position - transform.position) < detectAngle && Vector3.Distance(transform.position, otherPlayer.transform.position) <= detectDistance && rockets > 0) {
				lockCircle.gameObject.SetActive (true);
				lockCircle2.gameObject.SetActive(true);
				if (lockCircle2.GetComponent<innerLockCircle> ().isLocked ()) {
					lockCircle3.gameObject.SetActive(true);
				}
				Vector2 ViewportPosition = GetComponent<PlayerManager> ().cam.GetComponent<Camera> ().WorldToScreenPoint (otherPlayer.transform.position);
				float width = GetComponent<PlayerManager> ().cam.GetComponent<Camera> ().pixelWidth / 2.0f;
				if (playerId == 2) {
					width *= 3;
				}

				lockCircle.anchoredPosition = ViewportPosition - new Vector2 (width, GetComponent<PlayerManager> ().cam.GetComponent<Camera> ().pixelHeight / 2.0f);

			} else {
				lockCircle.gameObject.SetActive (false);
				lockCircle2.gameObject.SetActive(false);
				lockCircle3.gameObject.SetActive (false);
			}
		}

		KeyCode shootkey = KeyCode.None;
		if (playerId == 1) {
			shootkey = KeyCode.Space;
		}else if(playerId == 2){
			shootkey = KeyCode.RightShift;
		}
		if (Input.GetKeyDown (shootkey) || mustShoot) {
			shoot ();
			mustShoot = false;
		}

		Vector2 joyStickSpeedToUse = new Vector2 ();
		if (joyStick.x == 0 && joyStick.y == 0) {
			joyStickSpeedToUse = lastJoyStick;
		} else {
			joyStickSpeedToUse = joyStick;
			lastJoyStick = joyStick;
		}
		Quaternion currot = this.transform.rotation;
		if (joyStickSpeedToUse.x != 0 || joyStickSpeedToUse.y != 0) {
			Quaternion targetRotation = Quaternion.LookRotation ((new Vector3 (joyStickSpeedToUse.y, 0, joyStickSpeedToUse.x) + this.transform.position) - this.transform.position);
			currot = Quaternion.Slerp (this.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

			yRot = currot.eulerAngles.y;
		}

		KeyCode rightKey = KeyCode.None;
		if (playerId == 1) {
			rightKey = KeyCode.D;
		}else if(playerId == 2){
			rightKey = KeyCode.RightArrow;
		}

		if (Input.GetKey (rightKey)) {
			yRot += 300 * Time.deltaTime;
		}

		KeyCode leftkey = KeyCode.None;
		if (playerId == 1) {
			leftkey = KeyCode.A;
		}else if(playerId == 2){
			leftkey = KeyCode.LeftArrow;
		}

		if (Input.GetKey (leftkey)) {
			yRot -= 300 * Time.deltaTime;
		}


		KeyCode rocketKey = KeyCode.None;
		if (playerId == 1) {
			rocketKey = KeyCode.LeftAlt;
		}else if(playerId == 2){
			rocketKey = KeyCode.RightAlt;
		}

		if (Input.GetKeyDown (rocketKey)) {
			fire ();
		}

		this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, yRot, zRot);

		currotationSpeed = lastYrot - yRot;
		lastYrot = yRot;


	}

	public void FixedUpdate(){
		if (currotationSpeed < -3) {
			float toAdd;
			if (zRot > 5) {
				toAdd = overFlipSpeed;
			} else {
				toAdd = flipSpeed;
			}
			zAcc -= toAdd;
		} else if (currotationSpeed > 3) {
			float toAdd;
			if (zRot < -5) {
				toAdd = overFlipSpeed;
			} else {
				toAdd = flipSpeed;
			}
			zAcc += toAdd;
		} else {
			if (zRot < -1) {

				zAcc += flipBackSpeed;
			} else if (zRot > 1) {
				zAcc -= flipBackSpeed;
			}
		}

		if (zRot > flipangle) {
			zAcc -= (zRot - flipangle)*0.03f;
		}

		if (zRot < -flipangle) {
			zAcc += (-zRot - -flipangle)*0.03f;
		}

		zVel += zAcc;
		zVel *= 0.85f;
		zRot += zVel;


		float mag = joyStick.magnitude;
		if (mag > 1) {
			mag = 1;
		}
		move (mag);

		KeyCode movekey = KeyCode.None;
		if (playerId == 1) {
			movekey = KeyCode.W;
		}else if(playerId == 2){
			movekey = KeyCode.UpArrow;
		}
		if (Input.GetKey (movekey)) {
			move (1);
		}

		zAcc = 0;
	}

	IEnumerator waitforshoot(){
		yield return new WaitForSeconds (0.1f);
		canshoot = true;
	}

	void shoot(){
		if (!canshoot)
			return;

		Instantiate (bullet, this.transform.position + this.transform.forward * 8, this.transform.rotation);
		canshoot = false;
		StartCoroutine (waitforshoot ());
	}

	public void move(float magnitude){
		Quaternion r = this.transform.rotation;
		r = new Quaternion (0, r.y, 0, r.w);
		Vector3 s = (r * Vector3.forward);

		this.GetComponent<Rigidbody>().AddForce(s*magnitude*speed);
	}

	public void fire(){
		if (lockCircle2.GetComponent<innerLockCircle> ().isLocked ()) {
			if (Vector2.Distance (lockCircle3.anchoredPosition, Vector2.zero) <= minLockDistance) {
				lockCircle3.GetComponent<innerLockCircle> ().lockPos ();
				GameObject r = Instantiate (rocketPrefab, transform.position + transform.forward * 15, Quaternion.identity) as GameObject;
				r.GetComponent<rocket> ().target = otherPlayer.transform;
				lockCircle2.GetComponent<innerLockCircle> ().reset ();
				lockCircle3.GetComponent<innerLockCircle> ().reset ();
				lockCircle2.gameObject.SetActive (false);
				lockCircle3.gameObject.SetActive (false);
				rockets--;

			} else {
				lockCircle2.GetComponent<innerLockCircle> ().reset ();
				lockCircle3.GetComponent<innerLockCircle> ().reset ();
				lockCircle3.gameObject.SetActive (false);
			}
		} else {
			
			if (Vector2.Distance (lockCircle2.anchoredPosition, Vector2.zero) <= minLockDistance) {
				lockCircle2.GetComponent<innerLockCircle> ().lockPos ();
			} else {
				lockCircle2.GetComponent<innerLockCircle> ().reset ();
			}
		}
	}

	public void pickedUpRocket(){
		rockets++;
	}
}
