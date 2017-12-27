using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class wisataController : MonoBehaviour {
    public int panelIdx;
    public GameObject canvas;
    public GameObject panelPrefab;
    public GameObject wisataPrefab;
    public GameObject[] navBtn = new GameObject[2];
    public string url;

    private List<GameObject> panel = new List<GameObject>();
    private string jsonString;
    private bool isLoaded;

    // Use this for initialization
    void Start () {
        panelIdx = 0;
        jsonString = null;
        isLoaded = false;
        panel.Add(Instantiate(panelPrefab, canvas.transform));
        StartCoroutine(DownloadTest(url));
	}
	
	// Update is called once per frame
	void Update () {
        if (panelIdx == 0)
            navBtn[0].SetActive(false);
        else
            navBtn[0].SetActive(true);
        if(panelIdx == panel.Count -1)
            navBtn[1].SetActive(false);
        else
            navBtn[1].SetActive(true);

        if (jsonString == null)
            return;
        if (!isLoaded)
        {
            panel[0].SetActive(true);
            createWisata();
            Debug.Log(jsonString);
            isLoaded = true;
        }
    }

    //Button Controller

    public void nextButton()
    {
        panelIdx++;
        panel[panelIdx - 1].SetActive(false);
        panel[panelIdx].SetActive(true);
    }

    public void prevButton()
    {
        if (panelIdx != 0)
            panelIdx--;
        panel[panelIdx + 1].SetActive(false);
        panel[panelIdx].SetActive(true);
    }

    // End Button Controller

    private IEnumerator DownloadTest(string url)
    {
        WWW www = new WWW("http://" + url);
        yield return www;
        Debug.Log(url);
        jsonString = www.text;
    }

    //Create list of objek wisata
    void createWisata()
    {
        int curPanel = 0;
        int objPerPanel = 0;
        JsonData jsonVal = JsonMapper.ToObject(jsonString);
        Debug.Log(jsonVal.Count);
        Text[] newJudul;

        for(int i = 0; i< jsonVal.Count; i++) {

            if(objPerPanel >= 3)
            {
                panel.Add(Instantiate(panelPrefab, canvas.transform));
                curPanel++;
                objPerPanel = 0;
            }
            newJudul = wisataPrefab.GetComponentsInChildren<Text>(true);
            newJudul[0].text = jsonVal[i]["nama_objek_wisata"].ToString();
            newJudul[2].text = jsonVal[i]["lokasi_objek_wisata"].ToString();
            Instantiate(wisataPrefab, panel[curPanel].transform);
            //if (curPanel != 0)
            //    panel[curPanel].SetActive(false);
            objPerPanel++;
        }

    }
}
