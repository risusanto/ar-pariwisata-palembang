using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class headerObjWisata : MonoBehaviour {

    public Text id;
    public Image img;

    private string url = "52.187.131.133/api/getphotowisata/";
    private string imgUrl = "52.187.131.133/assets/img/objek_wisata/";
    private string detailUrl = "52.187.131.133/api/wisatagetrow/";
    private string jsonString;
    private string jsonDetail;
    private bool detailLoaded;
    private bool isloaded;
    // Use this for initialization

    void Start () {
        jsonString = null;
        isloaded = false;
        url += id.text;
        jsonDetail = null;
        detailLoaded = false;
        Debug.Log(url);
        StartCoroutine(DownloadTest(url));
        StartCoroutine(DownloadDetail(detailUrl + id.text));
	}
	
	// Update is called once per frame
	void Update () {
        if (jsonString == null)
            return;
        else
        {
            if(isloaded == false)
            {
                JsonData jsonVal = JsonMapper.ToObject(jsonString);
                Debug.Log(jsonVal.Count);
                if(jsonVal.Count > 0)
                {
                    StartCoroutine(downloadImage(jsonVal[0]["id_foto"].ToString()));
                }
                Debug.Log(jsonString);
                isloaded = true;
            }
        }
	}

    private IEnumerator DownloadTest(string url)
    {
        WWW www = new WWW("http://" + url);
        yield return www;
        Debug.Log(url);
        jsonString = www.text;
    }

    private IEnumerator DownloadDetail(string url)
    {
        WWW www = new WWW("http://" + url);
        yield return www;
        Debug.Log(url);
        jsonDetail = www.text;
    }

    IEnumerator downloadImage(string id)
    {
        var wwwImage = new WWW("http://" + imgUrl + id + ".jpg");
        Debug.Log(imgUrl + id + ".jpg");
        yield return wwwImage;
        Texture2D texture = new Texture2D(wwwImage.texture.width, wwwImage.texture.height, TextureFormat.DXT1, false);

        wwwImage.LoadImageIntoTexture(texture);
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        img.sprite = spriteToUse;

        wwwImage.Dispose();
        wwwImage = null;
    }

    public void detailWisata()
    {
        JsonData jsonVal = JsonMapper.ToObject(jsonDetail);
        Text[] objDetail;
        wisataController.instance.showDetail = !wisataController.instance.showDetail;
        objDetail = wisataController.instance.detail.GetComponentsInChildren<Text>(true);
        objDetail[0].text = jsonVal[0]["detail_objek_wisata"].ToString();
        objDetail[1].text = jsonVal[0]["nama_objek_wisata"].ToString();
        wisataController.instance.latLong = jsonVal[0]["latlong_objek_wisata"].ToString();
    }
}
