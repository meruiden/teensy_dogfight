using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class innerLockCircle : MonoBehaviour {
	bool locked = false;
	public float lerpDistance = 10;
	Vector2 startPos; 
	Vector2 endPos;

	Vector2 posToGo;
	public float speed = 5;


	// Use this for initialization
	void OnEnable () {
		reset ();
	}

	void Update(){
		if (!locked) {
			if (Vector2.Distance (GetComponent<RectTransform> ().anchoredPosition, posToGo) > 0.1f) {
				GetComponent<RectTransform> ().anchoredPosition = Vector2.MoveTowards (GetComponent<RectTransform> ().anchoredPosition, posToGo, speed * Time.deltaTime);
			} else {
				if (posToGo == startPos) {
					posToGo = endPos;
				} else {
					posToGo = startPos;
				}

			}

		}
	}

	public bool isLocked(){
		return locked;
	}

	public void lockPos(){
		locked = true;
		GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
	}

	public void reset(){
		Quaternion rot = Random.rotation;
		rot = Quaternion.Euler (0, 0, rot.eulerAngles.z);
		startPos = (rot * Vector2.up) * lerpDistance;

		endPos = (rot * -Vector2.up) * lerpDistance;
		GetComponent<RectTransform>().anchoredPosition = startPos;
		posToGo = endPos;
		locked = false;
	}
		

}
