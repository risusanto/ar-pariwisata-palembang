using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonHandler : MonoBehaviour {

    public GameObject contactObj;
    public GameObject wisataObj;
    public GameObject testimoniObj;

    bool showTestimoni;
    bool showContact;
    bool showBerita;
    bool showWisata;
    testimoni newTesti;
	// Use this for initialization
	void Start () {
        showTestimoni = false;
        showContact = false;
        showWisata = false;
        newTesti = new testimoni();
	}
	
	// Update is called once per frame
	void Update () {
        contactObj.SetActive(showContact);
        wisataObj.SetActive(showWisata);
        testimoniObj.SetActive(showTestimoni);
	}

    /*Testimoni handler:
     * get input from input field
     * nama, email, testimoni
     */
    public void getNama(string nama)
    {
        newTesti.nama = nama;
    }

    public void getEmail(string email)
    {
        newTesti.email = email;
    }

    public void getTesti(string testi)
    {
        newTesti.testi = testi;
    }

    //--------------------- Button Handler -------------------------------------

    public void kontak()
    {
        if (showBerita)
            showBerita = false;
        showContact = !showContact;
    }

    public void wisata_btn()
    {
        showWisata = !showWisata;
    }

    public void berita()
    {
        SceneManager.LoadScene("berita");
    }

    public void wisataAlam()
    {
        SceneManager.LoadScene("alam");
    }

    public void wisataBuatan()
    {
        SceneManager.LoadScene("buatan");
    }

    public void wisataSejarah()
    {
        SceneManager.LoadScene("sejarah-budaya");
    }

    public void loadCobaAR()
    {
        SceneManager.LoadScene("MenuCobaAR");
    }

    public void testimoni_btn()
    {
        showTestimoni = !showTestimoni;
    }

    public void kirimTesti()
    {
        StartCoroutine(upload());
        showTestimoni = !showTestimoni;
        // Debug.Log(string.Format("{0}, {1}, {2}", newTesti.nama, newTesti.email, newTesti.testi));
    }

    public void keluar()
    {
        Application.Quit();
    }

    //--------------------- Button Handler -------------------------------------

    //---- Testimoni Send ---
    IEnumerator upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("testimoni", newTesti.testi);
        form.AddField("nama", newTesti.nama);
        form.AddField("email", newTesti.email);

        UnityWebRequest www = UnityWebRequest.Post("http://52.187.131.133/api/testimoni", form);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log("error");
        }
        else
        {
            Debug.Log(www.responseCode);
        }

    }
}

class testimoni
{
    public string nama { set; get; }
    public string email { set; get; }
    public string testi { set; get; }
}
