using UnityEngine;

public class objekWisataHandler : MonoBehaviour {
    public int indexWisata;

    public void tampil()
    {
        wisataHandler.instance.create(indexWisata);
    }

    public void tutup()
    {
        Destroy(gameObject);
    }
}
