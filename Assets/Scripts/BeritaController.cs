using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;

public class BeritaController : MonoBehaviour {

    public GameObject headlinePosition;
    public GameObject headlineBeritaObj;
    public static BeritaController instance;
    public float yPosition;
    private string url = "52.187.131.133/api";
    public string jsonString;
    public bool isLoaded;

    Text[] newJudul;
    string imageUrl = "52.187.131.133/assets/img/berita/";
    List<Sprite> beritaSprites = new List<Sprite>();
    bool imgLoaded;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        isLoaded = false;
        imgLoaded = false;
        jsonString = null;
        StartCoroutine(DownloadTest(url));
        //StartCoroutine(downloadImage(imageUrl+"2.jpg"));
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        JsonData jsonVal = JsonMapper.ToObject(jsonString);
        if (jsonString == null)
            return;
        if (isLoaded == false)
        {
            for (int i = 0; i < jsonVal.Count; i++)
            {
                StartCoroutine(downloadImage(jsonVal[i]["id_berita"].ToString()));
                Debug.Log(beritaSprites.Count);
            }
            isLoaded = true;
        }
        if (beritaSprites.Count == jsonVal.Count && imgLoaded == false)
        {
            createBerita();
            imgLoaded = true;
        }
    }

    private IEnumerator DownloadTest(string url)
    {
        WWW www = new WWW("http://"+url);
        yield return www;
        Debug.Log(url);
        jsonString = www.text;
    }

    public void createBerita()
    {
        JsonData jsonVal = JsonMapper.ToObject(jsonString);
        float newPos = yPosition;
        Image[] newImage;
        for(int i = 0; i < jsonVal.Count; i++)
        {
            headlineBeritaObj.transform.position = new Vector3(headlineBeritaObj.transform.position.x,
                    newPos - (i * 200), headlineBeritaObj.transform.position.z);
            newJudul = headlineBeritaObj.GetComponentsInChildren<Text>(true);
            newImage = headlineBeritaObj.GetComponentsInChildren<Image>();
            newImage[1].sprite = beritaSprites[i];
            newJudul[0].text = jsonVal[i]["judul_berita"].ToString();
            newJudul[1].text = jsonVal[i]["id_berita"].ToString();
            Instantiate(headlineBeritaObj, headlinePosition.transform);
            headlineBeritaObj.transform.position = new Vector3(headlineBeritaObj.transform.position.x,
                yPosition, headlineBeritaObj.transform.position.z);
        }
    }
    IEnumerator downloadImage(string id)
    {
        var wwwImage = new WWW("http://" + imageUrl +id+".jpg");
        Debug.Log(imageUrl + id + ".jpg");
        yield return wwwImage;
        Texture2D texture = new Texture2D(wwwImage.texture.width, wwwImage.texture.height, TextureFormat.DXT1, false);

        wwwImage.LoadImageIntoTexture(texture);
        Rect rec = new Rect(0, 0, texture.width, texture.height);
        Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        beritaSprites.Add(spriteToUse);

        wwwImage.Dispose();
        wwwImage = null;
    }
}
