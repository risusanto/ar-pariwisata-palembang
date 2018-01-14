using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bestViewController : MonoBehaviour {

    public GameObject[] panel;
    public GameObject[] btn;

    static int currIdx;
    static int prevIdx;
	// Use this for initialization
	void Start () {
        currIdx = 0;
        panel[currIdx].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(currIdx == 4)
        {
            btn[1].SetActive(false);
        } else
        {
            btn[1].SetActive(true);
        }
        if(currIdx == 0)
        {
            btn[0].SetActive(false);
        } else
        {
            btn[0].SetActive(true);
        }
	}

    public void nextBtn()
    {
        prevIdx = currIdx;
        currIdx += 1;
        panel[currIdx].SetActive(true);
        panel[prevIdx].SetActive(false);
    }

    public void prevBtn()
    {
        prevIdx = currIdx;
        currIdx -= 1;
        panel[currIdx].SetActive(true);
        panel[prevIdx].SetActive(false);
    }
}
