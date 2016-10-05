using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {
	public serialHandler handler;
	public GameObject listItemPrefab;
	public GameObject holder;
	public GameObject panel;
	public string sceneToLoad;
	// Use this for initialization
	void Start () {
		string[] devs = handler.getDevices ();
		foreach (string s in devs) {
			GameObject it = Instantiate (listItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			it.transform.SetParent(holder.transform);
			it.GetComponent<menuItem> ().setData (this.gameObject, s);
		}
	}

	// Update is called once per frame
	void Update () {

		if (handler.isConnected ()) {
			if (panel.activeSelf) {
				panel.SetActive (false);
				SceneManager.LoadScene (sceneToLoad);
			}
	
		}


	}

	public void connectTo(string address){
		handler.connectTo (address);

	}



}
