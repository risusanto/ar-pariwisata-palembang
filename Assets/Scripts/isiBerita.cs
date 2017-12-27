using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isiBerita : MonoBehaviour {
    
    public static isiBerita instance;
    public bool showState;
    public Image imgBerita;
    public Text judul;
    public Text isi;
    public GameObject isi_window;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        showState = false;
	}
	
	// Update is called once per frame
	void Update () {
        isi_window.SetActive(showState);
	}

    public void setBerita(string strjudul,string strisi)
    {
        judul.text = strjudul;
        isi.text = strisi;
    }

    public void close()
    {
        showState = false;
    }
}
