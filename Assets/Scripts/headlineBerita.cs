using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

public class headlineBerita : MonoBehaviour {

    Text[] id_berita;
    bool isLoaded;

    string id;
    public string url;
    public string jsonString;
    public Image img;
    // Use this for initialization

    void Start () {
        jsonString = null;
        isLoaded = false;
        id_berita = gameObject.GetComponentsInChildren<Text>(true);
        id = id_berita[1].text.ToString();
        url = "52.187.131.133/api/getberita/" + id;
        StartCoroutine(DownloadTest(url));
    }
	
	// Update is called once per frame
	void Update () {
        if (jsonString == null)
            return;
        else
        {
            if(!isLoaded)
                isLoaded = true;
        }
	}

    private IEnumerator DownloadTest(string url)
    {
        WWW www = new WWW("http://" + url);
        yield return www;
        jsonString = www.text;
    }

    public void showBerita()
    {
        if(isLoaded == true)
        {
            JsonData jsonvale = JsonMapper.ToObject(jsonString);
            isiBerita.instance.showState = true;
            isiBerita.instance.imgBerita.sprite = img.sprite;
            isiBerita.instance.setBerita(jsonvale["judul_berita"].ToString(), jsonvale["isi_berita"].ToString());
            Debug.Log("Judul Berita: " + jsonvale["judul_berita"]);
        }
    }
}
