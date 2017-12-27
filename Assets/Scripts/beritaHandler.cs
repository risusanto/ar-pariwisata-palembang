using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class beritaHandler : MonoBehaviour {

    public GameObject headlinePosition;
    public GameObject headlineBeritaObj;
    public static beritaHandler instance;
    public float yPosition;
    Text[] newJudul;
    public string url = "http://localhost/vuforia/api";
    public string jsonString;

    private delegate void TextDelegate(string text);

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(DownloadTest(url));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void createBerita()
    {
        //Debug.Log(jsonString);
        if(jsonString != null)
        {
            LitJson.JsonData jsonvale = LitJson.JsonMapper.ToObject(jsonString);
            Debug.Log(jsonvale.Count);
            float newPos = yPosition;
            for (int i = 0; i < jsonvale.Count; i++)
            {
                headlineBeritaObj.transform.position = new Vector3(headlineBeritaObj.transform.position.x,
                    newPos - (i * 100), headlineBeritaObj.transform.position.z);
                newJudul = headlineBeritaObj.GetComponentsInChildren<Text>(true);
                newJudul[0].text = jsonvale[i]["judul_berita"].ToString();
                newJudul[1].text = jsonvale[i]["id_berita"].ToString();
                Instantiate(headlineBeritaObj, headlinePosition.transform);
                headlineBeritaObj.transform.position = new Vector3(headlineBeritaObj.transform.position.x,
                    yPosition, headlineBeritaObj.transform.position.z);
            }
        }

    }

    private IEnumerator DownloadTest(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        jsonString = www.text;
    }

    private void PrintText(string text)
    {
        Debug.Log(text);
    }
}
