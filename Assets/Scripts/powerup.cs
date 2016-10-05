using UnityEngine;
using System.Collections;

public class powerup : MonoBehaviour {
    private float destroycounter;
	// Use this for initialization
	void Start () {
        destroycounter = 0;

    }
	
	// Update is called once per frame
	void Update () {
        destroycounter += Time.deltaTime;
        this.transform.eulerAngles += new Vector3(this.transform.eulerAngles.x, 100, this.transform.eulerAngles.z) * Time.deltaTime;

        if (destroycounter > 20)
        {
            Destroy(gameObject);
        }
    }
}
