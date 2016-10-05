using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public Rigidbody rb;
    public GameObject[] HealthImages;
    public GameObject cam;
    public GameObject player;
    public GameObject directlight;
    public bool pickedupSpeed;
    public bool pickedupSize;
    public float Health;
    public float pickupSizecount;
    public float pickupSpeedcount;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        pickedupSize = false;
        pickedupSpeed = false;
        pickupSizecount = 0;
        pickupSpeedcount = 0;
        Health = 100;
        StartCoroutine(regen());
    }

    // Update is called once per frame
    void Update () {
		if (Health > 100) {
			Health = 100;
		}
		if (Health < 0) {
			Health = 0;
		}
        healthCheck();
		if (pickedupSize) {
			this.transform.localScale = Vector3.Slerp (this.transform.localScale, new Vector3 (0.6f, 0.6f, 0.6f), 10 * Time.deltaTime);
			pickupSizecount += Time.deltaTime;
			if (pickupSizecount > 10) {
				pickupSizecount = 0;
				pickedupSize = false;

			}
		} else {
			this.transform.localScale = Vector3.Slerp (this.transform.localScale, new Vector3 (1, 1, 1), 10 * Time.deltaTime);
		}
        if (pickedupSpeed)
        {
            this.GetComponent<SpaceShip>().speed = 300;
            pickupSpeedcount += Time.deltaTime;
            if (pickupSpeedcount > 10)
            {
                pickupSpeedcount = 0;
                pickedupSpeed = false;
                this.GetComponent<SpaceShip>().speed = 200;
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "powerup1")
        {
            Destroy(col.gameObject);
            pickedupSpeed = true;
			pickupSpeedcount = 0;
            //this.GetComponent<SpaceShip>().speed = 200;
        }
        if (col.gameObject.tag == "powerup2")
        {
            Destroy(col.gameObject);
            Health += 10;
        }
        if (col.gameObject.tag == "powerup3")
        {
            Destroy(col.gameObject);
            pickedupSize = true;
			pickupSizecount = 0; 
        }
        if (col.gameObject.tag == "Table")
        {
            cam.GetComponent<SmoothFollow>().height = 10;

        }

		if (col.gameObject.tag == "rocket") {
			this.GetComponent<SpaceShip> ().pickedUpRocket ();
			Destroy (col.gameObject);
		}
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Table")
        {
            cam.GetComponent<SmoothFollow>().height = 100;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Knop")
        {
            Debug.Log("Knop");
            directlight.SetActive(false);
        }
    }
    void healthCheck()
    {

        if (Health < 90)
        {
            HealthImages[9].SetActive(false);
        }
        if (Health < 80)
        {
            HealthImages[8].SetActive(false);
        }
        if (Health < 70)
        {
            HealthImages[7].SetActive(false);
        }
        if (Health < 60)
        {
            HealthImages[6].SetActive(false);
        }
        if (Health < 50)
        {
            HealthImages[5].SetActive(false);
        
        }
        if (Health < 40)
        {
            HealthImages[4].SetActive(false);
        }
        if (Health < 30)
        {
            HealthImages[3].SetActive(false);
        }
        if (Health < 20)
        {
            HealthImages[2].SetActive(false);
        }
        if (Health < 10)
        {
            HealthImages[1].SetActive(false);
        }
        if (Health == 0)
        {
            HealthImages[0].SetActive(false);
        }


        if (Health > 90)
        {
            HealthImages[9].SetActive(true);
        }
        if (Health > 80)
        {
            HealthImages[8].SetActive(true);
        }
        if (Health > 70)
        {
            HealthImages[7].SetActive(true);
        }
        if (Health > 60)
        {
            HealthImages[6].SetActive(true);
        }
        if (Health > 50)
        {
            HealthImages[5].SetActive(true);
        }
        if (Health > 40)
        {
            HealthImages[4].SetActive(true);
        }
        if (Health > 30)
        {
            HealthImages[3].SetActive(true);
        }
        if (Health > 20)
        {
            HealthImages[2].SetActive(true);
        }
        if (Health > 10)
        {
            HealthImages[1].SetActive(true);
        }
        if (Health > 0)
        {
            HealthImages[0].SetActive(true);
        }
    }

    IEnumerator regen()
    {
        yield return new WaitForSeconds(3);
        if(Health < 50)
        {
            Health += 10;
        }
        StartCoroutine(regen());
    }
}
