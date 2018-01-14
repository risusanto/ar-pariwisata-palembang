using UnityEngine;
using UnityEngine.SceneManagement;

public class ArScene : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
	}

    public void cobaAr()
    {
        SceneManager.LoadScene("percobaan_1");
    }

    public void bestView()
    {
        SceneManager.LoadScene("BestView");
    }
}
