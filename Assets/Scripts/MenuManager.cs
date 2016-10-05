using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public GameObject startButton;
    public GameObject settingButton;
    public GameObject creditButton;
    public GameObject quitButton;
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject SlideHere;
    public GameObject SlideAway;
    public GameObject SlideAway2;

    public GameObject color;
    public GameObject color2;

    public float slideSpeed;

    private bool startbutClick;
    private bool settingbutClick;
    private bool creditbutClick;
    private bool quitbutClick;
    private bool backbutClick;

    // Use this for initialization
    void Start () {
        playMenu.SetActive(false);
        startbutClick = false;
        settingbutClick = false;
        creditbutClick = false;
        quitbutClick = false;
        backbutClick = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(playMenu.transform.position.x);
        if (startbutClick)
        {
            playMenu.SetActive(true);
            mainMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(mainMenu.GetComponent<RectTransform>().anchoredPosition, SlideAway.GetComponent<RectTransform>().anchoredPosition, 10 * Time.deltaTime);
            playMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(playMenu.GetComponent<RectTransform>().anchoredPosition, SlideHere.GetComponent<RectTransform>().anchoredPosition, 10 * Time.deltaTime);
        }
        if (backbutClick)
        {
            playMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(playMenu.GetComponent<RectTransform>().anchoredPosition, SlideAway2.GetComponent<RectTransform>().anchoredPosition, 10 * Time.deltaTime);
            mainMenu.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(mainMenu.GetComponent<RectTransform>().anchoredPosition, SlideHere.GetComponent<RectTransform>().anchoredPosition, 10 * Time.deltaTime);
        }
    }

    public void startClick()
    {
        Debug.Log("start");
        startbutClick = true;

    }
    public void settingClick()
    {
        Debug.Log("setting");
        settingbutClick = true;

    }
    public void creditClick()
    {
        Debug.Log("credit");
        creditbutClick = true;

    }
    public void quitClick()
    {
        Debug.Log("quit");
        quitbutClick = true;

    }
    public void startMainGame()
    {
        Application.LoadLevel(1);

    }
    public void backClick()
    {
        backbutClick = true;

    }
    public void colorClick()
    {
        color.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
    public void colorClick2()
    {
        color2.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value, 1.0f);

    }
}
