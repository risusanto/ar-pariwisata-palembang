using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArPositionController : MonoBehaviour
{
    public Text idxPos;
    public string[] latposList = new string[10];
    public string[] nameList = new string[10];

    private string latlong;
    private string keterangan;

    void Start()
    {
        StartCoroutine(StartPos());
        latlong = "Lokasi tidak diketahui";
    }

    IEnumerator StartPos()
    {
        Debug.Log("jalan");
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            latlong = "Gagal Membaca lokasi";
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 8;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 8 seconds
        if (maxWait < 1)
        {
            latlong = "timeout";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            latlong = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            latlong = string.Format("{0},{1}", Input.location.lastData.latitude.ToString(), Input.location.lastData.longitude.ToString());
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    void Update()
    {
        keterangan = "<tidak diketahui>";
        for(int i = 0; i < 10; i++)
        {
            if(latlong == latposList[i])
            {
                keterangan = nameList[i];
                break;
            }
        }
        idxPos.text = string.Format("Lokasi: {0} ({1})", latlong,keterangan);
    }
}