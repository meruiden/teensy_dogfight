using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serialHandler : MonoBehaviour {
	public SerialController serialController;
	string lastmsg = "";
	bool connected = false;

	void Start(){
		DontDestroyOnLoad (this.gameObject);
	}
	// Invoked when a line of data is received from the serial device.
	public void recieveMessage (string msg)
	{
		lastmsg = msg;

	}

	// Invoked when a connect/disconnect event occurrs. The parameter 'success'
	// will be 'true' upon connection, and 'false' upon disconnection or
	// failure to connect.
	public void onConnected(bool success)
	{
		if (success) {
			Debug.Log ("Connection established");
			connected = true;
		} else {
			Debug.Log ("Connection attempt failed or disconnection detected");
			connected = false;
		}

	}

	public bool isConnected(){
		return connected;
	}

	public string[] getDevices(){
		return serialController.getDevices ();
	}

	public string getRecievedMessage(){
		return lastmsg;
	}

	public void write(string message){
		serialController.SendSerialMessage (message);
	}

	public void connectTo(string address){
		serialController.connect (address);
	}

	public void sendMsg(string msg){
		serialController.SendSerialMessage (msg);
	}
}
