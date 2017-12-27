using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wisataHandler : MonoBehaviour {

    public GameObject[] objekWisata;
    public GameObject position;
    public static wisataHandler instance;

    private GameObject wisataActive;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void create(int i)
    {
        wisataActive = objekWisata[i];
        Instantiate(wisataActive,position.transform);
    }

    public void tutup()
    {
        Destroy(wisataActive);
        try
        {
            Destroy(wisataActive);
        }
        catch
        {

        }
    }
}
