using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuItem : MonoBehaviour {
	Menu menu;
	string address;
	public Text addressText;

	public void setData(GameObject menu, string address){
		this.address = address;
		this.menu = menu.GetComponent<Menu>();
		addressText.text = "Port: " + address;
	}
	public void connect(){
		menu.connectTo (address);
	}
}
